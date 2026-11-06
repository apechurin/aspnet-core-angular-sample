#!/bin/bash

set -e

RID=linux-x64
# RID=win-x64
TargetFramework=net8.0

ScriptDir=$(cd -- "$(dirname -- "$0")" &>/dev/null && pwd)

if [ -d publish ]; then rm -Rf publish; fi
mkdir -p publish

if [ -d bin ]; then rm -Rf bin; fi

dotnet publish -c Release --self-contained -r ${RID}
cp -r bin/Release/${TargetFramework}/${RID}/publish/* publish

rm -rf publish/appsettings*.json

find publish -name ".gitkeep" -type f -delete
