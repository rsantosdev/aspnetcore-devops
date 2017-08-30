FROM microsoft/aspnetcore
COPY src/ContactManager.Api/bin/Release/netcoreapp2.0/publish/ /root/
EXPOSE 80
WORKDIR /root
ENTRYPOINT dotnet ContactManager.Api.dll