using System;
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
        public const string USUARIO_RESPUESTA = "Usuario respuesta: ";
    }
}
