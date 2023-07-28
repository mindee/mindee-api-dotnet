The .NET SDK OCR SDK supports the  API  [Licenses plate OCR](https://developers.mindee.com/docs/license-plates-ocr-nodejs)  for extracting data from pictures (or documents) of cars.

Using this [sample photo](https://files.readme.io/ffc127d-sample_receipt.jpg) below, we are going to illustrate how to extract the data that we want using the OCR SDK.
![sample receipt](https://files.readme.io/fd6086e-license_plate.jpg)

## Quick Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Eu.LicensePlate;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<LicensePlateV1>(inputSource);

// Print a summary of all the predictions
// System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

Output:
```
:License plates: 8LQA341, 8LBW890
``` 

&nbsp;
&nbsp;
**Questions?**
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
