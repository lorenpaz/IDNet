#!/usr/bin/env python
# -*- coding: utf-8 -*-
#Juan Mas Aguilar, Lorenzo De La Paz Suárez
# Consultas MySQL
import MySQLdb

#Conexión base de datos MYSQL
db = MySQLdb.connect(host="localhost", user="root", passwd="mypassword", db="PythonU")

#Cursor
cursor = db.cursor()

#Ejemplo de consulta
cursor.execute("SELECT * FROM Students")