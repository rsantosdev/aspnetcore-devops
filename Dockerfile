FROM microsoft/aspnetcore
COPY src/ContactManager.Api/bin/Release/netcoreapp2.0/publish/ /root/
EXPOSE 5000/tcp
WORKDIR /root
ENTRYPOINT dotnet ContactManager.Api.dll