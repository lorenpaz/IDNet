#!/usr/bin/env python
# -*- coding: utf-8 -*-
#Juan Mas Aguilar, Lorenzo De La Paz Suárez
# Consultas MongoDB
from pymongo import MongoClient
import sys

#Ejemplo de conexión a la base de datos 'usuarios' y a la tabla 'usuariosTelefonica'
mongoclient = MongoClient()
db = mongoclient['usuarios']
c = db['usuariosTelefonica']

#Recogemos el primer parametro para saber si quiere que cojamos todos los datos de la tabla
aux = str(sys.argv[1])

#Ejemplo
if (aux == "selectall"):
	doc = c.find()
	for user in doc:
		print user['nombre']+' ' + user['Apellidos']
else:
	print "No todos"