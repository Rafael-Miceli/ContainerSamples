FROM microsoft/dotnet:2.2-sdk as build

WORKDIR /app

COPY api/app-clients-searcher.csproj api/

RUN dotnet restore api/app-clients-searcher.csproj

COPY . . 

RUN dotnet publish api/app-clients-searcher.csproj -c Release -o /publish

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "app-clients-searcher.dll"]