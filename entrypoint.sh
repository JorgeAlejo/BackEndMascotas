#!/bin/bash
set -e

echo "Running migrations..."
dotnet ef database update

echo "Starting the API..."
dotnet BackEndMascotas.dll
