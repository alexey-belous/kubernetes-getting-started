FROM mcr.microsoft.com/dotnet/core/runtime:2.2

WORKDIR /app
COPY ./bin/Release/netcoreapp2.1/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "api.dll"]
