FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RekdApi/RekdApi.csproj", "RekdApi/"]
RUN dotnet restore "RekdApi/RekdApi.csproj"
COPY . .
WORKDIR "/src/RekdApi"
RUN dotnet build "RekdApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RekdApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM build AS migrate
WORKDIR /src/RekdApi
RUN dotnet tool install --local dotnet-ef --version 8.0.0
RUN dotnet ef database update

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RekdApi.dll"]