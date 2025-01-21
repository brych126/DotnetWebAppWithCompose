## Linux containers on Windows host
1. Clone the repo and navigate to its root folder
```sh
cd .\DotnetWebAppWithCompose\
```
2. Generate cert and configure local machine:
```sh
mkdir -p $env:APPDATA\ASP.NET\Https
dotnet dev-certs https -ep $env:APPDATA\ASP.NET\Https\WebFrontEnd.pfx -p <CREDENTIAL_PLACEHOLDER>
dotnet dev-certs https --trust
```
3. Configure application secrets, for the certificate:
```sh
dotnet user-secrets -p .\WebFrontEnd\WebFrontEnd.csproj set "Kestrel:Certificates:Development:Password" <CREDENTIAL_PLACEHOLDER>
```
> [!NOTE]
> The password must match the password used for the certificate.
5. Start services using `docker compose`:
```sh
docker compose up -d
```

## Linux containers on Linux host
1. Clone the repo and navigate to its root folder
```sh
cd DotnetWebAppWithCompose/
```
2. Create a certificate directory with appropriate permissions:
```sh
mkdir -p -m 755 ~/.aspnet/https
mkdir -p -m 755 ~/.microsoft/usersecrets
```
3. Generate cert and configure local machine:
```sh
dotnet dev-certs https -ep ~/.aspnet/https/WebFrontEnd.pfx -p <CREDENTIAL_PLACEHOLDER>
dotnet dev-certs https --trust
```
4. Configure application secrets, for the certificate:
```sh
dotnet user-secrets -p ./WebFrontEnd/WebFrontEnd.csproj set "Kestrel:Certificates:Development:Password" <CREDENTIAL_PLACEHOLDER>
```
> [!NOTE]
> The password must match the password used for the certificate.
5. Change permissions for created files:
```sh
sudo chmod 755 -R  ~/.aspnet/
sudo chmod 755 -R  ~/.microsoft/
```
6.  Update `.env` file by replacing old value for `COMPOSE_FILE` env variable with new one `COMPOSE_FILE=docker-compose.yml:docker-compose.override.yml:docker-compose.healthcheck.yml:docker-compose-linux.override.yml`.
`docker-compose-linux.override.yml` file has been added to the list of docker compose yaml files. It is needed because it contains overrides for certificate mount paths for linux host.
7. Start services using `docker compose`:
```sh
docker compose up -d
```

## Useful References
- https://learn.microsoft.com/en-us/visualstudio/containers/tutorial-multicontainer?view=vs-2022
- https://learn.microsoft.com/en-us/visualstudio/containers/container-certificate-management?view=vs-2022#certificates
- https://learn.microsoft.com/en-us/visualstudio/containers/docker-compose-properties?view=vs-2022#overriding-visual-studios-docker-compose-configuration
- https://learn.microsoft.com/en-us/visualstudio/containers/container-volume-mapping?view=vs-2022#volume-mounts-in-visual-studio-container-images
- https://learn.microsoft.com/en-us/visualstudio/containers/container-debug-customization?view=vs-2022
   
