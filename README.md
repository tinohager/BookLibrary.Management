# BookLibrary.Management (Library Management)

This is a simple prototype for a library management. You can create books, authors, publisher, customers. The books can borrow a customer.

- asp.net core 3 Backend
- [vue](https://github.com/vuejs/vue) frontend with [element-ui](https://github.com/ElemeFE/element)
- ef core

## Requirements

### API
 - Visual Studio 2019
 - mssql server (see docker)

### SPA
 - Visual Studio Code
 - nodejs >= v12
 - npm

## Start in production
Run this commands in the directory of the `docker-compose.yml` file.
```
docker-compose build
docker-compose up
```

## Start a sql server with docker

Start Powershell and execute following command

```docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Secure-Password.1234' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest```
