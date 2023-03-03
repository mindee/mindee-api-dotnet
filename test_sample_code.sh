#! /bin/sh
set -e
for f in `find docs/code_samples -name "*.txt"`
do
  echo $f
  if echo "$f" | grep -q "custom_v1.txt" 
  then 
    echo "Not processed."
    continue
  fi

  cat docs/code_samples/base.csx $f > _test.csx

  if echo "$f" | grep -q "default.txt"
  then
    sed -i "s/my-endpoint/bank_account_details/" _test.csx
    sed -i "s/my-account/mindee/" _test.csx
    sed -i "s/my-version/1/" _test.csx
  fi

  sed -i "s/my-api-key/$1/" _test.csx
  sed -i "s/\/path\/to\/the\/file.ext/tests\/resources\/pdf\/blank_1.pdf/" _test.csx
    
  dotnet-script _test.csx
done
