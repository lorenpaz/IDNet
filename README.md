# IDNet

IDNet es un framework de conexión P2P de bases de datos distribuidas e independientes.

![icono IDNet](/iconoIDNet.png)

“IDNet” es un framework consistente en la creación de una red de conexión Peer-to-Peer
(P2P) para conectar bases de datos independientes y distribuidas, es decir, sin necesidad de
discriminar la estructura interna de la misma y sin estar centralizadas las bases de datos.
El prototipo está diseñado con el objetivo de soportar la creación de Organizaciones Vir-
tuales, en las cuáles los usuarios pueden obtener información de las bases de datos de sus
vecinos, pudiendo realizar consultas a la base de datos vecina. La información sensible trans-
mitida entre los vecinos se encuentra securizada mediante una capa de seguridad inherente
al framework.
La red P2P está implementada mediante contenedores Docker en Cloud mediante el pro-
veedor Amazon Web Services, aportándonos elasticidad y capacidad de respuesta a la misma.

##Normas de uso

Dentro de las carpetas IDNet-AWS-Nodo1 y IDNet-AWS-Nodo1 se encuentran los archivos de arranque para lanzar IDNetSoftware junto con IDNetDaemon a sus respectivos nodos en Amazon Web Services, en los cuáles se ejecuta el código del nodo GateKeeper.

Por otro lado, en las carpetas IDNetSoftware, IDNetDaemon y GateKeeper se encuentran los códigos fuentes, tanto del nodo cliente (IDNetSoftware y IDNetDaemon) como el nodo GateKeeper.

##Web de soporte
En el siguiente link [icono IDNet](http://lorenpaz.github.io/IDNet) se encuentra la web con la información sobre el proyecto.
 
