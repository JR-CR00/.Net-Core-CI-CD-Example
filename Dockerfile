FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["DotNetApiExampleCICD/DotNetApiExampleCICD.csproj", "DotNetApiExampleCICD/"]
COPY ["DotNetApiExampleCICD.Tests/DotNetApiExampleCICD.Tests.csproj", "DotNetApiExampleCICD.Tests/"]

RUN dotnet restore "DotNetApiExampleCICD/DotNetApiExampleCICD.csproj"

COPY . .

RUN dotnet test "DotNetApiExampleCICD.Tests/DotNetApiExampleCICD.Tests.csproj" \
    --no-restore \
    --verbosity normal
