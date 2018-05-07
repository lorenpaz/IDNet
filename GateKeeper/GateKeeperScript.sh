#!/bin/bash

PATH_NAME="GKDaemon/bin/Release/"
PROGRAM_NAME="GKDaemon"
LOCK_FILE="/tmp/"${PROGRAM_NAME}".lock"
LOG_FOLDER="logFiles"

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
	                echo "GKDaemon stop"
	        else
			rm -rf ${LOCK_FILE}
			rm -rf ${LOG_FOLDER}
	                echo "Error stop demon"
        fi
    else
        echo "GKDaemon is not running"
    fi
}

start()
{
    echo "Arrancando el servicio "${PROGRAM_NAME}
    /usr/bin/mono-service -l:${LOCK_FILE} ./${PATH_NAME}${PROGRAM_NAME}.exe
    echo "Done"
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
