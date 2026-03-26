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

echo "--- Test model list retrieval"
MODELS=$("$CLI_PATH" search-models)
if [ -z "$MODELS" ]; then
  echo "Error: no models found"
  exit 1
else
  echo "Models retrieval OK"
fi

run_test() {
  model_id="$1"
  model_type="$2"

  echo "--- Test $model_type ID: $model_id"
  SUMMARY_OUTPUT=$("$CLI_PATH" "$model_type" -m "$model_id" "$TEST_FILE")
  echo "$SUMMARY_OUTPUT"
  echo ""
  echo ""
  sleep 0.5
}

run_test "$MINDEE_V2_SE_TESTS_FINDOC_MODEL_ID" "extraction"
run_test "$MINDEE_V2_SE_TESTS_CROP_MODEL_ID" "crop"
run_test "$MINDEE_V2_SE_TESTS_SPLIT_MODEL_ID" "split"
run_test "$MINDEE_V2_SE_TESTS_CLASSIFICATION_MODEL_ID" "classification"
run_test "$MINDEE_V2_SE_TESTS_OCR_MODEL_ID" "ocr"
