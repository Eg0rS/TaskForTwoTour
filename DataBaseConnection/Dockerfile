FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DataBaseConnection/DataBaseConnection.csproj", "DataBaseConnection/"]
RUN dotnet restore "DataBaseConnection/DataBaseConnection.csproj"
COPY . .
WORKDIR "/src/DataBaseConnection"
RUN dotnet build "DataBaseConnection.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataBaseConnection.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataBaseConnection.dll"]
