Limpiar el proyecto antes de subirlo a Github please
------------------------------------------------------------------
ARRANCAR DAEMON:

-Abrir terminal en la carpeta IDNet/IDNetDaemon

-Ejecutar el script que aparece:
./IDNetDaemonScript.sh start

-Comprobar que está operativo: ps a
-Ver la carpeta logs con la información. La carpeta log se crea cuando se lanza el demonio

-Terminar daemon:
./IDNetDaemonScript.sh stop
 
ADVERTENCIA: si por algún casual NO ves el demonio corriendo, haz: rm /tmp/IDNetDaemon.lock
A veces me ha pasado eso, debido a que HAY ERRORES por algún lado

Cosas que funcionan:
-Contesta al cliente PruebaClient.exe
-Caso 005

Cosas que le faltan:
-Los SELECT hacerlos en código
-Usuario ponga su usuario y contraseña de la bbdd. Para mongodb (por defecto no hay) y para mysql
-EL servidor y el cliente actuen con SSL. Ahora son sockets normales
-Los casos 004 y 006. 
Entre otras cosas jaja
-------------------------------------------------------------
PruebaClient.exe

Es una prueba que envia un mensaje de tipo 002 con origen,destino,tipoBBDD,nombreBBDD,etc
Funciona! Lo probé con mysql (debe de estar corriendo el demonio, y la contraseña de la BBDD debes de ponerla en el código como la mía está ahora, en PluginMysql)

Corres el demonio, lanzas el cliente y verás en el log que aparece la respuesta!

-----------------------------------------------------------------
IDNETSOFTWARE

-Funciona añadir una base de datos. Con el icono que hay en la barra  o en el menú bar Bases de datos-Añadir.


