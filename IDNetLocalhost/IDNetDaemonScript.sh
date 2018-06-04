#!/bin/bash

PATH_NAME="IDNetDaemon/"
PROGRAM_NAME="IDNetDaemon"
LOCK_FILE="/tmp/"${PROGRAM_NAME}".lock"
LOG_FOLDER="logs"
BASEDIR=$(pwd)

usage()
{
    echo "$0 (start|stop)"
}

stop()
{
    if [ -e ${LOCK_FILE} ]
    then
        _pid=$(cat ${LOCK_FILE})
        kill $_pid
        rt=$?
        if [ "$rt" == "0" ]
	        then
			rm -rf ${LOCK_FILE}
			rm -rf ${LOG_FOLDER}
	                echo "IDNetDaemon stop"
	        else
			rm -rf ${LOCK_FILE}
			rm -rf ${LOG_FOLDER}
	                echo "Error stop demon"
        fi
    else
        echo "IDNetDaemon is not running"
	rm -rf ${LOCK_FILE}
	rm -rf ${LOG_FOLDER}
    fi
}

start()
{
    PATH="$(pwd)/${PATH_NAME}${PROGRAM_NAME}"
    echo "Arrancando el servicio "${PROGRAM_NAME} " desde el directorio: "$PATH
     /usr/bin/mono-service -l:${LOCK_FILE} -n:${PROGRAM_NAME} ${PATH}.exe
    echo "Service ${PROGRAM_NAME} started"
}

case $1 in
    "start")
            start
            ;;
    "stop")
            stop
            ;;
    *)
            usage
            ;;
esac
exit
