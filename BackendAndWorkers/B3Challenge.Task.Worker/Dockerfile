#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["B3Challenge.Task.Worker/B3Challenge.Task.Worker.csproj", "B3Challenge.Task.Worker/"]
COPY ["Infra/B3Challenge.Business/B3Challenge.Business.csproj", "Infra/B3Challenge.Business/"]
COPY ["Infra/B3Challenge.Domain/B3Challenge.Domain.csproj", "Infra/B3Challenge.Domain/"]
COPY ["Infra/B3Challenge.Rabbit/B3Challenge.Rabbit.csproj", "Infra/B3Challenge.Rabbit/"]
COPY ["Infra/B3Challenge.Repository/B3Challenge.Repository.csproj", "Infra/B3Challenge.Repository/"]
RUN dotnet restore "B3Challenge.Task.Worker/B3Challenge.Task.Worker.csproj"
COPY . .
WORKDIR "/src/B3Challenge.Task.Worker"
RUN dotnet build "B3Challenge.Task.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "B3Challenge.Task.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "B3Challenge.Task.Worker.dll"]