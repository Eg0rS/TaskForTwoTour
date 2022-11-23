FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["tour2/tour2.csproj", "tour2/"]
RUN dotnet restore "tour2/tour2.csproj"
COPY . .
WORKDIR "/src/tour2"
RUN dotnet build "tour2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "tour2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "tour2.dll"]
