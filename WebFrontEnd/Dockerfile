# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used(by default base stage is used unless it is overriden)
# when running from VS in fast mode (Default for Debug configuration)
FROM base AS debug
RUN apt-get update && apt-get install -y procps iputils-ping
USER $APP_UID

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
#Copy all necessary csproj files here via separate COPY
COPY WebFrontEnd/*.csproj WebFrontEnd/
RUN dotnet restore WebFrontEnd/
#Copy all projects' sources via separate COPY
COPY WebFrontEnd/ WebFrontEnd
WORKDIR /src/WebFrontEnd
RUN dotnet build --no-restore -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish --no-restore -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER $APP_UID
ENTRYPOINT ["dotnet", "WebFrontEnd.dll"]