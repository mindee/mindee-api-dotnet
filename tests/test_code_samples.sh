#! /bin/sh
set -e

OUTPUT_FILE='_test.csx'
ACCOUNT=$1
ENDPOINT=$2
API_KEY=$3

if [ -z "${ACCOUNT}" ]; then echo "ACCOUNT is required"; exit 1; fi
if [ -z "${ENDPOINT}" ]; then echo "ENDPOINT is required"; exit 1; fi

for f in $(find docs/code_samples -maxdepth 1 -name "*.txt" | sort -h)
do
  echo "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"
  echo "${f}"
  echo "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"
  echo

  cat docs/code_samples/base.csx "${f}" > $OUTPUT_FILE

  if echo "${f}" | grep -q "custom_v1.txt"
  then
    sed -i "s/my-account/$ACCOUNT/g" $OUTPUT_FILE
    sed -i "s/my-endpoint/$ENDPOINT/g" $OUTPUT_FILE
  fi

  if echo "${f}" | grep -q "default.txt"
  then
    sed -i "s/my-endpoint/bank_account_details/" $OUTPUT_FILE
    sed -i "s/my-account/mindee/" $OUTPUT_FILE
    sed -i "s/my-version/1/" $OUTPUT_FILE
  fi

  sed -i "s/my-api-key/$API_KEY/" $OUTPUT_FILE
  sed -i "s/\/path\/to\/the\/file.ext/tests\/resources\/pdf\/blank_1.pdf/" $OUTPUT_FILE

  dotnet-script $OUTPUT_FILE
done
