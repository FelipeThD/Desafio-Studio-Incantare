# --------- Build Stage ---------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o arquivo de projeto e restaura dependências
COPY BackendTraining/BackendTraining.csproj ./BackendTraining/
RUN dotnet restore ./BackendTraining/BackendTraining.csproj

# Copia todo o código do projeto
COPY BackendTraining/. ./BackendTraining/

# Publica a aplicação em Release para pasta /app/publish
RUN dotnet publish ./BackendTraining/BackendTraining.csproj -c Release -o /app/publish

# --------- Runtime Stage ---------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia o output publicado do build stage
COPY --from=build /app/publish .

# Expõe a porta padrão da API
EXPOSE 5000

# Define o entrypoint da aplicação
ENTRYPOINT ["dotnet", "BackendTraining.dll"]
