#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CancunHotelAPI.csproj", "."]
RUN dotnet restore "./CancunHotelAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CancunHotelAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CancunHotelAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CancunHotelAPI.dll"]