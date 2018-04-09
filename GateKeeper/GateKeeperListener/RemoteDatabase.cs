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
					string codigo = (String)dataReader["code"];
                    ok = codigo.Equals(code);
				}

				//close Data Reader
				dataReader.Close();

				//close Connection
				this._mysql.Close();
			}
			catch (MySqlException)
			{
                log.Error("Ha habido un error con la comprobación del código de seguridad");
			}

			return ok;
		}
    }
}
