# Use the official .NET 10 runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Product.Catalog.csproj", "."]
RUN dotnet restore "./Product.Catalog.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Product.Catalog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Product.Catalog.csproj" -c Release -o /app/publish

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.Catalog.dll"]