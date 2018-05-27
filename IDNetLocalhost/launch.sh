#!/bin/sh
if ! pgrep -x "mono-sgen" > /dev/null
then
    ./IDNetDaemonScript.sh start
fi
exec /usr/bin/mono IDNetSoftware/IDNetSoftware.exe "$@"

