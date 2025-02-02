FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Chatting/ExpressMessenger.Chatting.Api/ExpressMessenger.Chatting.Api.csproj", "Chatting/ExpressMessenger.Chatting.Api/"]
COPY ["Common/ExpressMessenger.Common.Api/ExpressMessenger.Common.Api.csproj", "Common/ExpressMessenger.Common.Api/"]
COPY ["Common/ExpressMessenger.Common.Application/ExpressMessenger.Common.Application.csproj", "Common/ExpressMessenger.Common.Application/"]
COPY ["Common/ExpressMessenger.Common.Domain/ExpressMessenger.Common.Domain.csproj", "Common/ExpressMessenger.Common.Domain/"]
COPY ["SharedKernel/ExpressMessenger.SharedKernel.ApiContracts/ExpressMessenger.SharedKernel.ApiContracts.csproj", "SharedKernel/ExpressMessenger.SharedKernel.ApiContracts/"]
COPY ["Chatting/ExpressMessenger.Chatting.Application/ExpressMessenger.Chatting.Application.csproj", "Chatting/ExpressMessenger.Chatting.Application/"]
COPY ["Chatting/ExpressMessenger.Chatting.Domain/ExpressMessenger.Chatting.Domain.csproj", "Chatting/ExpressMessenger.Chatting.Domain/"]
COPY ["Common/ExpressMessenger.Common.Infrastructure/ExpressMessenger.Common.Infrastructure.csproj", "Common/ExpressMessenger.Common.Infrastructure/"]
COPY ["Chatting/ExpressMessenger.Chatting.Infrastructure/ExpressMessenger.Chatting.Infrastructure.csproj", "Chatting/ExpressMessenger.Chatting.Infrastructure/"]
RUN dotnet restore "Chatting/ExpressMessenger.Chatting.Api/ExpressMessenger.Chatting.Api.csproj"
COPY . .
WORKDIR "/src/Chatting/ExpressMessenger.Chatting.Api"
RUN dotnet build "ExpressMessenger.Chatting.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ExpressMessenger.Chatting.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpressMessenger.Chatting.Api.dll"]
