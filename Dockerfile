# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar arquivo de projeto e restaurar dependências
COPY ["BackendTraining/BackendTraining.csproj", "BackendTraining/"]
RUN dotnet restore "BackendTraining/BackendTraining.csproj"

# Copiar todo o projeto e publicar
COPY BackendTraining/. ./BackendTraining/
WORKDIR /src/BackendTraining
RUN dotnet publish -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "BackendTraining.dll"]
