#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/POS.Api/POS.Api.csproj", "src/POS.Api/"]
COPY ["src/POS.Application/POS.Application.csproj", "src/POS.Application/"]
COPY ["src/POS.Domain/POS.Domain.csproj", "src/POS.Domain/"]
COPY ["src/POS.Infraestructure/POS.Infraestructure.csproj", "src/POS.Infraestructure/"]
COPY ["src/POS.Utilities/POS.Utilities.csproj", "src/POS.Utilities/"]
RUN dotnet restore "src/POS.Api/POS.Api.csproj"
COPY . .
WORKDIR "/src/src/POS.Api"
RUN dotnet build "POS.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "POS.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN ln -fs /usr/share/zoneinfo/America/Lima /etc/localtime
RUN dpkg-reconfigure --frontend noninteractive tzdata

ENTRYPOINT ["dotnet", "POS.Api.dll"]