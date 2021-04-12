#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Fupi-KeyGenService/Fupi-KeyGenService.csproj", "Fupi-KeyGenService/"]
RUN dotnet restore "Fupi-KeyGenService/Fupi-KeyGenService.csproj"
COPY . .
WORKDIR "/src/Fupi-KeyGenService"
RUN dotnet build "Fupi-KeyGenService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fupi-KeyGenService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fupi-KeyGenService.dll"]