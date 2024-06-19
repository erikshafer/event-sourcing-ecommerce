FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5262
EXPOSE 5263

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ShoppingCart.Api/ShoppingCart.Api.csproj", "ShoppingCart.Api/"]
COPY ["src/Retail/ShoppingCart/ShoppingCart.csproj", "src/Retail/ShoppingCart/"]
COPY ["src/Core/Ecommerce.Core/Ecommerce.Core.csproj", "src/Core/Ecommerce.Core/"]
COPY ["src/Core/Ecommerce.Eventuous/Ecommerce.Eventuous.csproj", "src/Core/Ecommerce.Eventuous/"]
RUN dotnet restore "ShoppingCart.Api/ShoppingCart.Api.csproj"
COPY . .
WORKDIR "/src/ShoppingCart.Api"
RUN dotnet build "ShoppingCart.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ShoppingCart.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingCart.Api.dll"]
