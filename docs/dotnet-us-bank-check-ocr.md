The .NET OCR SDK supports the [Bank Checks OCR](https://developers.mindee.com/docs/bank-check-ocr) for extracting data from bank checks.

## Quick Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Us.BankCheck;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

MindeeClient mindeeClient = new MindeeClient(apiKey);

// load an input source
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<BankCheckV1>(inputSource);

// Print a summary of all the predictions
System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());

```

Output:
```
########
Document
########
:Mindee ID: 0626bc33-2b9f-4645-b455-6af111c551cf
:Filename: check.jpg

Inference
#########
:Product: mindee/bank_check v1.0
:Rotation applied: Yes

Prediction
==========
:Routing number: 012345678
:Account number: 12345678910
:Check number: 8620001342
:Date: 2022-04-26
:Amount: 6496.58
:Payees: John Doe, Jane Doe

Page Predictions
================

Page 0
------
:Routing number: 012345678
:Account number: 12345678910
:Check number: 8620001342
:Date: 2022-04-26
:Amount: 6496.58
:Payees: John Doe, Jane Doe
```

&nbsp;
&nbsp;
**Questions?**
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
