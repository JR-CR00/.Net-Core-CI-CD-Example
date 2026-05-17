FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["DotNetApiExampleCICD/DotNetApiExampleCICD.csproj", "DotNetApiExampleCICD/"]
COPY ["DotNetApiExampleCICD.Tests/DotNetApiExampleCICD.Tests.csproj", "DotNetApiExampleCICD.Tests/"]

RUN dotnet restore "DotNetApiExampleCICD/DotNetApiExampleCICD.csproj"

COPY . .

RUN dotnet test "DotNetApiExampleCICD.Tests/DotNetApiExampleCICD.Tests.csproj" \
    --no-restore \
    --verbosity normal

RUN dotnet publish "DotNetApiExampleCICD/DotNetApiExampleCICD.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

RUN mkdir -p /app/wwwroot

EXPOSE 8080

ENTRYPOINT ["dotnet", "DotNetApiExampleCICD.dll"]