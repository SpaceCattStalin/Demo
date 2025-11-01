#!/bin/sh
set -e

echo "[entrypoint] Starting SQL Server..."
/opt/mssql/bin/sqlservr &

echo "[entrypoint] Waiting for SQL Server to be ready..."
for i in {1..60}; do
  if /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "${MSSQL_SA_PASSWORD}" -C -Q "SELECT 1" >/dev/null 2>&1; then
    echo "[entrypoint] SQL Server is ready."
    break
  fi
  echo "  ... still starting ($i)"
  sleep 2
done

echo "[entrypoint] Starting .NET API..."
exec dotnet API.dll
