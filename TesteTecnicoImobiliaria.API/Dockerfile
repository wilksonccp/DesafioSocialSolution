FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY TesteTecnicoImobiliaria.sln ./
COPY TesteTecnicoImobiliaria.API/TesteTecnicoImobiliaria.API.csproj TesteTecnicoImobiliaria.API/
COPY TesteTecnicoImobiliaria.WEB/TesteTecnicoImobiliaria.WEB.csproj TesteTecnicoImobiliaria.WEB/
COPY TesteTecnicoImobiliaria.DAL/TesteTecnicoImobiliaria.DAL.csproj TesteTecnicoImobiliaria.DAL/
COPY TesteTecnicoImobiliaria.Modelo/TesteTecnicoImobiliaria.Modelo.csproj TesteTecnicoImobiliaria.Modelo/
COPY TesteTecnicoImobiliaria.Regra/TesteTecnicoImobiliaria.Regra.csproj TesteTecnicoImobiliaria.Regra/
RUN dotnet restore TesteTecnicoImobiliaria.API/TesteTecnicoImobiliaria.API.csproj
COPY . .
RUN dotnet publish TesteTecnicoImobiliaria.API/TesteTecnicoImobiliaria.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "TesteTecnicoImobiliaria.API.dll"]
