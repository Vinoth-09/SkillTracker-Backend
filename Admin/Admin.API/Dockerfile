FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Admin/Admin.API/SkillTracker.Admin.API.csproj", "Admin/Admin.API/"]
COPY ["Admin/Admin.Infrastructure/Admin.Infrastructure.csproj", "Admin/Admin.Infrastructure/"]
COPY ["Admin/Admin.Application/Admin.Application.csproj", "Admin/Admin.Application/"]
COPY ["Common/SkillTracker.Entities/SkillTracker.Entities.csproj", "Common/SkillTracker.Entities/"]
COPY ["Admin/Admin.Domain/Admin.Domain.csproj", "Admin/Admin.Domain/"]
COPY ["Common/EventBus.Messaging/EventBus.Messaging.csproj", "Common/EventBus.Messaging/"]
RUN dotnet restore "Admin\Admin.API\SkillTracker.Admin.API.csproj"
COPY . .
WORKDIR "/src/Admin/Admin.API"
RUN dotnet build "SkillTracker.Admin.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SkillTracker.Admin.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkillTracker.Admin.API.dll"]
