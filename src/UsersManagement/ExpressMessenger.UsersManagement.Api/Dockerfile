FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UsersManagement/ExpressMessenger.UsersManagement.Api/ExpressMessenger.UsersManagement.Api.csproj", "UsersManagement/ExpressMessenger.UsersManagement.Api/"]
COPY ["Common/ExpressMessenger.Common.Api/ExpressMessenger.Common.Api.csproj", "Common/ExpressMessenger.Common.Api/"]
COPY ["Common/ExpressMessenger.Common.Application/ExpressMessenger.Common.Application.csproj", "Common/ExpressMessenger.Common.Application/"]
COPY ["Common/ExpressMessenger.Common.Domain/ExpressMessenger.Common.Domain.csproj", "Common/ExpressMessenger.Common.Domain/"]
COPY ["UsersManagement/ExpressMessenger.UsersManagement.Application/ExpressMessenger.UsersManagement.Application.csproj", "UsersManagement/ExpressMessenger.UsersManagement.Application/"]
COPY ["UsersManagement/ExpressMessenger.UsersManagement.Domain/ExpressMessenger.UsersManagement.Domain.csproj", "UsersManagement/ExpressMessenger.UsersManagement.Domain/"]
COPY ["UsersManagement/ExpressMessenger.UsersManagement.Infrastructure/ExpressMessenger.UsersManagement.Infrastructure.csproj", "UsersManagement/ExpressMessenger.UsersManagement.Infrastructure/"]
COPY ["Common/ExpressMessenger.Common.Infrastructure/ExpressMessenger.Common.Infrastructure.csproj", "Common/ExpressMessenger.Common.Infrastructure/"]
COPY ["SharedKernel/ExpressMessenger.SharedKernel.ApiContracts/ExpressMessenger.SharedKernel.ApiContracts.csproj", "SharedKernel/ExpressMessenger.SharedKernel.ApiContracts/"]
RUN dotnet restore "UsersManagement/ExpressMessenger.UsersManagement.Api/ExpressMessenger.UsersManagement.Api.csproj"
COPY . .
WORKDIR "/src/UsersManagement/ExpressMessenger.UsersManagement.Api"
RUN dotnet build "ExpressMessenger.UsersManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ExpressMessenger.UsersManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpressMessenger.UsersManagement.Api.dll"]
