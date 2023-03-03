#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["APIs/B3Challenge.Search.API/B3Challenge.Search.API.csproj", "APIs/B3Challenge.Search.API/"]
RUN dotnet restore "APIs/B3Challenge.Search.API/B3Challenge.Search.API.csproj"
COPY . .
WORKDIR "/src/APIs/B3Challenge.Search.API"
RUN dotnet build "B3Challenge.Search.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "B3Challenge.Search.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "B3Challenge.Search.API.dll"]