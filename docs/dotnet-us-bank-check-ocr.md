The .NET OCR SDK supports the [Bank Checks OCR](https://developers.mindee.com/docs/bank-check-ocr) for extracting data from bank checks.

## Quick Start
```csharp
// Load a file from disk and parse it

string path = "./path/to/the/file.ext";
var prediction = await _mindeeClient
    .LoadDocument(File.OpenRead(Path), System.IO.Path.GetFileName(Path))
    .ParseAsync<BankCheckV1Prediction>();

// Print a summary of the parsed data
System.Console.WriteLine(prediction.Inference.Prediction.ToString());
```

Output:
```
----- US Bank Check V1 -----
Routing number: 012345678
Account number: 12345678910
Check number: 8620001342
Date: 2022-04-26
Amount: 6496.58
Payees: John Doe, Jane Doe
----------------------
```

&nbsp;
&nbsp;
**Questions?**
<img alt="Slack Logo Icon" style="display:inline!important" src="https://files.readme.io/5b83947-Slack.png" width="20" height="20">&nbsp;&nbsp;[Join our Slack]((https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
