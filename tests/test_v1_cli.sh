#! /bin/bash
set -e

TEST_FILE=$1
NET_VERSION=$2
RID=$3

if [ -z "$TEST_FILE" ]; then
  echo "Error: no sample file provided"
  exit 1
fi

if [ -z "$NET_VERSION" ]; then
  NET_VERSION="net6.0"
  echo "Warning: no .NET version provided, defaulting to '$NET_VERSION'"
fi

if [ -z "$RID" ]; then
  if [[ "$OSTYPE" == "linux-gnu"* ]]; then
    RID="linux-x64"
  elif  [[ "$OSTYPE" == "darwin"* ]]; then
    RID="osx-x64"
  elif  [[ "$OSTYPE" == "win"* ]]; then
    RID="win-x64"
  else
    echo ""
    echo "Error: Could not determine default Runtime Identifier (RID) for OS type '$OSTYPE'."
    echo "Please provide one manually. Available: 'linux-x64', 'osx-x64', 'win-x64'"
    exit 1
  fi
  echo "Warning: Runtime Identifier (RID) not provided, defaulting to $RID"
fi
WD="$(basename $PWD)"
if [[ "$WD" == "tests" ]]; then
  CLI_PATH="../src/Mindee.Cli/bin/Release/$NET_VERSION/$RID/Mindee.Cli"
else
  CLI_PATH="./src/Mindee.Cli/bin/Release/$NET_VERSION/$RID/Mindee.Cli"
fi

PRODUCT_ARRAY=("financial-document" "receipt" "invoice" "invoice-splitter")
PRODUCTS_SIZE="${#PRODUCT_ARRAY[@]}"
i=1
for product in "${PRODUCT_ARRAY[@]}"
do
  echo "--- Test $product with Summary Output ($i/$PRODUCTS_SIZE) ---"
  SUMMARY_OUTPUT=$("$CLI_PATH" v1 "$product" "$TEST_FILE")
  echo "$SUMMARY_OUTPUT"
  echo ""
  echo ""
  sleep 0.5
  ((i++))
done
