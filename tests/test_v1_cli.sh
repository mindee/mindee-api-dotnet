#!/bin/sh
set -e

TEST_FILE=$1
NET_VERSION=$2
RID=$3

if [ -z "$TEST_FILE" ]; then
  echo "Error: no sample file provided"
  exit 1
fi

if [ -z "$NET_VERSION" ]; then
  NET_VERSION="net8.0"
  echo "Warning: no .NET version provided, defaulting to '$NET_VERSION'"
fi

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
  echo "Warning: Runtime Identifier (RID) not provided, defaulting to $RID"
fi

WD="$(basename "$PWD")"
if [ "$WD" = "tests" ]; then
  CLI_PATH="../src/Mindee.Cli/bin/Release/$NET_VERSION/$RID/Mindee.Cli"
else
  CLI_PATH="./src/Mindee.Cli/bin/Release/$NET_VERSION/$RID/Mindee.Cli"
fi
if [[ "$RID" == "win-x64" ]]; then
  CLI_PATH="${CLI_PATH}.exe"
fi

if [ "$RID" = "win-x64" ]; then
  CLI_PATH="${CLI_PATH}.exe"
fi

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
