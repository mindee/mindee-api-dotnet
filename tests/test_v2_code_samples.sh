#! /bin/sh
set -e

OUTPUT_FILE='_test_v2.csx'
API_KEY=$1
MODEL_ID=$2
WEBHOOK_ID=$3

if [ -z "${API_KEY}" ]; then echo "API_KEY is required"; exit 1; fi
if [ -z "${MODEL_ID}" ]; then echo "MODEL_ID is required"; exit 1; fi
if [ -z "${WEBHOOK_ID}" ]; then echo "WEBHOOK_ID is required"; exit 1; fi

for f in $(find docs/code_samples -maxdepth 1 -name "v2_*.txt" | sort -h)
do
  echo "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"
  echo "${f}"
  echo "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"
  echo

  cat docs/code_samples/base.csx "${f}" > $OUTPUT_FILE

  sed -i "s/MY_API_KEY/$API_KEY/" $OUTPUT_FILE
  sed -i "s/MY_MODEL_ID/$MODEL_ID/" $OUTPUT_FILE
  sed -i "s/MY_WEBHOOK_ID/$WEBHOOK_ID/" $OUTPUT_FILE

  sed -i "s/\/path\/to\/the\/file.ext/tests\/resources\/file_types\/pdf\/blank_1.pdf/" $OUTPUT_FILE

  dotnet-script $OUTPUT_FILE
done
