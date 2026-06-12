# build-releases.ps1
# PowerShell script to build multi-platform executables inside Docker and extract them locally.

# Stop on errors
$ErrorActionPreference = "Stop"

Write-Host "==================================================" -ForegroundColor Cyan
Write-Host "Building Docs Metadata Editor Releases inside Docker" -ForegroundColor Cyan
Write-Host "==================================================" -ForegroundColor Cyan

# 1. Clean previous release folders
if (Test-Path ./releases) {
    Write-Host "Cleaning old releases..." -ForegroundColor Gray
    Remove-Item -Recurse -Force ./releases
}
New-Item -ItemType Directory -Path ./releases | Out-Null

# 2. Build the Docker image up to the 'build' stage
Write-Host "Building Docker build-container..." -ForegroundColor Yellow
docker build --target build -t docs-metadata-editor-build .

# 3. Create a temporary container to extract files
Write-Host "Creating temporary container..." -ForegroundColor Yellow
$containerName = "docs-metadata-editor-temp-build"
# Clean up any leftover container with the same name
docker rm -f $containerName 2>$null | Out-Null
docker create --name $containerName docs-metadata-editor-build | Out-Null

# 4. Copy the single-file self-contained executables to the local filesystem
Write-Host "Extracting published binaries to ./releases..." -ForegroundColor Yellow

# Copy Windows executable
docker cp "$($containerName):/app/publish/windows/Docs_Metadata_Editor.exe" ./releases/Docs_Metadata_Editor_Windows_x64.exe

# Copy Linux executable
docker cp "$($containerName):/app/publish/linux/Docs_Metadata_Editor" ./releases/Docs_Metadata_Editor_Linux_x64

# Copy macOS executable
docker cp "$($containerName):/app/publish/macos/Docs_Metadata_Editor" ./releases/Docs_Metadata_Editor_macOS_x64

# 5. Clean up container
Write-Host "Cleaning up container..." -ForegroundColor Yellow
docker rm $containerName | Out-Null

Write-Host "==================================================" -ForegroundColor Green
Write-Host "Success! The following files are ready in ./releases/:" -ForegroundColor Green
Get-ChildItem ./releases | ForEach-Object { Write-Host "  - $($_.Name)" -ForegroundColor Green }
Write-Host "==================================================" -ForegroundColor Green
