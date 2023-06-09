FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["Consid.Logger.Application/Consid.Logger.Application.csproj", "Consid.Logger.Application/"] 
COPY ["Consid.Logger.Domain/Consid.Logger.Domain.csproj", "Consid.Logger.Domain/"] 
COPY ["Consid.Logger.Host/Consid.Logger.Host.csproj", "Consid.Logger.Host/"] 
COPY ["Consid.Logger.Api/Consid.Logger.Api.csproj", "Consid.Logger.Api/"] 

RUN dotnet restore "Consid.Logger.Host/Consid.Logger.Host.csproj"
COPY . .
WORKDIR "Consid.Logger.Host"
RUN dotnet build "Consid.Logger.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Consid.Logger.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Consid.Logger.Host.dll"]
