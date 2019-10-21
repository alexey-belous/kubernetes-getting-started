FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build_stage
WORKDIR /src
COPY ./ /src
RUN dotnet publish -o ./publish -c Release

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
WORKDIR /app
COPY --from=build_stage /src/publish /app
EXPOSE 5000
ENTRYPOINT ["dotnet", "Minikube.Registration.dll"]
