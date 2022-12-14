The .NET OCR SDK supports the [invoice API](https://developers.mindee.com/docs/invoice-ocr) for extracting data from invoices.

Using this [sample invoice](https://files.readme.io/a74eaa5-c8e283b-sample_invoice.jpeg) below, we are going to illustrate how to extract the data that we want, using the OCR SDK.
![sample invoice](https://files.readme.io/a74eaa5-c8e283b-sample_invoice.jpeg)

## Quick Start
```csharp
// Load a file from disk and parse it

string path = "./a74eaa5-c8e283b-sample_invoice.jpeg";
var prediction = await _mindeeClient
    .LoadDocument(File.OpenRead(Path), System.IO.Path.GetFileName(Path))
    .ParseAsync<InvoiceV4Prediction>();

// Print a summary of the parsed data
System.Console.WriteLine(prediction.Inference.Prediction.ToString());
```

Output:
```
----- Invoice V4 -----
Locale: fr; fr; EUR;
Invoice number: 0042004801351
Reference numbers: AD29094
Invoice date: 2020-02-17
Invoice due date: 2020-02-17
Supplier name: TURNPIKE DESIGNS CO.
Supplier address: 156 University Ave, Toronto ON, Canada M5H 2H7
Supplier company registrations: 501124705; FR33501124705
Supplier payment details: FR7640254025476501124705368;
Customer name: JIRO DOI
Customer company registrations: FR00000000000; 111222333
Customer address: 1954 Bloon Street West Toronto, ON, M6P 3K9 Canada
Line Items:
  Code           | QTY    | Price   | Amount   | Tax (Rate)     | Description
                 |        |         | 4.31     |  (2.1 %)       | PQ20 ETIQ ULTRA RESIS METAXXDC
                 | 1.0    | 65.0    | 75.0     | 10.0           | Platinum web hosting package Dow...
  XXX81125600010 | 1.0    | 250.01  | 275.51   | 25.5 (10.2 %)  | a long string describing the ite...
  ABC456         | 200.3  | 8.101   | 1622.63  | 121.7 (7.5 %)  | Liquid perfection
                 |        |         |          |                | CARTOUCHE L NR BROTHER TN247BK
Taxes: 97.98 20.0%
Total taxes: 97.98
Total amount excluding taxes: 489.97
Total amount including taxes: 587.95
----------------------
```

## Properties
Each properties of the object contains at a minimum the following attributes:

* `Value` (string or double depending on the field type):
  Corresponds to the field value. Can be `null` if no value was extracted.
* `Confidence` (a `double`):
  The confidence score of the field prediction.
* `Polygon` (a `List<List<double>>`):
  Contains the relative vertices coordinates (points) of a polygon containing the field in the image.

## Extracted Fields
Attributes that will be extracted from the document and available in the `Invoice` object:

- [Customer Information](#customer-information)
- [Dates](#dates)
- [Locale and Currency](#locale)
- [Reference numbers](#reference-numbers)
- [Supplier Information](#supplier-information)
- [Line items](#line-items)
- [Taxes](#taxes)
- [Total Amounts](#total-amounts)

### Customer Information
* **`CustomerName`** (StringField): Customer's name

* **`CustomerAddress`** (StringField): Customer's postal address

* **`CustomerCompanyRegistrations`** (List<CompanyRegistration>): Customer's company registration

### Dates
Date fields:
* contain the `raw` attribute, which is the textual representation found on the document.
* have a `value` attribute which is the [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601) representation of the date, regardless of the `raw` contents.

The following date fields are available:

* **`Date`**: Date the invoice was issued

* **`DueDate`**: Payment due date of the invoice.

### Locale
**`Locale`** (Locale): Locale information.

* `Locale.Language` (string): Language code in [ISO 639-1](https://en.wikipedia.org/wiki/ISO_639-1) format as seen on the document.
  The following language codes are supported: `ca`, `de`, `en`, `es`, `fr`, `it`, `nl` and `pt`.

* `Locale.Currency` (string): Currency code in [ISO 4217](https://en.wikipedia.org/wiki/ISO_4217) format as seen on the document.
  The following country codes are supported: `CAD`, `CHF`, `GBP`, `EUR`, `USD`.

* `Locale.Country` (string): Country code in [ISO 3166-1](https://en.wikipedia.org/wiki/ISO_3166-1) alpha-2 format as seen on the document.
  The following country codes are supported: `CA`, `CH`, `DE`, `ES`, `FR,` `GB`, `IT`, `NL`, `PT` and `US`.

### Reference numbers
* `ReferenceNumbers` (List<StringField>) : Represents a list of Reference numbers including PO number.

### Supplier Information

**`SupplierCompanyRegistrations`** (List<CompanyRegistration>):  List of detected supplier's company registration numbers. Each object in the list contains an extra attribute:

* `Type` (string): Type of company registration number among: [VAT NUMBER](https://en.wikipedia.org/wiki/VAT_identification_number), [SIRET](https://en.wikipedia.org/wiki/SIRET_code), [SIREN](https://en.wikipedia.org/wiki/SIREN_code), [NIF](https://en.wikipedia.org/wiki/National_identification_number), [CF](https://en.wikipedia.org/wiki/Italian_fiscal_code), [UID](https://en.wikipedia.org/wiki/VAT_identification_number), [STNR](https://de.wikipedia.org/wiki/Steuernummer), [HRA/HRB](https://en.wikipedia.org/wiki/German_Commercial_Register), [TIN](https://en.wikipedia.org/wiki/Taxpayer_Identification_Number) (includes EIN, FEIN, SSN, ATIN, PTIN, ITIN), [RFC](https://wise.com/us/blog/clabe-rfc-curp-abm-meaning-mexico), [BTW](https://en.wikipedia.org/wiki/European_Union_value_added_tax), [ABN](https://abr.business.gov.au/Help/AbnFormat), [UEN](https://www.uen.gov.sg/ueninternet/faces/pages/admin/aboutUEN.jspx), [CVR](https://en.wikipedia.org/wiki/Central_Business_Register_(Denmark)), [ORGNR](https://en.wikipedia.org/wiki/VAT_identification_number), [INN](https://www.nalog.gov.ru/eng/exchinf/inn/), [DPH](https://en.wikipedia.org/wiki/Value-added_tax), [GSTIN](https://en.wikipedia.org/wiki/VAT_identification_number), [COMPANY REGISTRATION NUMBER](https://en.wikipedia.org/wiki/VAT_identification_number) (UK), [KVK](https://business.gov.nl/starting-your-business/registering-your-business/lei-rsin-vat-and-kvk-number-which-is-which/), [DIC](https://www.vatify.eu/czech-vat-number.html)

* `Value` (string): Value of the company identifier

* **`SupplierName`**: Supplier name as written in the invoice (logo or supplier Info).

* **`SupplierAddress`**: Supplier address as written in the invoice.

* **`SupplierPaymentDetails`** (List<PaymentDetails>): List of invoice's supplier payment details. Each object in the list contains extra attributes:
* `Iban` (string)
* `Swift` (string)
* `RoutingNumber` (string)
* `AccountNumber` (string)

### Line items

**`LineItems`** (List<InvoiceLineItem>):  Line items details. Each object in the list contains :
* `ProductCode` (string)
* `Description` (string)
* `Quantity` (double)
* `UnitPrice` (double)
* `TotalAmount` (double)
* `TaxRate` (double)
* `TaxAmount` (double)
* `Confidence` (double)
* `PageId` (double)
* `Polygon` (List<List<double>>)


### Taxes
**`Taxes`** (List<TaxField>): Contains tax fields as seen on the receipt.

* `Value` (double): The tax amount.
* `Code` (string): The tax code (HST, GST... for Canadian; City Tax, State tax for US, etc..).
* `Rate` (double): The tax rate.

### Total Amounts

* **`TotalAmount`** (AmountField): Total amount including taxes.

* **`TotalNet`** (AmountField): Total amount excluding taxes.

&nbsp;
&nbsp;
**Questions?**
<img alt="Slack Logo Icon" style="display:inline!important" src="https://files.readme.io/5b83947-Slack.png" width="20" height="20">&nbsp;&nbsp;[Join our Slack](https://slack.mindee.com)
