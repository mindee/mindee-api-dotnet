#!/usr/bin/env bash
#
# License whitelist check for NuGet dependencies.
#
# Runs `dotnet delice` and fails if any package uses a license expression that
# isn't listed in .husky/licenses.allowed, unless the package name appears in
# .husky/licenses.allowed-packages.
#
# Requires: dotnet, jq, bash. No Python.
#
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
SLN="$SCRIPT_DIR/../Mindee.sln"
ALLOWED_FILE="$SCRIPT_DIR/licenses.allowed"
ALLOWED_PKGS_FILE="$SCRIPT_DIR/licenses.allowed-packages"

command -v jq >/dev/null 2>&1 || {
   echo "[husky] jq is required for the license check but was not found in PATH." >&2
   exit 2
}

TMP_JSON="$(mktemp -t delice-XXXXXX.json)"
trap 'rm -f "$TMP_JSON"' EXIT

echo "[husky] Running dotnet delice on ${SLN} ..."
dotnet delice "$SLN" -j --json-output "$TMP_JSON" >/dev/null

strip_comments() {
   # Drop blank lines and '#' comments, trim trailing whitespace.
   sed -e 's/[[:space:]]*$//' -e '/^[[:space:]]*#/d' -e '/^[[:space:]]*$/d' "$1"
}

ALLOWED_LICENSES="$(strip_comments "$ALLOWED_FILE" || true)"
ALLOWED_PACKAGES="$(strip_comments "$ALLOWED_PKGS_FILE" 2>/dev/null || true)"

# Emit one tab-separated record per package: project<TAB>name<TAB>version<TAB>expression
# We deliberately avoid jq's @tsv, which doubles backslashes (breaking matches
# against expressions like `licenses\LICENSE.txt`).
RECORDS="$(jq -r '
   .projects[]
   | .projectName as $p
   | .licenses[]
   | .expression as $e
   | .packages[]
   | [$p, .name, (.version // "?"), $e] | join("\t")
' "$TMP_JSON")"

violations=""
while IFS=$'\t' read -r project name version expression; do
   [ -z "${name:-}" ] && continue
   # Allowed license expression?
   if printf '%s\n' "$ALLOWED_LICENSES" | grep -Fxq -- "$expression"; then
      continue
   fi
   # Per-package exception?
   if [ -n "$ALLOWED_PACKAGES" ] && printf '%s\n' "$ALLOWED_PACKAGES" | grep -Fxq -- "$name"; then
      continue
   fi
   violations+=$'\n'"  - ${name}@${version} (license: ${expression}) [project: ${project}]"
done <<< "$RECORDS"

if [ -n "$violations" ]; then
   {
      echo "Disallowed package licenses detected:"
      # De-duplicate while preserving order.
      printf '%s\n' "$violations" | awk 'NF && !seen[$0]++'
      echo
      echo "Either remove the offending dependency, add the license expression to"
      echo ".husky/licenses.allowed, or add the package name to"
      echo ".husky/licenses.allowed-packages after review."
   } >&2
   exit 1
fi

echo "License check passed: all packages use whitelisted licenses."
