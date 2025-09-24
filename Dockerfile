# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o arquivo de projeto e restaura as dependências
COPY BackendTraining.csproj ./
RUN dotnet restore

# Copia o restante do código
COPY . ./
RUN dotnet publish ./BackendTraining.csproj -c Release -o /app


# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "BackendTraining.dll"]