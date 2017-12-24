using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using log4net;
using System;
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

		//Realizar consulta a la BBBDD
		public string SelectRequest()
        {
            return this._salida;
        }

        //Tarea para la obtención de la información de la BBDD
        async Task MainAsync()
        {
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
                    JToken doc = JObject.Parse(collDoc.ToJson());

					string nombreColeccion = (string) doc.SelectToken("name");
                    var coleccion = this._database.GetCollection<BsonDocument>(nombreColeccion);

                    var firstDocument = coleccion.FindAsync(_ => true).Result.Single();
                    firstDocument.Remove("_id");
                    j += "{\"name\":"+"\""+nombreColeccion+"\""+",\"ejemploColeccion\":"+firstDocument.ToJson() +"}";
				    j += ",";	
                }
                j = j.Remove(j.Length - 1,1);
            }
            j += "]}";
            this._salida = j;
        }

       /* static void Main(string[] args)
		{
            PluginMongo p = new PluginMongo("usuarios");
            p.EstructureRequest();

        }*/
		
    }
}
