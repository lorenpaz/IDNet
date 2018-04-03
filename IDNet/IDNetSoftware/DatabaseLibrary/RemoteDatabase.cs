using System;
using MySql.Data.MySqlClient;

using ConstantsLibraryS;
using CriptoLibraryS;

namespace DatabaseLibrary
{
    public class RemoteDatabase
    {
        //Conector MySQL
        private MySqlConnection _mysql;

        //String con la conexion
        private String _connectionString;

        /*
         * Constructor
        */
        public RemoteDatabase()
        {
            this._connectionString = Constants.MYSQL_REMOTE;
            this._mysql = null;
        }

        /*
         * Método para comprobar el inicio de sesión de un usuario
         * */
        public bool CheckUser(string username, string password)
        {
            bool ok = false;
            try
            {
                //Connection
                this._mysql = new MySqlConnection(this._connectionString);
                this._mysql.Open();

                //QUery
                string query = Constants.MysqlRemoteSelect(username);

                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, this._mysql);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Encriptamos la contraseña
                byte[] passwordBytes = Cripto.EncryptSHA256(password);

                //Read the data and check password
                while (dataReader.Read())
                {
                    ok = passwordBytes == dataReader["password"];
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this._mysql.Close();

            }catch(MySqlException ){
                
            }

            return ok;
        }

        /*
         * Método para la inserción de un nuevo usuario
         * */
        public bool InsertUser(string username, string password)
        {
            bool ok = false;

            try
            {
                //Establecemos la conexión
                this._mysql = new MySqlConnection(this._connectionString);
                this._mysql.Open();

                //Encriptamos la contraseña
                byte[] passwordBytes = Cripto.EncryptSHA256(password);

                //Query
                string query = Constants.MysqlRemoteInsert(username,passwordBytes);

                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, this._mysql);

                //Execute command
                ok = cmd.ExecuteNonQuery()==1;

                //close connection
                this._mysql.Close();


            }catch (MySqlException ){
                
            }

            return ok;
        }
    }
}
