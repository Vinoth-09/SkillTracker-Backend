#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Profile/Profile.API/Profile.API.csproj", "Profile/Profile.API/"]
COPY ["Profile/Profile.Application/Profile.Application.csproj", "Profile/Profile.Application/"]
COPY ["Profile/Profile.Domain/Profile.Domain.csproj", "Profile/Profile.Domain/"]
COPY ["Common/SkillTracker.Entities/SkillTracker.Entities.csproj", "Common/SkillTracker.Entities/"]
COPY ["Common/EventBus.Messaging/EventBus.Messaging.csproj", "Common/EventBus.Messaging/"]
COPY ["Profile/Profile.Infrastructure/Profile.Infrastructure.csproj", "Profile/Profile.Infrastructure/"]
RUN dotnet restore "Profile/Profile.API/Profile.API.csproj"
COPY . .
WORKDIR "/src/Profile/Profile.API"
RUN dotnet build "Profile.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Profile.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Profile.API.dll"]