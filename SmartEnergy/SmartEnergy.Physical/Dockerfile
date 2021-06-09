#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["SmartEnergy.Physical/SmartEnergy.Physical.csproj", "SmartEnergy.Physical/"]
COPY ["SmartEnergy.Contract/SmartEnergy.Contract.csproj", "SmartEnergy.Contract/"]
RUN dotnet restore "SmartEnergy.Physical/SmartEnergy.Physical.csproj"
COPY . .
WORKDIR "/src/SmartEnergy.Physical"
RUN dotnet build "SmartEnergy.Physical.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartEnergy.Physical.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartEnergy.Physical.dll"]