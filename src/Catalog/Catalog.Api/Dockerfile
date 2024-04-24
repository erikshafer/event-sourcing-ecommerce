FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5252
EXPOSE 5253

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Catalog/Catalog.Api/Catalog.Api.csproj", "src/Catalog/Catalog.Api/"]
COPY ["src/Core/Ecommerce.Core.WebApi/Ecommerce.Core.WebApi.csproj", "src/Core/Ecommerce.Core.WebApi/"]
COPY ["src/Core/Ecommerce.Core/Ecommerce.Core.csproj", "src/Core/Ecommerce.Core/"]
COPY ["src/Catalog/Catalog/Catalog.csproj", "src/Catalog/Catalog/"]
RUN dotnet restore "src/Catalog/Catalog.Api/Catalog.Api.csproj"
COPY . .
WORKDIR "/src/src/Catalog/Catalog.Api"
RUN dotnet build "Catalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Catalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Api.dll"]
