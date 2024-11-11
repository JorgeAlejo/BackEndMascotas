#base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

#compile and publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ENV ASPNETCORE_ENVIRONMENT=Development
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BackEndMascotas.csproj", "./"]
RUN dotnet restore "BackEndMascotas.csproj"

COPY . .
WORKDIR "/src/"
RUN dotnet build "BackEndMascotas.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BackEndMascotas.csproj" -c $BUILD_CONFIGURATION -o /app/publish

#Final Immage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "BackEndMascotas.dll", "--environment=Development"]