    using System;
using MySql.Data.MySqlClient;
using log4net;

namespace GateKeeperListener
{
    public class RemoteDatabase
    {
        //Conector MySQL
        private MySqlConnection _mysql;

        //String con la conexion
        private String _connectionString;
		private ILog log = LogManager.GetLogger(typeof(Pathfinder));

		/*
         * Constructor
        */
		public RemoteDatabase()
        {
            this._connectionString = Constants.MYSQL_REMOTE;
            this._mysql = null;
        }


		/*
        * Método para comprobar el codigo de un usuario
        * */
		public bool CheckCode(string username, string code)
		{
			Random r = new Random();
			bool ok = false;
			string codigo = null;
			try
			{
				//Connection
				this._mysql = new MySqlConnection(this._connectionString);
				this._mysql.Open();

				//Query
				string query = Constants.MysqlRemoteSelect();

				//Create Command
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = this._mysql;
				cmd.CommandText = query;
				cmd.Parameters.AddWithValue("username", username);

				//Create a data reader and Execute the command
				MySqlDataReader dataReader = cmd.ExecuteReader();


				//Read the data and check password
				while (dataReader.Read())
				{
					string usuario = (String)dataReader["username"];
					codigo = dataReader["code"].ToString();
                    ok = codigo.Equals(code);
				}

				//close Data Reader
				dataReader.Close();

				//close Connection
				this._mysql.Close();
			}
			catch (MySqlException)
			{
                log.Error("Ha habido un error con la comprobacion del codigo de seguridad");
                if(codigo != null)
                {
                    log.Error("Codigo recibido:"+code+".Codigo legitimo:"+codigo);
                }
			}

			return ok;
		}
    }
}
