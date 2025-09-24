# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar o .csproj que está na raiz
COPY BackendTraining.csproj ./

# Restaurar dependências
RUN dotnet restore BackendTraining.csproj

# Copiar todo o código fonte
COPY . ./

# Publicar a aplicação em modo Release
RUN dotnet publish BackendTraining.csproj -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

# Porta padrão do ASP.NET
EXPOSE 8080

ENTRYPOINT ["dotnet", "BackendTraining.dll"]
