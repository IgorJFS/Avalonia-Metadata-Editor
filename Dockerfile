# ================= Build Stage =================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY Docs_Metadata_Editor.csproj .
RUN dotnet restore

# Copy all source files
COPY . .

# Publish self-contained, single-file executables for major platforms
# 1. Windows x64
RUN dotnet publish Docs_Metadata_Editor.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o /app/publish/windows

# 2. Linux x64
RUN dotnet publish Docs_Metadata_Editor.csproj -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:PublishReadyToRun=true -o /app/publish/linux

# 3. macOS x64 (Standard C# publish targets macOS Intel/Apple Silicon platforms)
RUN dotnet publish Docs_Metadata_Editor.csproj -c Release -r osx-x64 --self-contained true -p:PublishSingleFile=true -o /app/publish/macos

# ================= Runtime Stage =================
# noble represents Ubuntu 24.04 LTS which has up-to-date dependencies for Avalonia UI.
FROM mcr.microsoft.com/dotnet/runtime:10.0-noble AS runtime
WORKDIR /app

# Install native dependencies required for Avalonia UI (X11 & OpenGL support)
RUN apt-get update && apt-get install -y --no-install-recommends \
    libx11-6 \
    libegl1 \
    libgl1 \
    libglib2.0-0 \
    libfontconfig1 \
    libxrandr2 \
    libxcursor1 \
    libxinerama1 \
    libxi6 \
    && rm -rf /var/lib/apt/lists/*

# Copy the Linux build to the runtime container
COPY --from=build /app/publish/linux .

# Entrypoint to run the application inside Docker (X11 display redirection required)
ENTRYPOINT ["./Docs_Metadata_Editor"]
