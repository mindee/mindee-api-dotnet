The .NET OCR SDK supports the  API  [Shipping containers OCR](https://developers.mindee.com/docs/shipping-containers-ocr) for extracting data from pictures of containers identification.

Using this [sample](https://files.readme.io/853f15a-shipping_containers.jpg) below, we are going to illustrate how to extract the data that we want using the OCR SDK.
![sample](https://files.readme.io/853f15a-shipping_containers.jpg)

## Quick Start
```csharp
// Load a file from disk and parse it

string path = "./path/to/the/file.ext";
var prediction = await _mindeeClient
    .LoadDocument(File.OpenRead(Path), System.IO.Path.GetFileName(Path))
    .ParseAsync<ShippingContainerV1Prediction>();

// Print a summary of the parsed data
System.Console.WriteLine(prediction.Inference.Prediction.ToString());
```

Output:
```
----- Shipping Container V1 -----
Owner: MMAU
Serial number: 1193249
Size and type: 45R1
----------------------
```

&nbsp;
&nbsp;
**Questions?**
<img alt="Slack Logo Icon" style="display:inline!important" src="https://files.readme.io/5b83947-Slack.png" width="20" height="20">&nbsp;&nbsp;[Join our Slack]((https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
