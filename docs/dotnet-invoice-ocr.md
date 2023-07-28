The .NET OCR SDK supports the [invoice API](https://developers.mindee.com/docs/invoice-ocr) for extracting data from invoices.

Using this [sample invoice](https://files.readme.io/a74eaa5-c8e283b-sample_invoice.jpeg) below, we are going to illustrate how to extract the data that we want, using the OCR SDK.
![sample invoice](https://files.readme.io/a74eaa5-c8e283b-sample_invoice.jpeg)

## Quick Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Invoice;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<InvoiceV4>(inputSource);

// Print a summary of all the predictions
// System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

Output:
```
:Locale: en; en; CAD;
:Document type: INVOICE
:Invoice number: 14
:Reference numbers: AD29094
:Invoice date: 2018-09-25
:Invoice due date: 2018-09-25
:Supplier name: TURNPIKE DESIGNS CO.
:Supplier address: 156 University Ave, Toronto ON, Canada M5H 2H7
:Supplier company registrations:
:Supplier payment details:
:Customer name: JIRO DOI
:Customer address: 1954 Bloon Street West Toronto, ON, M6P 3K9 Canada
:Customer company registrations:
:Taxes:
  +---------------+--------+----------+---------------+
  | Base          | Code   | Rate (%) | Amount        |
  +===============+========+==========+===============+
  |               |        | 8.00     | 193.20        |
  +---------------+--------+----------+---------------+
:Total net:
:Total tax: 193.20
:Total amount: 2608.20
:Line Items:
  +----------------------+---------+---------+----------+------------------+--------------------------------------+
  | Code                 | QTY     | Price   | Amount   | Tax (Rate)       | Description                          |
  +======================+=========+=========+==========+==================+======================================+
  |                      | 1.00    | 65.00   | 65.00    |                  | Platinum web hosting package Down... |
  +----------------------+---------+---------+----------+------------------+--------------------------------------+
  |                      | 3.00    | 2100.00 | 2100.00  |                  | 2 page website design Includes ba... |
  +----------------------+---------+---------+----------+------------------+--------------------------------------+
  |                      | 1.00    | 250.00  | 250.00   |                  | Mobile designs Includes responsiv... |
  +----------------------+---------+---------+----------+------------------+--------------------------------------+
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
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
