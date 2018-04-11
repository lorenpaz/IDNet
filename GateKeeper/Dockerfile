FROM mono
EXPOSE 11000
EXPOSE 12000
ADD . /GKDaemon
WORKDIR /GKDaemon
RUN apt-get update && apt-get install mono-complete
RUN xbuild /p:Configuration=Release
CMD [ "/usr/bin/mono-service", "./GKDaemon/bin/Release/GKDaemon.exe" ]
