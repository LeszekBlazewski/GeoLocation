FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["GeoLocation.API/GeoLocation.API.csproj", "GeoLocation.API/"]
COPY ["GeoLocation.DAL/GeoLocation.DAL.csproj", "GeoLocation.DAL/"]
COPY ["GeoLocation.BL/GeoLocation.BL.csproj", "GeoLocation.BL/"]
RUN dotnet restore "GeoLocation.API/GeoLocation.API.csproj"
COPY . .
WORKDIR "/src/GeoLocation.API"
RUN dotnet build "GeoLocation.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeoLocation.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeoLocation.API.dll"]
