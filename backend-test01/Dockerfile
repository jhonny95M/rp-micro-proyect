FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/test01.Api/test01.Api.csproj", "src/test01.Api/"]
COPY ["src/test01.App/test01.Application.csproj", "src/test01.Application/"]
COPY ["src/test01.Database/test01.Database.csproj", "src/test01.Database/"]
COPY ["src/test01.Domain/test01.Domain.csproj", "src/test01.Domain/"]
COPY ["src/test01.Internal.Contract/test01.Internal.Contract.csproj", "src/test01.Internal.Contract/"]
COPY ["src/test01.Persistence/test01.Persistence.csproj", "src/test01.Persistence/"]
COPY ["src/test01.Service/test01.Service.csproj", "src/test01.Service/"]
ARG PAT
ARG ARTIFACT_URL


ENV NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED true
ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS {\"endpointCredentials\": [{\"endpoint\":\"${ARTIFACT_URL}\", \"username\":\"ArtifactsDocker\", \"password\":\"${PAT}\"}]}
ENV SERVICE_NAME_DOCKER="my-service"

RUN wget -qO- https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh | sh


RUN dotnet restore -s ${ARTIFACT_URL} -s "https://api.nuget.org/v3/index.json" "src/test01.Api/test01.Api.csproj"

COPY . .

FROM build AS publish

RUN dotnet tool install swashbuckle.aspnetcore.cli --version 6.6.1 --global

ENV PATH="${PATH}:/root/.dotnet/tools"

ENV COLLECTOR_ENDPOINT="http://34.85.198.247:4317"
ENV SERVICE_NAME="Integration.Portal.API"
ENV SLEEP_DURATION_PROVIDER="10"
ENV RETRY_COUNT="4"

RUN dotnet build "src/test01.Api/test01.Api.csproj" -c Release -o /app

RUN dotnet publish "src/test01.Api/test01.Api.csproj" -c Release -o /app

ENV ASPNETCORE_ENVIRONMENT "Swagger"
RUN dotnet build "src/test01.Api/test01.Api.csproj" -c Swagger -o /app-swagger

RUN addgroup --gid 1001 $SERVICE_NAME_DOCKER && \
    adduser  --ingroup $SERVICE_NAME_DOCKER --shell /bin/false --disabled-password --uid 1001 $SERVICE_NAME_DOCKER && \
    chown $SERVICE_NAME_DOCKER:$SERVICE_NAME_DOCKER /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
USER $SERVICE_NAME_DOCKER
ENV ASPNETCORE_ENVIRONMENT "Production"
ENTRYPOINT ["dotnet", "test01.Api.dll"]