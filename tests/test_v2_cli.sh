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

echo "--- Test model list retrieval"
MODELS=$("$CLI_PATH" v2 search-models)
if [[ -z "${MODELS}" ]]; then
  echo "Error: no models found"
  exit 1
else
  echo "Models retrieval OK"
fi

declare -A MODEL_MAP

if [ "$RID" = "win-x64" ]; then
  CLI_PATH="${CLI_PATH}.exe"
fi

echo "--- Test model list retrieval"
MODELS=$("$CLI_PATH" v2 search-models)
if [ -z "$MODELS" ]; then
  echo "Error: no models found"
  exit 1
else
  echo "Models retrieval OK"
fi

MODELS_SIZE=5
i=1

run_test() {
  model_id="$1"
  model_type="$2"

  echo "--- Test $model_type (ID: $model_id) with Summary Output ($i/$MODELS_SIZE) ---"
  SUMMARY_OUTPUT=$("$CLI_PATH" v2 "$model_type" -m "$model_id" "$TEST_FILE")
  echo "$SUMMARY_OUTPUT"
  echo ""
  echo ""
  sleep 0.5

  i=$((i + 1))
}

run_test "$MINDEE_V2_SE_TESTS_FINDOC_MODEL_ID" "extraction"
run_test "$MINDEE_V2_SE_TESTS_CROP_MODEL_ID" "crop"
run_test "$MINDEE_V2_SE_TESTS_SPLIT_MODEL_ID" "split"
run_test "$MINDEE_V2_SE_TESTS_CLASSIFICATION_MODEL_ID" "classification"
run_test "$MINDEE_V2_SE_TESTS_OCR_MODEL_ID" "ocr"
