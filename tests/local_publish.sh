#! /bin/sh
set -e

FRAMEWORK=$1
if [ -z "${FRAMEWORK}" ]; then FRAMEWORK="net9.0"; fi

VERSION="99.99.99"

sed -i "s/<VersionPrefix>[0-9.]*<\/VersionPrefix>/<VersionPrefix>$VERSION<\/VersionPrefix>/g" Directory.Build.props
sed -i "s/\*-\*/$VERSION/g" docs/code_samples/base.csx

rm -fr ./dist
dotnet pack "src/Mindee" -p:TargetFrameworks="${FRAMEWORK}" --output ./dist
cd ./dist
dotnet nuget push "*.nupkg" --source "NugetLocal"
