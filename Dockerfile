#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Simem.AppCom.Datos.Servicios/Simem.AppCom.Datos.Servicios.csproj", "Simem.AppCom.Datos.Servicios/"]
COPY ["Simem.AppCom.Datos.Core/Simem.AppCom.Datos.Core.csproj", "Simem.AppCom.Datos.Core/"]
COPY ["Simem.AppCom.Base.Interfaces/Simem.AppCom.Base.Interfaces.csproj", "Simem.AppCom.Base.Interfaces/"]
COPY ["Simem.AppCom.Datos.Dominio/Simem.AppCom.Datos.Dominio.csproj", "Simem.AppCom.Datos.Dominio/"]
COPY ["SImem.AppCom.Datos.Dto/SImem.AppCom.Datos.Dto.csproj", "SImem.AppCom.Datos.Dto/"]
COPY ["Simem.AppCom.Datos.Repo/Simem.AppCom.Datos.Repo.csproj", "Simem.AppCom.Datos.Repo/"]
COPY ["Mapeos/Mapeos.csproj", "Mapeos/"]
COPY ["Simem.AppCom.Base.Repo/Simem.AppCom.Base.Repo.csproj", "Simem.AppCom.Base.Repo/"]
COPY ["Simem.AppCom.Base.Utils/Simem.AppCom.Base.Utils.csproj", "Simem.AppCom.Base.Utils/"]
RUN dotnet restore "Simem.AppCom.Datos.Servicios/Simem.AppCom.Datos.Servicios.csproj"
COPY . .
WORKDIR "/src/Simem.AppCom.Datos.Servicios"
RUN dotnet build "Simem.AppCom.Datos.Servicios.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Simem.AppCom.Datos.Servicios.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .

ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true \
    ASPNETCORE_ENVIRONMENT=Development \
    AzureWebJobsStorage="" \
    FUNCTIONS_WORKER_RUNTIME=dotnet \
    AzureStorage=AzureStorage \
    StorageContainer=StorageContainerSimem \
    PQEEndpoint=PQEEndpoint \
    user_pqr=user-pqr \
    pass_pqr=pass-pqr \
    SimemConnection=SimemConnection \
    AzureKeyVaultUri=https://kvsimemprd01.vault.azure.net/ \
    JwtKey=JwtKey \
    ClBuzonPQR=ClBuzonPQR \
    BuzonPQR=BuzonPQR \
    urlCrmLogin=urlCrmLogin \
    urlCrmData=urlCrmData \
    crmStatus=crmStatus \
    crmModuleName=crmModuleName \
    crmWindow=crmWindow \
    crmWorkTeam=crmWorkTeam \
    crmPriority=crmPriority \
    secretKeyJWT=secretKeyJWT \
    clientId=ZQA5ADQANAA2AGIANwAyAC0AOAA3AGMAYgAtADQANQAyADEALQA4ADkAYgBmAC0AZgAwAGYANQBhADAAYwA2AGEAZQAzADUA \
    clientSecret=VgBHAGgAOABRAH4AVwBQAH4AQgBSAFQAUABqADgARQBNAHIAWABuAG0AZABIAHoAZQBiAEkAVgBCAEUAagBPAEMAVgBqAFgASQBjAFAAUgA= \
    tenantId=YwA5ADgAMABlADQAMQAwAC0AMABiADUAYwAtADQAOABiAGMALQBiAGQAMQBhAC0AOABiADkAMQBjAGEAYgBjADgANABiAGMA \
    Pipeline=false \
    ContactanosFalla=ContactanosFalla \
    DocumentalUser=DocumentalUser \
    DocumentalPass=DocumentalPass \
    DocumentalUrl=DocumentalUrl \
    DocumentalClaseDocumental=DocumentalClaseDocumental \
    DocumentalDebeResponder=DocumentalDebeResponder \
    DocumentalDependencia=DocumentalDependencia \
    DocumentalMedioRecibido=DocumentalMedioRecibido \
    UrlContactanos=UrlContactanos

ENTRYPOINT ["dotnet", "Simem.AppCom.Datos.Servicios.dll"]