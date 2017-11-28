using System;
using MessageLibraryS;

namespace ConstantsLibraryS
{
    //Clase con las constantes
    public static class Constants
    {
        public const string CONFIG = @"./config/";
        public const string ConfigFileDatabases = CONFIG+@"databases.conf";
		public const string ConfigFileNeighbours = CONFIG+@"neighbours.conf";

        public const string SOLICITUD_ESQUEMA = @"Solicitud de esquema de base de datos";
        public const string RESPUESTA_ESQUEMA = @"Respuesta a la solicitud de esquema de la base de datos";

        public const string USUARIO_SOLICITADO = @"Usuario solicitado: ";
        public const string USUARIO_RESPUESTA = "Usuario: ";

        public const string UNABLE_CONNECT_MYSQL_HOSTS = @"Unable to connect to any of the specified MYSQL hosts";
        public const string ACCESS_DENIED_MYSQL = "Access Denied: Check DB name, username, password";
        public const string NO_ERROR_MYSQL = @"There is not error in MYSQL Server";

        public const string UNABLE_CONNECT_MONGODB = @"Check your MongoDB Server";
        public const string NO_ERROR_MONGODB = @"There is not error in MongoDB Server";

        public static string SolicitudEsquema(Message messageRequest)
        {
			return "Status: " + messageRequest.MessageType + " " + Constants.SOLICITUD_ESQUEMA + "\n" +
				Constants.USUARIO_SOLICITADO + messageRequest.Destination + "\n";
        }

        public static string RespuestaEsquema(Message messageResponse)
        {
            return "Status: " + messageResponse.MessageType + " " + Constants.RESPUESTA_ESQUEMA + "\n" +
                Constants.USUARIO_RESPUESTA + messageResponse.Destination + "\n" +
                "+----------------------+" + "\n"+
                "|                                |" + "\n"+
                "|                                |"+ messageResponse.Body;
		}
    }
}
