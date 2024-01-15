FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BookShop.API/BookShop.API.csproj", "src/BookShop.API/"]
COPY ["src/BookShop.Infrastructure/BookShop.Infrastructure.csproj", "src/BookShop.Infrastructure/"]
COPY ["src/BookShop.Domain/BookShop.Domain.csproj", "src/BookShop.Domain/"]
RUN dotnet restore "./src/BookShop.API/./BookShop.API.csproj"
COPY . .
WORKDIR "/src/src/BookShop.API"
RUN dotnet build "./BookShop.API.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BookShop.API.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookShop.API.dll"]