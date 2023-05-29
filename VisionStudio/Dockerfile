FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VisionStudio.csproj", "./"]
RUN dotnet restore "VisionStudio.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "VisionStudio.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VisionStudio.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VisionStudio.dll"]
