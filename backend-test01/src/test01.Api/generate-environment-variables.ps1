# -----------------------------------------------------------------------------
# Script Name: generate-environment-variables.ps1
# Description: Generate environment variables file for the API and Migration project.
# -----------------------------------------------------------------------------

$envAPIContent = @"
COLLECTOR-ENDPOINT=http://34.85.231.67:4317
SERVICE-NAME=test01.Test.API
TESTAPP-DB=server=localhost;port=5432;User Id=postgres;Password=123456;Database=playground;
TIMEZONE=SA Pacific Standard Time
HEALTHCHECK-PORT=7185
TEST-BASEURI=https://dummyjson.com
SLEEP-DURATION-PROVIDER=10
IDENTITY-AUTHORITY=https://api.realplazalabs.com/v1/identity
IDENTITY-API-AUDIENCE=ms-rp-architecture
IDENTITY-HTTPS-METADATA=true
IDENTITY-CLIENT-ID=realplaza.swagger
IDENTITY-CLIENT-SECRET=71D58FDC-8467-4D61-9010-DCE401F16A78
IDENTITY-API-RESOURCE=ms-rp-architecture ms-rp-security
MICROSERVICE-NAME=Microservice Test
MICROSERVICE-DESCRIPTION=Description example
ORIGINS_CONFIGURATION=http://localhost,https://trustedwebsite2.com
"@


$envAPIContent | Out-File -FilePath ".env" -Encoding UTF8 -Force