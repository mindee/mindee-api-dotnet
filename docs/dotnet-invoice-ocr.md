The .NET OCR SDK supports the [invoice API](https://developers.mindee.com/docs/invoice-ocr) for extracting data from invoices.

Using this [sample invoice](https://files.readme.io/a74eaa5-c8e283b-sample_invoice.jpeg) below, we are going to illustrate how to extract the data that we want, using the OCR SDK.
![sample invoice](https://files.readme.io/a74eaa5-c8e283b-sample_invoice.jpeg)

## Quick Start
```csharp
// Load a file from disk and parse it

string path = "./a74eaa5-c8e283b-sample_invoice.jpeg";
var prediction = await _mindeeClient
    .LoadDocument(File.OpenRead(Path), System.IO.Path.GetFileName(Path))
    .ParseAsync<InvoiceV3Prediction>();

// Print a summary of the parsed data
System.Console.WriteLine(prediction.Inference.Prediction.ToString());
```

Output:
```
-----Invoice data-----
Invoice number: 14
Total amount including taxes: 2608.2
Total amount excluding taxes: 2415.0
Invoice date: 2018-09-25
Invoice due date: 2018-09-25
Supplier name: TURNPIKE DESIGNS CO.
Supplier address: 156 University Ave, Toronto ON, Canada M5H 2H7
Customer name: JIRO DOI
Customer company registration: 
Customer address: 1954 Bloon Street West Toronto, ON, M6P 3K9 Canada
Payment details: 
Company numbers: 
Taxes: 193.2 8.0%
Total taxes: 193.2
Locale: en; en; CAD;
----------------------
```

## Properties
Each properties of the object contains at a minimum the following attributes:

* `value` (string or double depending on the field type):
  Corresponds to the field value. Can be `null` if no value was extracted.
* `confidence` (a `double`):
  The confidence score of the field prediction.
* `polygon` (a `List<List<double>>`):
  Contains the relative vertices coordinates (points) of a polygon containing the field in the image.

## Extracted Fields
Attributes that will be extracted from the document and available in the `Invoice` object:

- [Customer Information](#customer-information)
- [Dates](#dates)
- [Locale and Currency](#locale)
- [Payment Information](#payment-information)
- [Supplier Information](#supplier-information)
- [Taxes](#taxes)
- [Total Amounts](#total-amounts)

### Customer Information
* **`customerName`** (Field): Customer's name

* **`customerAddress`** (Field): Customer's postal address

* **`customerCompanyRegistration`** (List<CompanyRegistration>): Customer's company registration

### Dates
Date fields:
* contain the `raw` attribute, which is the textual representation found on the document.
* have a `value` attribute which is the [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601) representation of the date, regardless of the `raw` contents.

The following date fields are available:

* **`date`**: Date the invoice was issued

* **`dueDate`**: Payment due date of the invoice.

### Locale
**`locale`** (Locale): Locale information.

* `locale.language` (string): Language code in [ISO 639-1](https://en.wikipedia.org/wiki/ISO_639-1) format as seen on the document.
  The following language codes are supported: `ca`, `de`, `en`, `es`, `fr`, `it`, `nl` and `pt`.

* `locale.currency` (string): Currency code in [ISO 4217](https://en.wikipedia.org/wiki/ISO_4217) format as seen on the document.
  The following country codes are supported: `CAD`, `CHF`, `GBP`, `EUR`, `USD`.

* `locale.country` (string): Country code in [ISO 3166-1](https://en.wikipedia.org/wiki/ISO_3166-1) alpha-2 format as seen on the document.
  The following country codes are supported: `CA`, `CH`, `DE`, `ES`, `FR,` `GB`, `IT`, `NL`, `PT` and `US`.

### Payment Information
**`paymentDetails`** (List<PaymentDetails>): List of invoice's supplier payment details. Each object in the list contains extra attributes:
* `iban` (string)
* `swift` (string)
* `routingNumber` (string)
* `accountNumber` (string)

### Supplier Information

**`companyRegistration`** (List<CompanyRegistration>):  List of detected supplier's company registration numbers. Each object in the list contains an extra attribute:

* `type` (String): Type of company registration number among: [VAT NUMBER](https://en.wikipedia.org/wiki/VAT_identification_number), [SIRET](https://en.wikipedia.org/wiki/SIRET_code), [SIREN](https://en.wikipedia.org/wiki/SIREN_code), [NIF](https://en.wikipedia.org/wiki/National_identification_number), [CF](https://en.wikipedia.org/wiki/Italian_fiscal_code), [UID](https://en.wikipedia.org/wiki/VAT_identification_number), [STNR](https://de.wikipedia.org/wiki/Steuernummer), [HRA/HRB](https://en.wikipedia.org/wiki/German_Commercial_Register), [TIN](https://en.wikipedia.org/wiki/Taxpayer_Identification_Number) (includes EIN, FEIN, SSN, ATIN, PTIN, ITIN), [RFC](https://wise.com/us/blog/clabe-rfc-curp-abm-meaning-mexico), [BTW](https://en.wikipedia.org/wiki/European_Union_value_added_tax), [ABN](https://abr.business.gov.au/Help/AbnFormat), [UEN](https://www.uen.gov.sg/ueninternet/faces/pages/admin/aboutUEN.jspx), [CVR](https://en.wikipedia.org/wiki/Central_Business_Register_(Denmark)), [ORGNR](https://en.wikipedia.org/wiki/VAT_identification_number), [INN](https://www.nalog.gov.ru/eng/exchinf/inn/), [DPH](https://en.wikipedia.org/wiki/Value-added_tax), [GSTIN](https://en.wikipedia.org/wiki/VAT_identification_number), [COMPANY REGISTRATION NUMBER](https://en.wikipedia.org/wiki/VAT_identification_number) (UK), [KVK](https://business.gov.nl/starting-your-business/registering-your-business/lei-rsin-vat-and-kvk-number-which-is-which/), [DIC](https://www.vatify.eu/czech-vat-number.html)

* `value` (string): Value of the company identifier

* **`supplier`**: Supplier name as written in the invoice (logo or supplier Info).

* **`supplierAddress`**: Supplier address as written in the invoice.

### Taxes
**`taxes`** (List<TaxField>): Contains tax fields as seen on the receipt.

* `value` (double): The tax amount.
* `code` (string): The tax code (HST, GST... for Canadian; City Tax, State tax for US, etc..).
* `rate` (double): The tax rate.

### Total Amounts

* **`totalIncl`** (Field): Total amount including taxes.

* **`totalExcl`** (Field): Total amount excluding taxes.

* **`totalTax`** (Field): Total tax value from tax lines.

&nbsp;
&nbsp;
**Questions?**
<img alt="Slack Logo Icon" style="display:inline!important" src="https://files.readme.io/5b83947-Slack.png" width="20" height="20">&nbsp;&nbsp;[Join our Slack](https://slack.mindee.com)
