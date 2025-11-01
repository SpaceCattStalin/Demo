# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files (paths are relative to root of context)
COPY ./API/API.csproj ./API/
COPY ./Services/Services.csproj ./Services/
COPY ./Repositories/Repositories.csproj ./Repositories/

RUN dotnet restore ./API/API.csproj

# Copy full source
COPY . .

# Publish
WORKDIR /src/API
RUN dotnet publish -c Release -o /out/publish


# =========================
# Final stage (SQL + Runtime)
# =========================
FROM mcr.microsoft.com/mssql/server:2022-latest AS final

USER root

ENV ACCEPT_EULA=Y \
    MSSQL_PID=Developer \
    MSSQL_SA_PASSWORD=YourStrong(!)Passw0rd

RUN apt-get update && apt-get install -y wget apt-transport-https gnupg && \
    wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O /tmp/packages-microsoft-prod.deb && \
    dpkg -i /tmp/packages-microsoft-prod.deb && \
    apt-get update && apt-get install -y dotnet-runtime-8.0 mssql-tools18 unixodbc-dev && \
    ln -s /opt/mssql-tools18/bin/sqlcmd /usr/bin/sqlcmd && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=build /out/publish .

COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

USER mssql

ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080}

EXPOSE 8080 1433

ENTRYPOINT ["/entrypoint.sh"]
