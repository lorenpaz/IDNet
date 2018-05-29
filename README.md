# IDNet

<!--[Creative](http://startbootstrap.com/template-overviews/creative/) is a one page creative theme for [Bootstrap](http://getbootstrap.com/) created by [Start Bootstrap](http://startbootstrap.com/).-->
IDNet es un framework de conexión P2P de bases de datos distribuidas e independientes.

[icono IDNet](Web/img/iconoIDNet.png) 
[![Licencia Github](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/BlackrockDigital/startbootstrap-creative/master/LICENSE)
## Resumen

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

<!--[![Creative Preview](https://startbootstrap.com/assets/img/templates/creative.jpg)](https://blackrockdigital.github.io/startbootstrap-creative/) 

**[View Live Preview](https://blackrockdigital.github.io/startbootstrap-creative/)**-->

## Descripción general

IDNet es, a priori, un modelo de red que permite conectar bases de datos e intercambiar
querys de forma anónima y segura. Sus principales bazas son el uso de criptografía simétrica
y asimétrica así como la implementación de protocolos propios de conexión y enrutado.
La red está organizada con una topología P2P que permite el anonimato y aumenta la
seguridad de las comunicaciones. Del mismo modo, está implementada para favorecer el
uso de “Organizaciones Virtuales”, un concepto que permitirá a IDNet establecer ámbitos
privados. Todo ello soportado sobre una estructura en cloud.

### Ventajas

IDNet conlleva una serie de ventajas:
* Distribución Geográfica. IDNet permite que dos bases de datos se puedan comuni-
car independientemente de dónde se sitúen físicamente. El uso de AWS favorece está
“independencia” geográfica.
* Arquitectura de la Base de Datos. A través de IDNet, dos bases de datos pueden
comunicarse independientemente de la arquitectura interna que usen o de la represen-
tación de los propios datos.
* Seguridad de las comunicaciones. Los datos intercambiados pueden ser de un nivel
de sensibilidad extremadamente alto. Por ello, IDNet se vale de técnicas de cifrado
simétrico y asimétrico para asegurar la privacidad de los datos y la legitimidad de las
comunicaciones.
* Anonimato de los nodos. Los nodos Cliente no son conscientes nunca de las direc-
ciones IP del resto de nodos, esto se asegura con un sistema eficaz de alias.

### Características técnicas

IDNet está compuesta por distintos tipos de nodos. Cada uno de ellos realiza una serie
de funciones que permiten la correcta comunicación entre ellos. Cabe destacar las siguientes
funcionalidades de cada nodo:

#### Nodo Cliente

* Procesamiento de las querys.
* Conexión a las distintas bases de datos.
* Conexión al GateKeeper y obtención de los nodos vecinos con los que se puede comunicar.
* Autenticación del usuario que usa el software.
* Interfaz gráfica para facilitar el uso.
* Creación del demonio e implementación de los protocolos diseñados.

#### Nodo GateKeeper

* Propagación de los mensajes del cliente.
* Anonimización de los mensajes del cliente.
* Enrutado de los mensajes que viajan por la red.
* Identificación de las conexiones nuevas.
* Proveer al nodo Cliente de los vecinos disponibles.
<!--
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/BlackrockDigital/startbootstrap-creative/master/LICENSE)
[![npm version](https://img.shields.io/npm/v/startbootstrap-creative.svg)](https://www.npmjs.com/package/startbootstrap-creative)
[![Build Status](https://travis-ci.org/BlackrockDigital/startbootstrap-creative.svg?branch=master)](https://travis-ci.org/BlackrockDigital/startbootstrap-creative)
[![dependencies Status](https://david-dm.org/BlackrockDigital/startbootstrap-creative/status.svg)](https://david-dm.org/BlackrockDigital/startbootstrap-creative)
[![devDependencies Status](https://david-dm.org/BlackrockDigital/startbootstrap-creative/dev-status.svg)](https://david-dm.org/BlackrockDigital/startbootstrap-creative?type=dev)-->

## Descarga y despliegue
Para descargar el proyecto, acuda a la siguiente [dirección web](https://github.com/lorenpaz/IDNet/tree/master/) y descargue nuestro código fuente.
Una vez descargado, acuda a la carpeta llamada 'IDNetAWS' y lance el script 'launch.sh' desde línea de comandos.
A continuación comenzará el despliegue de la aplicación.
<!--
To begin using this template, choose one of the following options to get started:
* [Download the latest release on Start Bootstrap](https://startbootstrap.com/template-overviews/creative/)
* Install via npm: `npm i startbootstrap-creative`
* Clone the repo: `git clone https://github.com/BlackrockDigital/startbootstrap-creative.git`
* [Fork, Clone, or Download on GitHub](https://github.com/BlackrockDigital/startbootstrap-creative)-->

## Uso

### Uso básico



### Uso avanzado


<!--
#### Gulp Tasks

- `gulp` the default task that builds everything
- `gulp dev` browserSync opens the project in your default browser and live reloads when changes are made
- `gulp sass` compiles SCSS files into CSS
- `gulp minify-css` minifies the compiled CSS file
- `gulp minify-js` minifies the themes JS file
- `gulp copy` copies dependencies from node_modules to the vendor directory

## Bugs and Issues

Have a bug or an issue with this template? [Open a new issue](https://github.com/BlackrockDigital/startbootstrap-creative/issues) here on GitHub or leave a comment on the [template overview page at Start Bootstrap](http://startbootstrap.com/template-overviews/creative/).

## Custom Builds

You can hire Start Bootstrap to create a custom build of any template, or create something from scratch using Bootstrap. For more information, visit the **[custom design services page](https://startbootstrap.com/bootstrap-design-services/)**.
-->
## Sobre nosotros

Los integrantes del presente Trabajo de Fin de Grado en Ingeniería Informática son:
* ![Lorenzo José de la Paz Suárez](https://github.com/lorenpaz/)


*  ![Juan Mas Aguilar](https://github.com/masju96/)
<!--Start Bootstrap is an open source library of free Bootstrap templates and themes. All of the free templates and themes on Start Bootstrap are released under the MIT license, which means you can use them for any purpose, even for commercial projects.

* https://startbootstrap.com
* https://twitter.com/SBootstrap

Start Bootstrap was created by and is maintained by **[David Miller](http://davidmiller.io/)**, Owner of [Blackrock Digital](http://blackrockdigital.io/).

* http://davidmiller.io
* https://twitter.com/davidmillerskt
* https://github.com/davidtmiller

Start Bootstrap is based on the [Bootstrap](http://getbootstrap.com/) framework created by [Mark Otto](https://twitter.com/mdo) and [Jacob Thorton](https://twitter.com/fat).-->

## Licencias

 Código lanzado bajo la licencia [MIT](https://github.com/BlackrockDigital/startbootstrap-creative/blob/gh-pages/LICENSE).

<!--Copyright 2013-2017 Blackrock Digital LLC. Code released under the [MIT](https://github.com/BlackrockDigital/startbootstrap-creative/blob/gh-pages/LICENSE) license.-->
