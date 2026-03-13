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

echo "--- Test model list retrieval"
MODELS=$("$CLI_PATH" v2 search-models)
if [[ -z "${MODELS}" ]]; then
  echo "Error: no models found"
  exit 1
else
  echo "Models retrieval OK"
fi

declare -A MODEL_MAP

MODEL_MAP=(
  ["$MINDEE_V2_SE_TESTS_FINDOC_MODEL_ID"]="extraction"
  ["$MINDEE_V2_SE_TESTS_CROP_MODEL_ID"]="crop"
  ["$MINDEE_V2_SE_TESTS_SPLIT_MODEL_ID"]="split"
  ["$MINDEE_V2_SE_TESTS_CLASSIFICATION_MODEL_ID"]="classification"
  ["$MINDEE_V2_SE_TESTS_OCR_MODEL_ID"]="ocr"
)

MODELS_SIZE="${#MODEL_MAP[@]}"
i=1

for model_id in "${!MODEL_MAP[@]}"
do
  model_type="${MODEL_MAP[$model_id]}"
  echo "--- Test $model_type (ID: $model_id) with Summary Output ($i/$MODELS_SIZE) ---"
  SUMMARY_OUTPUT=$("$CLI_PATH" v2 "$model_type" -m "$model_id" "$TEST_FILE")
  echo "$SUMMARY_OUTPUT"
  echo ""
  echo ""
  sleep 0.5
  ((i++))
done
