#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AutomationProvider.Api/AutomationProvider.Api.csproj", "src/AutomationProvider.Api/"]
COPY ["src/AutomationProvider.Application/AutomationProvider.Application.csproj", "src/AutomationProvider.Application/"]
COPY ["src/AutomationProvider.Contracts/AutomationProvider.Contracts.csproj", "src/AutomationProvider.Contracts/"]
COPY ["src/AutomationProvider.Domain/AutomationProvider.Domain.csproj", "src/AutomationProvider.Domain/"]
COPY ["src/AutomationProvider.Infrastructure/AutomationProvider.Infrastructure.csproj", "src/AutomationProvider.Infrastructure/"]
RUN dotnet restore "./src/AutomationProvider.Api/./AutomationProvider.Api.csproj"
COPY . .
WORKDIR "/src/src/AutomationProvider.Api"
RUN dotnet build "./AutomationProvider.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AutomationProvider.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutomationProvider.Api.dll"]