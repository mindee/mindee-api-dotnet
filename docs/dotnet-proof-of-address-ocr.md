The .NET OCR SDK supports the [Proof of Address OCR API](https://developers.mindee.com/docs/proof-of-address-ocr).

Using this [sample](https://files.readme.io/a8e8cfa-a74eaa5-c8e283b-sample_invoice.jpeg) below, we are going to illustrate how to extract the data that we want using the OCR SDK.
![sample](https://files.readme.io/a8e8cfa-a74eaa5-c8e283b-sample_invoice.jpeg)

## Quick Start
```csharp
// Load a file from disk and parse it

string path = "./path/to/the/file.ext";
var prediction = await _mindeeClient
    .LoadDocument(File.OpenRead(Path), System.IO.Path.GetFileName(Path))
    .ParseAsync<ProofOfAddressV1Inference>();

// Print a summary of the parsed data
System.Console.WriteLine(prediction.ToString());
```

Output:
```
########
Document
########
:Mindee ID: 69eabee4-8f29-4e11-bb24-6a4ed965910a
:Filename: ppl_electric.jpg

Inference
#########
:Product: mindee/proof_of_address v1.0
:Rotation applied: No

Prediction
==========
:Locale: en; en; USD;
:Issuer name: PPL ELECTRIC UTILITIES
:Issuer Address: 2 NORTH 9TH STREET CPC-GENN1 ALLENTOWN,PA 18101-1175
:Issuer Company Registrations:
:Recipient name:
:Recipient Address: 123 MAIN ST ANYTOWN,PA 18062
:Recipient Company Registrations:
:Issuance date: 2011-07-27
:Dates: 2011-07-27
        2011-07-06
        2011-08-03
        2011-07-27
        2011-06-01
        2011-07-01
        2010-07-01
        2010-08-01
        2011-07-01
        2009-08-01
        2010-07-01
        2011-07-27

Page Predictions
================

Page 0
------
:Locale: en; en; USD;
:Issuer name: PPL ELECTRIC UTILITIES
:Issuer Address: 2 NORTH 9TH STREET CPC-GENN1 ALLENTOWN,PA 18101-1175
:Issuer Company Registrations:
:Recipient name:
:Recipient Address: 123 MAIN ST ANYTOWN,PA 18062
:Recipient Company Registrations:
:Issuance date: 2011-07-27
:Dates: 2011-07-27
        2011-07-06
        2011-08-03
        2011-07-27
        2011-06-01
        2011-07-01
        2010-07-01
        2010-08-01
        2011-07-01
        2009-08-01
        2010-07-01
        2011-07-27

```

&nbsp;
&nbsp;
**Questions?**
<img alt="Slack Logo Icon" style="display:inline!important" src="https://files.readme.io/5b83947-Slack.png" width="20" height="20">&nbsp;&nbsp;[Join our Slack]((https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)