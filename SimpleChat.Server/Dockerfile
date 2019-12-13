FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["SimpleChat.Server/SimpleChat.Server.csproj", "SimpleChat.Server/"]
RUN dotnet restore "SimpleChat.Server/SimpleChat.Server.csproj"
COPY . .
WORKDIR "/src/SimpleChat.Server"
RUN dotnet build "SimpleChat.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleChat.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleChat.Server.dll"]