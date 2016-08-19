#!/bin/bash
rm -rf ./docker/bin
mkdir -p ./docker/bin
cd ../src
dotnet restore
cd eggsolve.host
dotnet publish
cp -R ./bin/Debug/netcoreapp1.0/publish/. ../../build/docker/bin

