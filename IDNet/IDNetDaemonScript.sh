#!/bin/bash

PATH_NAME="IDNetDaemon/IDNetDaemon/bin/Debug/"
PROGRAM_NAME="IDNetDaemon"
LOCK_FILE="/tmp/"${PROGRAM_NAME}".lock"

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
			rm -rf /tmp/IDNetDaemon.lock
	                echo "IDNetDaemon stop"
	        else
	                echo "Error stop demon"
        fi
    else
        echo "IDNetDaemon is not running"
    fi
}

start()
{
    mono-service -l:${LOCK_FILE} ./${PATH_NAME}${PROGRAM_NAME}.exe
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
