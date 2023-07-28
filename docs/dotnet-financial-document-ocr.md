The .NET OCR SDK supports the [Financial document OCR API](https://developers.mindee.com/docs/financial-documents-ocr).

Using this [sample](https://files.readme.io/a8e8cfa-a74eaa5-c8e283b-sample_invoice.jpeg) below, we are going to illustrate how to extract the data that we want using the OCR SDK.
![sample](https://files.readme.io/a8e8cfa-a74eaa5-c8e283b-sample_invoice.jpeg)

## Quick Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.FinancialDocument;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<FinancialDocumentV1>(inputSource);

// Print a summary of all the predictions
// System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

Output:
```
:Locale: en; en; CAD;
:Invoice Number: 14
:Reference Numbers: AD29094
:Purchase Date: 2018-09-25
:Due Date: 2018-09-25
:Total Net:
:Total Amount: 2608.20
:Taxes:
  +---------------+--------+----------+---------------+
  | Base          | Code   | Rate (%) | Amount        |
  +===============+========+==========+===============+
  |               |        | 8.00     | 193.20        |
  +---------------+--------+----------+---------------+
:Supplier Payment Details:
:Supplier name: TURNPIKE DESIGNS CO.
:Supplier Company Registrations:
:Supplier Address: 156 University Ave, Toronto ON, Canada M5H 2H7
:Supplier Phone Number:
:Customer name: JIRO DOI
:Customer Company Registrations:
:Customer Address: 1954 Bloon Street West Toronto, ON, M6P 3K9 Canada
:Document Type: INVOICE
:Purchase Subcategory:
:Purchase Category: miscellaneous
:Total Tax: 193.20
:Tip and Gratuity:
:Purchase Time:
:Line Items:
  +--------------------------------------+--------------+----------+------------+--------------+--------------+------------+
  | Description                          | Product code | Quantity | Tax Amount | Tax Rate (%) | Total Amount | Unit Price |
  +======================================+==============+==========+============+==============+==============+============+
  | Platinum web hosting package Down... |              | 1.00     |            |              | 65.00        | 65.00      |
  +--------------------------------------+--------------+----------+------------+--------------+--------------+------------+
  | 2 page website design Includes ba... |              | 3.00     |            |              | 2100.00      | 2100.00    |
  +--------------------------------------+--------------+----------+------------+--------------+--------------+------------+
  | Mobile designs Includes responsiv... |              | 1.00     |            |              | 250.00       | 250.00     |
  +--------------------------------------+--------------+----------+------------+--------------+--------------+------------+
```

&nbsp;
&nbsp;
**Questions?**
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
