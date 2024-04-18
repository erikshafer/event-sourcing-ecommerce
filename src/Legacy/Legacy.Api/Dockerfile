FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Legacy/Legacy.Api/Legacy.Api.csproj", "src/Legacy/Legacy.Api/"]
COPY ["src/Legacy/Legacy.Data/Legacy.Data.csproj", "src/Legacy/Legacy.Data/"]
COPY ["src/Legacy/Legacy.Application/Legacy.Application.csproj", "src/Legacy/Legacy.Application/"]
RUN dotnet restore "src/Legacy/Legacy.Api/Legacy.Api.csproj"
COPY . .
WORKDIR "/src/src/Legacy/Legacy.Api"
RUN dotnet build "Legacy.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Legacy.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Legacy.Api.dll"]
