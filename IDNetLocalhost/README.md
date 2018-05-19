En el presente README se van a explicar qué aspectos son necesarios para lanzar IDNet en localhost:

	-Necesario una base de datos MySQL llamada 'IDNet' con una tabla llamada 'usuariosIDNet' creada de la siguiente forma:

CREATE TABLE usuariosIDNet (id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,username VARCHAR(50),password VARBINARY(255), code INT);

	-Necesario registrarse y posteriormente loguearse en la aplicación.

	-Las bases de datos que aparezcan en la aplicación deben de existir en tu local. 

	-Ejecutar el script launch: ./Launch.sh

	-EL archivo neighboursDatabases.conf debe de ser modificado de forma simultánea según se hagan modificaciones en el archivo datababases.conf y previamente a realizar la conexión. El primer usuario del archivo neighbours.conf debe de ser el propio usuario, con el fin de realizar pruebas en localhost a ti mismo.
