using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Json;

namespace PluginsLibrary
{
    public class PluginMongo
    {
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

        //Solicitud de la estructura de la base de datos
        public string EstructureRequest()
        {
            MainAsync().Wait();
            return this._salida;
		}

        //Tarea para la obtención de la info de la BBDD
        async Task MainAsync()
        {
			this._client = new MongoClient();
            this._database = this._client.GetDatabase(this._databaseName);
            var collection = await this._database.ListCollectionsAsync();

            var j = "";
			while (collection.MoveNext())
			{
				foreach (var collDoc in collection.Current)
				{
                    j += collDoc.ToJson();
				}
			}
            this._salida = j;
        }
			//var collection = _database.GetCollection<BsonDocument>("empleados");
			//var a = await collection.Find(new BsonDocument()).ToListAsync();
			//salida = a[0].ToJson();

			// var a = await collection.ToListAsync();
		
    }
}
