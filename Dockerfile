# Use the official Microsoft .NET Core SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution file and restore dependencies
COPY . ./
RUN dotnet publish src/LifeMastery.API/LifeMastery.API.csproj -c release -o /app/publish

# Use the official Microsoft .NET Core runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "LifeMastery.API.dll"]