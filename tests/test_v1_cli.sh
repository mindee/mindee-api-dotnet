#!/bin/sh
set -e

TEST_FILE=$1
NET_VERSION=$2
RID=$3

if [ -z "$TEST_FILE" ]; then
  TEST_FILE='./tests/resources/file_types/pdf/blank_1.pdf'
fi
echo "TEST_FILE: ${TEST_FILE}"

if [ -z "$NET_VERSION" ]; then
  NET_VERSION="net8.0"
fi
echo "NET_VERSION: ${NET_VERSION}"

if [ -z "$RID" ]; then
  OS_NAME="$(uname -s)"
  case "$OS_NAME" in
    Linux*)     RID="linux-x64" ;;
    Darwin*)    RID="osx-x64" ;;
    CYGWIN*|MINGW*|MSYS*) RID="win-x64" ;;
    *)
      echo ""
      echo "Error: Could not determine default Runtime Identifier (RID) for OS type '$OS_NAME'."
      echo "Please provide one manually. Available: 'linux-x64', 'osx-x64', 'win-x64'"
      exit 1
      ;;
  esac
fi
echo "RID: ${RID}"

CLI_PATH="./src/Mindee.Cli/bin/Release/$NET_VERSION/$RID/Mindee.Cli"
if [ "$RID" = "win-x64" ]; then
  CLI_PATH="${CLI_PATH}.exe"
fi
echo "CLI_PATH: ${CLI_PATH}"

PRODUCTS="financial-document receipt invoice invoice-splitter"
PRODUCTS_SIZE=4
i=1

for product in $PRODUCTS
do
  echo "--- Test $product with Summary Output ($i/$PRODUCTS_SIZE) ---"
  SUMMARY_OUTPUT=$("$CLI_PATH" v1 "$product" "$TEST_FILE")
  echo "$SUMMARY_OUTPUT"
  echo ""
  echo ""
  sleep 0.5
  i=$((i + 1))
done
