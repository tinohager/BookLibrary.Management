# BookLibrary.Management
Library Management

## Start a sql server with docker

Start Powershell and execute following command

```docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Secure-Password.1234' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest```
