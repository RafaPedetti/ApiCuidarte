# ===============================
# BUILD STAGE
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiamos todo el repo
COPY . .

# Restaurar dependencias
RUN dotnet restore ApiCuidarte/ApiCuidarte.csproj

# Publicar la API
RUN dotnet publish ApiCuidarte/ApiCuidarte.csproj -c Release -o /out

# ===============================
# RUNTIME STAGE
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar build
COPY --from=build /out .

# Railway usa este puerto
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Ejecutar la API
ENTRYPOINT ["dotnet", "ApiCuidarte.dll"]
