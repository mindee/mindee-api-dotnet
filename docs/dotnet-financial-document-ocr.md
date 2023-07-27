The .NET OCR SDK supports the [Financial document OCR API](https://developers.mindee.com/docs/financial-documents-ocr).

Using this [sample](https://files.readme.io/a8e8cfa-a74eaa5-c8e283b-sample_invoice.jpeg) below, we are going to illustrate how to extract the data that we want using the OCR SDK.
![sample](https://files.readme.io/a8e8cfa-a74eaa5-c8e283b-sample_invoice.jpeg)

## Quick Start
```csharp
using Mindee;
using Mindee.Parsing.Financial;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

MindeeClient mindeeClient = MindeeClientInit.Create(apiKey);

var response = await mindeeClient
    .LoadDocument(File.OpenRead(filePath), System.IO.Path.GetFileName(filePath))
    .ParseAsync<FinancialV1Inference>();

// Print a summary of the predictions
System.Console.WriteLine(response.Document.ToString());

// Print the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

Output:
```
:Document type: INVOICE
:Category: miscellaneous
:Subcategory:
:Locale: en; en; USD;
:Date: 2019-02-11
:Due date: 2019-02-26
:Time:
:Number: INT-001
:Reference numbers: 2412/2019
:Supplier name: JOHN SMITH
:Supplier address: 4490 Oak Drive Albany, NY 12210
:Supplier company registrations:
:Supplier payment details:
:Customer name: JESSIE M HORNE
:Customer address: 2019 Redbud Drive New York, NY 10011
:Customer company registrations:
:Tip:
:Taxes: 9.75 5.00%
:Total taxes: 9.75
:Total net: 195.00
:Total amount: 204.75

:Line Items:
====================== ======== ========= ========== ================== ====================================
Code                   QTY      Price     Amount     Tax (Rate)         Description
====================== ======== ========= ========== ================== ====================================
                       1.00     100.00    100.00                        Front and rear brake cables
                       2.00     25.00     50.00                         New set of pedal arms
                       3.00     15.00     45.00                         Labon 3hrs
====================== ======== ========= ========== ================== ====================================

```

&nbsp;
&nbsp;
**Questions?**
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
