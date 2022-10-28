The .NET OCR SDK supports the [receipt API](https://developers.mindee.com/docs/receipt-ocr) for extracting data from receipts.

Using this [sample receipt](https://files.readme.io/ffc127d-sample_receipt.jpg) below, we are going to illustrate how to extract the data that we want using the OCR SDK.
![sample receipt](https://files.readme.io/ffc127d-sample_receipt.jpg)

## Quick Start
```csharp
// Load a file from disk and parse it

string path = "./ffc127d-sample_receipt.jpeg";
var prediction = await _mindeeClient
                    .LoadDocument(File.OpenRead(Path), System.IO.Path.GetFileName(Path))
                    .ParseAsync<ReceiptV3Prediction>();

// Print a summary of the parsed data
System.Console.WriteLine(prediction.Inference.Prediction.ToString());
```

Output:
```
-----Receipt data-----
Total amount including taxes: 10.2
Total amount excluding taxes: 8.5
Date: 2016-02-26
Category: food
Time: 15:20
Merchant name: CLACHAN
Taxes: 1.7 20.0%
Total taxes: 1.7
Locale: en; GB; GBP;
---------------------
```

## Field Objects
Each `Field` object contains at a minimum the following attributes:

* `value` (string or number depending on the field type):
  Corresponds to the field value. Can be `null` if no value was extracted.
* `confidence` (Float):
  The confidence score of the field prediction.
* `polygon` (List<List<double>>):
  Contains the relative vertices coordinates (points) of a polygon containing the field in the image.

## Extracted Fields
Attributes that will be extracted from the document and available in the `Receipt` object:

- [Category](#category)
- [Date](#date)
- [Locale](#locale)
- [Supplier Information](#supplier-information)
- [Taxes](#taxes)
- [Time](#time)
- [Total Amounts](#total-amounts)

### Category
* **`category`** (Field): Receipt category as seen on the receipt.
  The following categories are supported: toll, food, parking, transport, accommodation, gasoline, miscellaneous.

### Date
Date fields:
* contain the `raw` attribute, which is the textual representation found on the document.
* have a `value` attribute which is the [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601) representation of the date, regardless of the `raw` contents.

The following date fields are available:
* **`date`**: Date the receipt was issued

### Locale
**`locale`** (Locale): Locale information.

* `locale.language` (String): Language code in [ISO 639-1](https://en.wikipedia.org/wiki/ISO_639-1) format as seen on the document.
  The following language codes are supported: `ca`, `de`, `en`, `es`, `fr`, `it`, `nl` and `pt`.

* `locale.currency` (String): Currency code in [ISO 4217](https://en.wikipedia.org/wiki/ISO_4217) format as seen on the document.
  The following country codes are supported: `CAD`, `CHF`, `GBP`, `EUR`, `USD`.

* `locale.country` (String): Country code in [ISO 3166-1](https://en.wikipedia.org/wiki/ISO_3166-1) alpha-2 format as seen on the document.
  The following country codes are supported: `CA`, `CH`, `DE`, `ES`, `FR,` `GB`, `IT`, `NL`, `PT` and `US`.

### Supplier Information
* **`supplier`** (Field): Supplier name as written in the receipt.

### Taxes
**`taxes`** (Array< TaxField >): Contains tax fields as seen on the receipt.

* `value` (Float): The tax amount.
* `code` (String): The tax code (HST, GST... for Canadian; City Tax, State tax for US, etc..).
* `rate` (Float): The tax rate.

### Time
* **`time`**: Time of purchase as seen on the receipt
    * `value` (string): Time of purchase with 24 hours formatting (hh:mm).
    * `raw` (string): In any format as seen on the receipt.

### Total Amounts
* **`totalIncl`** (Field): Total amount including taxes

* **`totalExcl`** (Field): Total amount paid excluding taxes

* **`totalTax`** (Field): Total tax value from tax lines

&nbsp;
&nbsp;
**Questions?**
<img alt="Slack Logo Icon" style="display:inline!important" src="https://files.readme.io/5b83947-Slack.png" width="20" height="20">&nbsp;&nbsp;[Join our Slack](https://slack.mindee.com)
