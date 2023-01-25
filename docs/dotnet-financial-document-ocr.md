The .NET OCR SDK supports the [Financial document OCR API](financial-documents-ocr).

Using this [sample](https://files.readme.io/a8e8cfa-a74eaa5-c8e283b-sample_invoice.jpeg) below, we are going to illustrate how to extract the data that we want using the OCR SDK.
![sample](https://files.readme.io/a8e8cfa-a74eaa5-c8e283b-sample_invoice.jpeg)

## Quick Start
```csharp
// Load a file from disk and parse it

string path = "./path/to/the/file.ext";
var prediction = await _mindeeClient
    .LoadDocument(File.OpenRead(Path), System.IO.Path.GetFileName(Path))
    .ParseAsync<FinancialDocumentV1Inference>();

// Print a summary of the parsed data
System.Console.WriteLine(prediction.ToString());
```

Output:
```
########
Document
########
:Mindee ID: 69eabee4-8f29-4e11-bb24-6a4ed965910a
:Filename: invoice.jpg

Inference
#########
:Product: mindee/financial_document v1.0
:Rotation applied: Yes

Prediction
==========
:Document type: INVOICE
:Category: miscellaneous
:Subcategory:
:Locale: en; en; USD;
:Date: 2019-02-11
:Due date: 2019-02-26
:Time:
:Number: INT-001
:Reference numbers: 2412/2019
:Date: 2019-02-11
:Due date: 2019-02-26
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

Page Predictions
================

Page 0
------
:Document type: INVOICE
:Category: miscellaneous
:Subcategory:
:Locale: en; en; USD;
:Date: 2019-02-11
:Due date: 2019-02-26
:Time:
:Number: INT-001
:Reference numbers: 2412/2019
:Date: 2019-02-11
:Due date: 2019-02-26
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
<img alt="Slack Logo Icon" style="display:inline!important" src="https://files.readme.io/5b83947-Slack.png" width="20" height="20">&nbsp;&nbsp;[Join our Slack]((https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
