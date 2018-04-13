#FROM pwillie/mono-service
FROM tenninebt/mono-service
EXPOSE 11000
EXPOSE 12000
ADD . .
#RUN apt-get update
#RUN dpkg --configure -a
#RUN dpkg -i libmono-corlib.deb
#RUN dpkg -i libmono-posix.deb
#RUN dpkg -i libmono-system.deb
#RUN dpkg -i mono-runtime.deb
#RUN dpkg -i mono-2.0-service.deb
#RUN xbuild /p:Configuration=Release
#WORKDIR /GKDaemon
CMD [ "mono-service", "./GKDaemon/bin/Release/GKDaemon.exe" ]
#CMD ["ls","-a"]
