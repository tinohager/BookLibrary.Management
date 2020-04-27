FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
COPY api .
COPY spa ./wwwroot/ 
ENTRYPOINT ["dotnet", "BookLibrary.Management.WebApi.dll"]