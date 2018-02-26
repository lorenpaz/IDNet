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
            try
            {
                MainAsync().Wait();
            }catch(Exception e){
                this._salida = "<error>Ha ocurrido un error en el vecino</error>";
            }
            return this._salida;
		}

		//Realizar consulta a la BBBDD
        public string SelectRequest(XmlNode body)
        {
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
            j += "\"name\": \""+this._databaseName+"\",";
            j += "\"colecciones\":[";

            while (collection.MoveNext())
            {
                foreach (var collDoc in collection.Current)
                {
                    collDoc.Remove("info");
                    collDoc.Remove("idIndex");
                    JToken doc = JObject.Parse(collDoc.ToJson());

					string nombreColeccion = (string) doc.SelectToken("name");
                    var coleccion = this._database.GetCollection<BsonDocument>(nombreColeccion);

                    //var firstDocument = coleccion.FindAsync(_ => true).Result.Single();
                   // var command = new BsonDocument("usersInfo", 1);
                    var firstDocument = await coleccion.Find(new BsonDocument()).FirstOrDefaultAsync();
                    firstDocument.Remove("_id");
                    j += "{\"name\":"+"\""+nombreColeccion+"\""+",\"ejemploColeccion\":"+firstDocument +"}";
				    j +=    ",";	
                }
                j = j.Remove(j.Length - 1,1);
            }
            j += "]}";
            this._salida = j;
        }

        private String ConvertSchema(BsonDocument firstDocument)
        {
            Dictionary<string,Object> document = firstDocument.ToDictionary();

            foreach(KeyValuePair<string,Object> entry in document)
            {
                firstDocument[entry.Key] = BsonValue.Create(entry.Value.GetType());
            }

            return document.ToJson();
        }

		public class ConsultaMongo
		{
			string _selectTarget;
			string _fromTarget;
			string _whereTarget;
			string _consulta;

			public String Consulta
			{
				get
				{
					return this._consulta;
				}
				set
				{
					this._consulta = value;
				}
			}

            public ConsultaMongo(XmlNode body)
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(body.InnerXml);
			}
		}
		
    }
}
