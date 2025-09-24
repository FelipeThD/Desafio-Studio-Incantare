# ===== BUILD STAGE =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o arquivo de projeto e restaura dependências
COPY BackendTraining/BackendTraining.csproj BackendTraining/
RUN dotnet restore BackendTraining/BackendTraining.csproj

# Copia todo o restante do projeto
COPY BackendTraining/. BackendTraining/
WORKDIR /src/BackendTraining

# Publica a aplicação
RUN dotnet publish -c Release -o /app/publish

# ===== RUNTIME STAGE =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

# Define a porta que a aplicação vai usar
EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "BackendTraining.dll"]
