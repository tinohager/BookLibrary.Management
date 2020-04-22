# BookLibrary.Management (Library Management)

This is a simple prototype for a library. You can create books, authors, publisher, customers. The books can borrow a customer.

- asp.net core 3 Backend
- vue frontend with element-ui
- ef core dal

## Start a sql server with docker

Start Powershell and execute following command

```docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Secure-Password.1234' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest```
