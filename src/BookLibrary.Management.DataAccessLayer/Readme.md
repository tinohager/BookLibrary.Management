1) Start a MSSQL Server

Start Powershell
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Secure-Password.1234' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest

2) Configuration

ConnectionString
"Server=localhost;Database=BookLibraryManagement;User Id=sa;Password=Secure-Password.1234;"