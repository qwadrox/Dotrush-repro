#!/usr/bin/env bash
#
# Generated by: https://openapi-generator.tech
#

dotnet restore src/ProductInventoryApi/ && \
    dotnet build src/ProductInventoryApi/ && \
    echo "Now, run the following to start the project: dotnet run -p src/ProductInventoryApi/ProductInventoryApi.csproj --launch-profile web"
