#! /bin/sh
set -e

FRAMEWORK=$1
if [ -z "${FRAMEWORK}" ]; then FRAMEWORK="net9.0"; fi

NUGET_ROOT_DIR=$2
if [ -z "${NUGET_ROOT_DIR}" ]; then NUGET_ROOT_DIR="$HOME"; fi

STRONG_NAME_KEY_FILE=$3
VERSION="99.99.99"
NUGET_DIR="${NUGET_ROOT_DIR}/nuget_local"
TMP_STRONG_NAME_KEY_FILE=""

cleanup() {
  if [ -n "${TMP_STRONG_NAME_KEY_FILE}" ] && [ -f "${TMP_STRONG_NAME_KEY_FILE}" ]; then
    rm -f "${TMP_STRONG_NAME_KEY_FILE}"
  fi
}
trap cleanup EXIT

# Resolve strong-name key source for local smoke tests.
if [ -z "${STRONG_NAME_KEY_FILE}" ]; then
  if [ -n "${MINDEE_STRONG_NAME_KEY_FILE}" ]; then
    STRONG_NAME_KEY_FILE="${MINDEE_STRONG_NAME_KEY_FILE}"
  elif [ -n "${MINDEE_SNK_B64}" ]; then
    TMP_STRONG_NAME_KEY_FILE="$(mktemp /tmp/Mindee.XXXXXX.snk)"
    printf "%s" "${MINDEE_SNK_B64}" | base64 -d > "${TMP_STRONG_NAME_KEY_FILE}"
    chmod 600 "${TMP_STRONG_NAME_KEY_FILE}"
    STRONG_NAME_KEY_FILE="${TMP_STRONG_NAME_KEY_FILE}"
  elif command -v sn >/dev/null 2>&1; then
    TMP_STRONG_NAME_KEY_FILE="$(mktemp /tmp/Mindee.XXXXXX.snk)"
    sn -k "${TMP_STRONG_NAME_KEY_FILE}"
    chmod 600 "${TMP_STRONG_NAME_KEY_FILE}"
    STRONG_NAME_KEY_FILE="${TMP_STRONG_NAME_KEY_FILE}"
  else
    echo "Strong-name key not found. Pass key path as 3rd arg, set MINDEE_STRONG_NAME_KEY_FILE, or set MINDEE_SNK_B64."
    exit 1
  fi
fi

if [ ! -f "${STRONG_NAME_KEY_FILE}" ]; then
  echo "Strong-name key file does not exist: ${STRONG_NAME_KEY_FILE}"
  exit 1
fi

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
dotnet pack "src/Mindee" -c Release -p:TargetFrameworks="${FRAMEWORK}" -p:MindeeStrongNameKeyFile="${STRONG_NAME_KEY_FILE}" --force --output ./dist
cd ./dist
dotnet nuget push "*.nupkg" --source "NugetLocal"
