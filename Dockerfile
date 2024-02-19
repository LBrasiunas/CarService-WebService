FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CarServiceApi/CarServiceApi.csproj", "CarServiceApi/"]
RUN dotnet restore "CarServiceApi/CarServiceApi.csproj"
COPY . .
WORKDIR "/src/CarServiceApi"
RUN dotnet build "CarServiceApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarServiceApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarServiceApi.dll"]