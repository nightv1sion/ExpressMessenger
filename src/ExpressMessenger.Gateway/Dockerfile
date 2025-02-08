FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ExpressMessenger.Gateway/ExpressMessenger.Gateway.csproj", "ExpressMessenger.Gateway/"]
COPY ["Common/ExpressMessenger.Common.Api/ExpressMessenger.Common.Api.csproj", "Common/ExpressMessenger.Common.Api/"]
COPY ["Common/ExpressMessenger.Common.Application/ExpressMessenger.Common.Application.csproj", "Common/ExpressMessenger.Common.Application/"]
COPY ["Common/ExpressMessenger.Common.Domain/ExpressMessenger.Common.Domain.csproj", "Common/ExpressMessenger.Common.Domain/"]
RUN dotnet restore "ExpressMessenger.Gateway/ExpressMessenger.Gateway.csproj"
COPY . .
WORKDIR "/src/ExpressMessenger.Gateway"
RUN dotnet build "ExpressMessenger.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ExpressMessenger.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpressMessenger.Gateway.dll"]
