# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["INVENTORY MANAGEMENT SYSTEM.csproj", "./"]
RUN dotnet restore "./INVENTORY MANAGEMENT SYSTEM.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "INVENTORY MANAGEMENT SYSTEM.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "INVENTORY MANAGEMENT SYSTEM.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Set the environment variable
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "INVENTORY MANAGEMENT SYSTEM.dll"]
