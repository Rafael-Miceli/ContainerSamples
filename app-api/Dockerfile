FROM microsoft/dotnet:2.2-sdk as build

WORKDIR /app

COPY api/app-api.csproj api/

RUN dotnet restore api/app-api.csproj

COPY . . 

RUN dotnet publish api/app-api.csproj -c Release -o /publish

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "app-api.dll"]