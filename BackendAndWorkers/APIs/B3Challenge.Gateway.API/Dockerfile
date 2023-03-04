#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["APIs/B3Challenge.Gateway.API/B3Challenge.Gateway.API.csproj", "APIs/B3Challenge.Gateway.API/"]
COPY ["Infra/B3Challenge.Domain/B3Challenge.Domain.csproj", "Infra/B3Challenge.Domain/"]
COPY ["Infra/B3Challenge.Rabbit/B3Challenge.Rabbit.csproj", "Infra/B3Challenge.Rabbit/"]
RUN dotnet restore "APIs/B3Challenge.Gateway.API/B3Challenge.Gateway.API.csproj"
COPY . .
WORKDIR "/src/APIs/B3Challenge.Gateway.API"
RUN dotnet build "B3Challenge.Gateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "B3Challenge.Gateway.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "B3Challenge.Gateway.API.dll"]