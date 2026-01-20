# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj primero para aprovechar cache
COPY ApiCuidarte/ApiCuidarte.csproj ApiCuidarte/
RUN dotnet restore ApiCuidarte/ApiCuidarte.csproj

# Copiar todo y publicar
COPY . .
RUN dotnet publish ApiCuidarte/ApiCuidarte.csproj -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Usar el puerto que Railway asigna
ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE 8080

ENTRYPOINT ["dotnet", "ApiCuidarte.dll"]