using System;
using MySql.Data.MySqlClient;

using ConstantsLibraryS;
using CriptoLibraryS;
using System.Linq;

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
                  cmd.Parameters.AddWithValue("username",username);

                  //Create a data reader and Execute the command
                  MySqlDataReader dataReader = cmd.ExecuteReader();

                  //Encriptamos la contraseña
                  byte[] passwordBytes = Cripto.EncryptSHA256(password);

                  //Read the data and check password
                  while (dataReader.Read())
                  {
                      string usuario = (String) dataReader["username"];
                      byte[] passwordRemote = (byte[]) dataReader["password"];
                      ok = passwordBytes.SequenceEqual(passwordRemote);
                  }

                  //close Data Reader
                  dataReader.Close();

              }catch(MySqlException ){

              }
            //bool ok = true;
            return ok;
        }

        /*
         * Método para la inserción de un nuevo usuario
         * */
        public bool InsertUser(string username, string password)
        {
            bool ok = false;
            Random r = new Random();
            try
            {
                //Establecemos la conexión
                this._mysql = new MySqlConnection(this._connectionString);
                this._mysql.Open();

                //Encriptamos la contraseña
                byte[] passwordBytes = Cripto.EncryptSHA256(password);

                //Query
                string query = Constants.MysqlRemoteInsert();

                //Random code
                int code = r.Next(5000);

                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = this._mysql;
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", passwordBytes);
                cmd.Parameters.AddWithValue("code", code);

                //Execute command
                ok = cmd.ExecuteNonQuery() == 1;

                //close connection
                this._mysql.Close();


            }catch (MySqlException e){
                string error = e.StackTrace;
            }

            return ok;
        }

        /*
         * Método público para guardar el usuario en un archivo
         * */
        public Tuple<string,int> SaveUserToFile(string username)
        {
            Tuple<string, int> tupla = null;
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
                    string usuario = (String) dataReader["username"];
                    int code = (int) dataReader["code"];
                    tupla = new Tuple<string, int>(usuario,code);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this._mysql.Close();
           // Tuple<string,int> tupla = new Tuple<string, int>("Lorenzo", 3456789);

            }
            catch (MySqlException)
            {

            }
            return tupla;
        }
    }
}
