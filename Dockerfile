# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de projeto e restaura dependências
COPY BackendTraining/BackendTraining.csproj ./BackendTraining/
RUN dotnet restore ./BackendTraining/BackendTraining.csproj

# Copia o restante do código
COPY BackendTraining/. ./BackendTraining/

# Publica
WORKDIR /src/BackendTraining
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BackendTraining.dll"]