#!/bin/bash

# Load environment variables from .env file if it exists
if [ -f ".devcontainer/.env" ]; then
    echo "Loading environment variables from .devcontainer/.env file..."
    export $(grep -v '^#' .devcontainer/.env | xargs)
    echo "Environment variables loaded successfully"
else
    echo "No .env file found in .devcontainer directory"
    echo "Please create .devcontainer/.env with GITHUB_USERNAME and GITHUB_TOKEN"
    exit 1
fi

# Check if required environment variables are set
if [ -z "$GITHUB_USERNAME" ] || [ -z "$GITHUB_TOKEN" ]; then
    echo "Error: GITHUB_USERNAME and GITHUB_TOKEN must be set in .devcontainer/.env"
    exit 1
fi

echo "GITHUB_USERNAME: $GITHUB_USERNAME"

# Add or update the source in the global NuGet configuration
if dotnet nuget list source | grep -q "monster-train-packages"; then
    echo "monster-train-packages package source already exists globally, updating credentials..."
    dotnet nuget update source monster-train-packages \
        --username $GITHUB_USERNAME \
        --password $GITHUB_TOKEN \
        --store-password-in-clear-text
else
    echo "Adding monster-train-packages package source to global configuration..."
    dotnet nuget add source --username $GITHUB_USERNAME \
        --name monster-train-packages \
        --password $GITHUB_TOKEN \
        --store-password-in-clear-text \
        "https://nuget.pkg.github.com/Monster-Train-2-Modding-Group/index.json"
fi

echo "Setup completed successfully!"

