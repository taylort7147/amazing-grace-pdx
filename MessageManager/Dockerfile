# Use .NET 9.0 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5212

# Set environment variable for the app to listen on the exposed port
ENV ASPNETCORE_URLS=http://+:5212
# Set up app data directory and permissions
RUN mkdir -p /app/data
RUN chown www-data:www-data /app/data
VOLUME /app/data

USER www-data

###############################################################################
# Build stage
###############################################################################

# Use .NET 9.0 SDK image for building the app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src

# Install OpenJDK 17
RUN apt-get update && apt-get install -y openjdk-17-jdk && \
    apt-get clean && rm -rf /var/lib/apt/lists/*

# (Optional) Verify Java is installed
RUN java -version

# Copy the entire source code to the image
WORKDIR "/src/MessageManager"
COPY . .

###############################################################################
# Build BibleReferenceParser
###############################################################################

# Set up script dependency
RUN chmod +x BibleReferenceParser/scripts/build_grammar.sh

# Ensure the *.csproj are copied first for caching dependencies
COPY ["BibleReferenceParser/BibleReferenceParser.csproj", "BibleReferenceParser/"]

# Restore the project dependencies
RUN dotnet restore "BibleReferenceParser/BibleReferenceParser.csproj"

# Build
RUN dotnet build "BibleReferenceParser/BibleReferenceParser.csproj" -c $configuration -o /app/build

###############################################################################
# Build MessageManager
###############################################################################

# Ensure the *.csproj are copied first for caching dependencies
COPY ["MessageManager/MessageManager.csproj", "MessageManager/"]
COPY . .

# Restore the project dependencies
RUN dotnet restore "MessageManager/MessageManager.csproj"

# Build
RUN dotnet build "MessageManager/MessageManager.csproj" -c $configuration -o /app/build

###############################################################################
# Publish
###############################################################################

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "MessageManager/MessageManager.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# Final image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY MessageManager/appsettings.Docker.json appsettings.json

# Set the entry point for the application
ENTRYPOINT ["dotnet", "MessageManager.dll"]
