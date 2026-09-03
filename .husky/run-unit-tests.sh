#!/usr/bin/env bash
#
# Run unit tests for the target frameworks that are actually supported on the
# current OS. .NET Framework targets (net472 / net48) require Windows because
# Docnet.Core's native PDF binaries don't load under Mono on *nix.
#
# Mirrors the matrix used in .github/workflows/_test-units.yml.
#
set -euo pipefail

PROJECT="tests/Mindee.UnitTests/Mindee.UnitTests.csproj"

case "$(uname -s 2>/dev/null || echo Windows)" in
   MINGW*|MSYS*|CYGWIN*|Windows*)
      FRAMEWORKS=("net6.0" "net8.0" "net10.0" "net472" "net48")
      ;;
   *)
      FRAMEWORKS=("net8.0" "net10.0")
      ;;
esac

echo "[husky] Running unit tests for: ${FRAMEWORKS[*]}"

for tfm in "${FRAMEWORKS[@]}"; do
   echo "[husky] --- $tfm ---"
   dotnet test "$PROJECT" -f "$tfm" --nologo -v:quiet
done
