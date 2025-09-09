# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Install OpenJDK (example for Ubuntu-based .NET SDK image)
RUN apt-get update && \
    apt-get install -y openjdk-17-jdk && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Verify java installation
RUN java -version

# Copy everything (solution + projects)
COPY . .

# Restore and build the whole solution
RUN dotnet restore ./BibleReferenceParser.sln
RUN dotnet publish ./BibleReferenceParser.sln -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Run the service
ENTRYPOINT ["dotnet", "BibleReferenceParserService.dll"]
