FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApp/ExpressMessenger.BlazorWebApp/ExpressMessenger.BlazorWebApp.csproj", "WebApp/ExpressMessenger.BlazorWebApp/"]
COPY ["SharedKernel/ExpressMessenger.SharedKernel.ApiContracts/ExpressMessenger.SharedKernel.ApiContracts.csproj", "SharedKernel/ExpressMessenger.SharedKernel.ApiContracts/"]
RUN dotnet restore "WebApp/ExpressMessenger.BlazorWebApp/ExpressMessenger.BlazorWebApp.csproj"
COPY . .
WORKDIR "/src/WebApp/ExpressMessenger.BlazorWebApp"
RUN dotnet build "ExpressMessenger.BlazorWebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ExpressMessenger.BlazorWebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpressMessenger.BlazorWebApp.dll"]
