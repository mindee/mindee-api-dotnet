# Mindee CLI for .NET

Simple command-line interface for the Mindee APIs.

## Install

```bash
dotnet tool install --global Mindee.Cli
mindee -h
```

## Authentication

Pass your API key with `--api-key`:

- V2: `mindee --api-key <V2_API_KEY> ...`
- V1: `mindee v1 --api-key <V1_API_KEY> ...`

You can also configure keys via environment variables:

- V2: `MindeeV2__ApiKey`
- V1: `Mindee__ApiKey`

## V2 Usage (Latest)

V2 commands are top-level commands:

### Products
- `classification` : Classify tool
- `crop` : Crop tool
- `extraction` : Extraction tool (most common models)
- `ocr` : OCR tool
- `split` : Split tool

### Tools
- `search-models`

### Inference syntax

```bash
mindee --api-key <V2_API_KEY> <command> --model-id <MODEL_ID> [options] <path>
```

### Search-models syntax

```bash
mindee --api-key <V2_API_KEY> search-models [--name <NAME>] [--model-type <TYPE>] [--raw-json]
```

### V2 examples

```bash
# List available extraction models
mindee --api-key <V2_API_KEY> search-models --model-type extraction

# Run an extraction model
mindee --api-key <V2_API_KEY> extraction --model-id <MODEL_ID> ./invoice.pdf --output full --raw-text
```

## V1 (Legacy)

V1 commands are under the `v1` namespace:

- `barcode-reader`
- `cropper`
- `financial-document`
- `fr-bank-account-details`
- `fr-carte-grise`
- `fr-health-card`
- `fr-carte-nationale-d-identite`
- `fr-payslip`
- `international-id`
- `invoice`
- `invoice-splitter`
- `multi-receipts-detector`
- `passport`
- `receipt`
- `us-bank-check`

### General syntax

```bash
mindee v1 --api-key <V1_API_KEY> <product> [options] <path>
```

### V1 examples

```bash
# Run invoice parsing
mindee v1 --api-key <V1_API_KEY> invoice ./invoice.pdf --output summary

# Run async receipt parsing and print raw JSON
mindee v1 --api-key <V1_API_KEY> receipt ./receipt.jpg --async --output raw
```

## Output Modes

For commands that support `--output`:

- `summary`: basic summary (default)
- `full`: detailed result
- `raw`: full JSON response

## Help

```bash
mindee -h
mindee <command> -h
mindee v1 -h
mindee v1 <product> -h
```

## References

- V2 docs: https://docs.mindee.com/integrations/client-libraries-sdk
- V1 docs: https://docs.mindee.com/v1/libraries/dotnet-ocr-sdk
