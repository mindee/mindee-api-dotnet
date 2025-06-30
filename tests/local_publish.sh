#! /bin/sh
set -e

FRAMEWORK=$1
if [ -z "${FRAMEWORK}" ]; then FRAMEWORK="net9.0"; fi

NUGET_ROOT_DIR=$2
if [ -z "${NUGET_ROOT_DIR}" ]; then NUGET_ROOT_DIR="$HOME"; fi

VERSION="99.99.99"
NUGET_DIR="${NUGET_ROOT_DIR}/nuget_local"

# only clean up as needed, fails otherwise
if dotnet nuget list source | grep "NugetLocal" -q
then
  dotnet nuget remove source "NugetLocal"
  rm -fr "${NUGET_DIR}"
fi

mkdir -p "${NUGET_DIR}"
dotnet nuget add source "${NUGET_DIR}" -n "NugetLocal"

sed -i "s/<VersionPrefix>[0-9.]*<\/VersionPrefix>/<VersionPrefix>$VERSION<\/VersionPrefix>/g" Directory.Build.props
sed -i "s/<VersionSuffix>.*<\/VersionSuffix>//g" Directory.Build.props

sed -i "s/\*-\*/$VERSION/g" docs/code_samples/base.csx

rm -fr ./dist
dotnet pack "src/Mindee" -p:TargetFrameworks="${FRAMEWORK}" --force --output ./dist
cd ./dist
dotnet nuget push "*.nupkg" --source "NugetLocal"
