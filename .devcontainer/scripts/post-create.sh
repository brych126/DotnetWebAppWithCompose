#!/bin/bash

# Create a certificate directory with appropriate permissions
mkdir -p -m 755 ~/.aspnet/https
mkdir -p -m 755 ~/.microsoft/usersecrets

# Generate cert and configure local machine
dotnet dev-certs https -ep ~/.aspnet/https/WebFrontEnd.pfx -p Password1
dotnet dev-certs https --trust

# Configure application secrets, for the certificate:
dotnet user-secrets -p ./WebFrontEnd/WebFrontEnd.csproj set "Kestrel:Certificates:Development:Password" Password1

# Change permissions for created files
sudo chmod 755 -R  ~/.aspnet/
sudo chmod 755 -R  ~/.microsoft/