FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Studio/Studio.csproj", "Studio/"]
RUN dotnet restore "Studio/Studio.csproj"
COPY . .
WORKDIR "/src/Studio"
RUN dotnet build "Studio.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Studio.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Studio.dll"]
