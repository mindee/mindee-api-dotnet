The .NET OCR SDK supports the [receipt API](https://developers.mindee.com/docs/receipt-ocr) for extracting data from receipts.

Using this [sample receipt](https://files.readme.io/ffc127d-sample_receipt.jpg) below, we are going to illustrate how to extract the data that we want using the OCR SDK.
![sample receipt](https://files.readme.io/ffc127d-sample_receipt.jpg)

## Quick Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Receipt;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<ReceiptV5>(inputSource);

// Print a summary of all the predictions
// System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

Output:
```
:Expense Locale: en-GB; en; GB; GBP;
:Purchase Category: food
:Purchase Subcategory: restaurant
:Document Type: EXPENSE RECEIPT
:Purchase Date: 2016-02-26
:Purchase Time: 15:20
:Total Amount: 10.20
:Total Net: 8.50
:Total Tax: 1.70
:Tip and Gratuity:
:Taxes:
  +---------------+--------+----------+---------------+
  | Base          | Code   | Rate (%) | Amount        |
  +===============+========+==========+===============+
  | 8.50          | VAT    | 20.00    | 1.70          |
  +---------------+--------+----------+---------------+
:Supplier Name: CLACHAN
:Supplier Company Registrations: 232153895
                                 232153895
:Supplier Address: 34 kingley street w1b 5qh
:Supplier Phone Number: 02074940834
:Line Items:
  +--------------------------------------+----------+--------------+------------+
  | Description                          | Quantity | Total Amount | Unit Price |
  +======================================+==========+==============+============+
  | Meantime Pale                        | 2.00     | 10.20        |            |
  +--------------------------------------+----------+--------------+------------+
```

## Field Objects
Each `Field` object contains at a minimum the following attributes:

* `Value` (string or number depending on the field type):
  Corresponds to the field value. Can be `null` if no value was extracted.
* `Confidence` (Float):
  The confidence score of the field prediction.
* `Polygon` (List<List<double>>):
  Contains the relative vertices coordinates (points) of a polygon containing the field in the image.

## Extracted Fields
Attributes that will be extracted from the document and available in the `Receipt` object:

- [Category](#category)
- [Date](#date)
- [Locale](#locale)
- [Supplier Information](#supplier-information)
- [Taxes](#taxes)
- [Time](#time)
- [Tip](#tip)
- [Total Amounts](#total-amounts)

### Category
* **`Category`** (StringField): Receipt category as seen on the receipt.
List of supported categories supported: https://developers.mindee.com/docs/receipt-ocr#category.
* **`SubCategory`** (StringField): More precise subcategory.
List of supported subcategories supported: https://developers.mindee.com/docs/receipt-ocr#subcategory.
* **`DocumentType`** (StringField): Is a classification field of the receipt.
  The document types supported: https://developers.mindee.com/docs/receipt-ocr#document-type

### Date
Date fields:
* contain the `raw` attribute, which is the textual representation found on the document.
* have a `value` attribute which is the [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601) representation of the date, regardless of the `raw` contents.

The following date fields are available:
* **`Date`**: Date the receipt was issued

### Locale
**`Locale`** (Locale): Locale information.

* `Locale.Language` (String): Language code in [ISO 639-1](https://en.wikipedia.org/wiki/ISO_639-1) format as seen on the document.
  The following language codes are supported: `ca`, `de`, `en`, `es`, `fr`, `it`, `nl` and `pt`.

* `Locale.Currency` (String): Currency code in [ISO 4217](https://en.wikipedia.org/wiki/ISO_4217) format as seen on the document.
  The following country codes are supported: `CAD`, `CHF`, `GBP`, `EUR`, `USD`.

* `Locale.Country` (String): Country code in [ISO 3166-1](https://en.wikipedia.org/wiki/ISO_3166-1) alpha-2 format as seen on the document.
  The following country codes are supported: `CA`, `CH`, `DE`, `ES`, `FR,` `GB`, `IT`, `NL`, `PT` and `US`.

### Supplier Information
* **`Supplier`** (StringField): Supplier name as written in the receipt.

### Taxes
**`Taxes`** (Array< TaxField >): Contains tax fields as seen on the receipt.

* `Value` (Float): The tax amount.
* `Code` (String): The tax code (HST, GST... for Canadian; City Tax, State tax for US, etc..).
* `Rate` (Float): The tax rate.
* `basis` (Float): The tax base.

### Time
* **`Time`**: Time of purchase as seen on the receipt
    * `Value` (string): Time of purchase with 24 hours formatting (hh:mm).
    * `Raw` (string): In any format as seen on the receipt.

### Tip
**`Tip`** (AmountField): Total amount of tip and gratuity.


### Total Amounts
* **`TotalAmount`** (AmountField): Total amount including taxes and tips

* **`TotalNet`** (AmountField): Total amount paid excluding taxes and tip

* **`TotalTax`** (AmountField): Total tax value from tax lines


&nbsp;
&nbsp;
**Questions?**
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
