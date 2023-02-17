#! /bin/sh

for f in `find docs/code_samples -name "*.txt"`
do
  echo $f
  cat docs/code_samples/base.csx $f > _test.csx
  sed -i 's/my-api-key/$1/' _test.csx
  sed -i 's/\/path\/to\/the\/file.ext/.\/data\/pdf\/blank_1.pdf/' _test.csx
  dotnet-script _test.csx
done
