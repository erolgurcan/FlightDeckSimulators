#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["flight_data_server/flight_data_server.csproj", "flight_data_server/"]
RUN dotnet restore "flight_data_server/flight_data_server.csproj"
COPY . .
WORKDIR "/src/flight_data_server"
RUN dotnet build "flight_data_server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "flight_data_server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "flight_data_server.dll"]