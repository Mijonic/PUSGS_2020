#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["SmartEnergy.DocumentExtensions/SmartEnergy.DocumentExtensions.csproj", "SmartEnergy.DocumentExtensions/"]
RUN dotnet restore "SmartEnergy.DocumentExtensions/SmartEnergy.DocumentExtensions.csproj"
COPY . .
WORKDIR "/src/SmartEnergy.DocumentExtensions"
RUN dotnet build "SmartEnergy.DocumentExtensions.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartEnergy.DocumentExtensions.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartEnergy.DocumentExtensions.dll"]