The .NET OCR SDK supports the [Bank Checks OCR](https://developers.mindee.com/docs/bank-check-ocr) for extracting data from bank checks.

## Quick Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Us.BankCheck;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<BankCheckV1>(inputSource);

// Print a summary of all the predictions
// System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

Output:
```
:Check Issue Date: 2022-03-29
:Amount: 15332.90
:Payees: JOHN DOE
         JANE DOE
:Routing Number: 063608668
:Account Number: 7789778136
:Check Number: 0003401
```

&nbsp;
&nbsp;
**Questions?**
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
