FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine

# Install cultures (same approach as Alpine SDK image)
RUN apk add --no-cache icu-libs

# Disable the invariant mode (set in base image)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

COPY api .
COPY spa ./wwwroot/ 
ENTRYPOINT ["dotnet", "BookLibrary.Management.WebApi.dll"]