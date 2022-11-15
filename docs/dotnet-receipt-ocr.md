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
----- Receipt V4 -----
Filename: 6882f91-receipt23.png
Total amount: 7.27
Total net: 6.86
Tip:
Date: 2022-04-03
Category: food
Time: 10:00
Supplier name: MINDEE TAKE OUT
Taxes: 0.41 TAX
Total taxes: 0.41
Locale: en-US; en; US; USD;
----------------------
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
* **`Category`** (Field): Receipt category as seen on the receipt.
  The following categories are supported: toll, food, parking, transport, accommodation, gasoline, miscellaneous.

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
<img alt="Slack Logo Icon" style="display:inline!important" src="https://files.readme.io/5b83947-Slack.png" width="20" height="20">&nbsp;&nbsp;[Join our Slack](https://slack.mindee.com)
