The .NET SDK OCR SDK supports the  API  [Licenses plate OCR](https://developers.mindee.com/docs/license-plates-ocr-nodejs)  for extracting data from pictures (or documents) of cars.

Using this [sample photo](https://files.readme.io/ffc127d-sample_receipt.jpg) below, we are going to illustrate how to extract the data that we want using the OCR SDK.
![sample receipt](https://files.readme.io/fd6086e-license_plate.jpg)

## Quick Start
```csharp
// Load a file from disk and parse it

string path = "./path/to/the/file.ext";
var prediction = await _mindeeClient
    .LoadDocument(File.OpenRead(Path), System.IO.Path.GetFileName(Path))
    .ParseAsync<LicensePlatesV1Prediction>();

// Print a summary of the parsed data
System.Console.WriteLine(prediction.Inference.Prediction.ToString());
```

Output:
```
----- EU License plate V1 -----
Licence plates: BY323YB
----------------------
```

&nbsp;
&nbsp;
**Questions?**
<img alt="Slack Logo Icon" style="display:inline!important" src="https://files.readme.io/5b83947-Slack.png" width="20" height="20">&nbsp;&nbsp;[Join our Slack]((https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
