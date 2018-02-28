using System;
using System.Xml;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace PluginsLibrary
{   
    public class PluginMongo
    {
        static readonly ILog log = LogManager.GetLogger(typeof(PluginMongo));
        //Propiedades
        private IMongoClient _client;
        private IMongoDatabase _database;
        private string _databaseName;
        private string _salida;

        //Constructor
        public PluginMongo(string databaseName)
        {
            this._databaseName = databaseName;
        }

        public string Salida
        {
            get
            {
                return _salida;
            }
            set
            {
                _salida = value;
            }
        }

        //Solicitud de la estructura de la BBDD
        public string EstructureRequest()
        {
            MainAsync().Wait();
            return this._salida;
        }

        //Tarea para la obtención de la información de la BBDD
        async Task MainAsync()
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
            this._client = new MongoClient();
            this._database = this._client.GetDatabase(this._databaseName);
            var collection = await this._database.ListCollectionsAsync();

            List<JObject> objects = new List<JObject>();
            var j = "{\"database\": {";
            j += "\"name\": \"" + this._databaseName + "\",";
            j += "\"colecciones\":[";

            while (collection.MoveNext())
            {
                foreach (var collDoc in collection.Current)
                {
                    collDoc.Remove("info");
                    collDoc.Remove("idIndex");
                    JToken doc = JObject.Parse(collDoc.ToJson());

                    string nombreColeccion = (string)doc.SelectToken("name");
                    var coleccion = this._database.GetCollection<BsonDocument>(nombreColeccion);

                    //var firstDocument = coleccion.FindAsync(_ => true).Result.Single();
                    // var command = new BsonDocument("usersInfo", 1);
                    var firstDocument = await coleccion.Find(new BsonDocument()).FirstOrDefaultAsync();
                    firstDocument.Remove("_id");
                    j += "{\"name\":" + "\"" + nombreColeccion + "\"" + ",\"ejemploColeccion\":" + ConvertSchema(firstDocument) + "}";
                    j += ",";
                }
                j = j.Remove(j.Length - 1, 1);
            }
            j += "]}";
            this._salida = j;
        }

        //Realizar consulta a la BBBDD
        public string SelectRequest(XmlNode body)
        {
            SecondAsync(body).Wait();
            return this._salida;
        }

        async Task SecondAsync(XmlNode body)
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
            ConsultaMongo c = new ConsultaMongo(body);
            this._client = new MongoClient();
            this._database = this._client.GetDatabase(this._databaseName);

            var collection = this._database.GetCollection<BsonDocument>(c.CollectionTarget);
            FilterDefinition<BsonDocument> filter = null;
            IFindFluent<BsonDocument, BsonDocument> documents = null;

            if (c.FilterTarget == null)
            {
                filter = FilterDefinition<BsonDocument>.Empty;
            }
            else
            {
                filter = c.FilterTarget;
            }
            documents = collection.Find(filter);

            if (c.LimitTarget != null)
            {
                documents = documents.Limit(Int32.Parse(c.LimitTarget));
            }
            if (c.SortTarget != null)
            {
                documents = documents.Sort(c.SortTarget);
            }

            documents = documents.Project(c.ProjectionsTarget);

            var documentsList = await documents.ToListAsync();

            if (documentsList.Count != 0)
            {
                var j = "{\"row\": [";
                int i = 1;
                foreach (var doc in documentsList)
                {
                    doc.Remove("_id");

                    j += AttributesJson(doc.ToJson());
                    i += 1;
                    j += ",";
                }
                j = j.Remove(j.Length - 1, 1);
                j += "]}";
                this._salida = j;
            }
            else
            {
                this._salida = "<error></error>";
            }
        }

        private string AttributesJson(string json)
        {
            JObject c = JObject.Parse(json);
            JObject auxiliar = new JObject();
            foreach(var aux in c)
            {
                auxiliar.Add("@" +aux.Key,aux.Value);
            }

            return auxiliar.ToString();
        }

        private String ConvertSchema(BsonDocument firstDocument)
        {
            JObject c = JsonConvert.DeserializeObject<JObject>(firstDocument.ToJson());
            JObject aux = new JObject();

            foreach (var a in c)
            {
                aux.Add(a.Key, a.Value.Type.ToString());
            }

            return aux.ToString();
        }

        public class ConsultaMongo
        {
            string _collectionTarget;
            string _filterTarget;
            string _proyectionsTarget;
            string _sortTarget;
            string _limitTarget;

            public String CollectionTarget
            {
                get
                {
                    return this._collectionTarget;
                }
                set
                {
                    this._collectionTarget = value;
                }
            }
            public String ProjectionsTarget
            {
                get
                {
                    return this._proyectionsTarget;
                }
                set
                {
                    this._proyectionsTarget = value;
                }
            }
            public String FilterTarget
            {
                get
                {
                    return this._filterTarget;
                }
                set
                {
                    this._filterTarget = value;
                }
            }
            public String SortTarget
            {
                get
                {
                    return this._sortTarget;
                }
                set
                {
                    this._sortTarget = value;
                }
            }
            public String LimitTarget
            {
                get
                {
                    return this._limitTarget;
                }
                set
                {
                    this._limitTarget = value;
                }
            }

            public ConsultaMongo(XmlNode body)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(body.InnerXml);

                //Collection
                this._collectionTarget = doc.GetElementsByTagName("collection")[0].InnerText;

                //Filter
                string filterField = doc.GetElementsByTagName("filter")[0].InnerText;
                if (filterField == "")
                {
                    this._filterTarget = null;
                }
                else
                {
                    string[] filter = filterField.Split();
                    this._filterTarget = "{";
                    if (filter[1] == "$eq" || filter[1] == "$ne")
                    {
                        this._filterTarget += filter[0] + " : { '" + filter[1] + "' : '" + filter[2] + "' } }";
                    }
                }

                //Projection
                XmlAttributeCollection listAttributes = doc.GetElementsByTagName("projection")[0].Attributes;
                this._proyectionsTarget = "{";
                int i = 1;
                foreach (XmlAttribute attribute in listAttributes)
                {
                    if (i == listAttributes.Count)
                    {
                        this._proyectionsTarget += attribute.Name + ": " + attribute.Value + "}";
                    }
                    else
                    {
                        this._proyectionsTarget += attribute.Name + ": " + attribute.Value + ",";
                    }
                    i++;
                }

                //Sort
                string sort = doc.GetElementsByTagName("sort")[0].InnerText;
                if (sort != "")
                {
                    this._sortTarget = "{" + sort + ": 1}";
                }
                else
                {
                    this._sortTarget = null;
                }

                //Limit
                string limitField = doc.GetElementsByTagName("limit")[0].InnerText;
                if (limitField == "" || limitField == "None")
                {
                    this._limitTarget = null;
                }
                else
                {
                    this._limitTarget = doc.GetElementsByTagName("limit")[0].InnerText;
                }
            }
        }
        /* static void Main(string[] args)
         {
             PluginMongo p = new PluginMongo("usuarios");
             p.EstructureRequest();
         }*/

    }
}
