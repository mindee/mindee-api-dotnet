The .NET OCR SDK supports the [Proof of Address OCR API](https://developers.mindee.com/docs/proof-of-address-ocr).

Using this [sample](https://files.readme.io/a8e8cfa-a74eaa5-c8e283b-sample_invoice.jpeg) below, we are going to illustrate how to extract the data that we want using the OCR SDK.
![sample](https://files.readme.io/a8e8cfa-a74eaa5-c8e283b-sample_invoice.jpeg)

## Quick Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.ProofOfAddress;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

MindeeClient mindeeClient = new MindeeClient(apiKey);

// load an input source
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<ProofOfAddressV1>(inputSource);

// Print a summary of all the predictions
System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());

```

Output:
```
:Locale: en; en; CAD;
:Issuer Name: TURNPIKE DESIGNS CO.
:Issuer Company Registrations:
:Issuer Address: 156 University Ave, Toronto ON, Canada M5H 2H7
:Recipient Name: JIRO DOI
:Recipient Company Registrations:
:Recipient Address: 1954 Bloon Street West Toronto, ON, M6P 3K9 Canada
:Dates: 2018-09-25
:Date of Issue: 2018-09-25
```

&nbsp;
&nbsp;
**Questions?**
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
