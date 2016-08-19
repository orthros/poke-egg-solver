FROM microsoft/dotnet:latest
MAINTAINER Colin Nelson
COPY ./bin/. /app
WORKDIR /app

EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000

ENTRYPOINT ["dotnet", "eggsolve.host.dll"]
