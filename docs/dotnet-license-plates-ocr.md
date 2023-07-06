The .NET SDK OCR SDK supports the  API  [Licenses plate OCR](https://developers.mindee.com/docs/license-plates-ocr-nodejs)  for extracting data from pictures (or documents) of cars.

Using this [sample photo](https://files.readme.io/ffc127d-sample_receipt.jpg) below, we are going to illustrate how to extract the data that we want using the OCR SDK.
![sample receipt](https://files.readme.io/fd6086e-license_plate.jpg)

## Quick Start
```csharp
using Mindee;
using Mindee.Product.Eu.LicensePlate;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

MindeeClient mindeeClient = new MindeeClient(apiKey);

var response = await mindeeClient
    .LoadDocument(File.OpenRead(filePath), System.IO.Path.GetFileName(filePath))
    .ParseAsync<LicensePlateV1>();

// Print a summary of the predictions
System.Console.WriteLine(response.Document.ToString());

// Print the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

Output:
```
########
Document
########
:Mindee ID: 858d2e05-2166-46ad-81da-c917e3a1b453
:Filename: multiplate.jpg

Inference
#########
:Product: mindee/license_plates v1.0
:Rotation applied: No

Prediction
==========
:License plates: 8LQA341, 8LBW890

Page Predictions
================

Page 0
------
:License plates: 8LQA341, 8LBW890
``` 

&nbsp;
&nbsp;
**Questions?**
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
