The .NET OCR SDK supports the [invoice API](https://developers.mindee.com/docs/invoice-ocr) for extracting data from invoices.

Using this [sample invoice](https://files.readme.io/a74eaa5-c8e283b-sample_invoice.jpeg) below, we are going to illustrate how to extract the data that we want, using the OCR SDK.
![sample invoice](https://files.readme.io/a74eaa5-c8e283b-sample_invoice.jpeg)

## Quick Start
```csharp
using Mindee;
using Mindee.Product.Invoice;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

MindeeClient mindeeClient = new MindeeClient(apiKey);

var response = await mindeeClient
    .LoadDocument(File.OpenRead(filePath), System.IO.Path.GetFileName(filePath))
    .ParseAsync<InvoiceV4>();

// Print a summary of the predictions
System.Console.WriteLine(response.Document.ToString());

// Print the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

Output:
```
########
Document
########
:Mindee ID: 656c2ec1-0920-4556-9bc2-772162bc698a
:Filename: invoice.pdf

Inference
#########
:Product: mindee/invoices v4.1
:Rotation applied: Yes

Prediction
==========
:Locale: fr; fr; EUR;
:Document type: INVOICE
:Invoice number: 0042004801351
:Reference numbers: AD29094
:Invoice date: 2020-02-17
:Invoice due date: 2020-02-17
:Supplier name: TURNPIKE DESIGNS CO.
:Supplier address: 156 University Ave, Toronto ON, Canada M5H 2H7
:Supplier company registrations: 501124705; FR33501124705
:Supplier payment details: FR7640254025476501124705368;
:Customer name: JIRO DOI
:Customer address: 1954 Bloon Street West Toronto, ON, M6P 3K9 Canada
:Customer company registrations: FR00000000000; 111222333
:Taxes: 97.98 20.00%
:Total net: 489.97
:Total taxes: 97.98
:Total amount: 587.95

:Line Items:
====================== ======== ========= ========== ================== ====================================
Code                   QTY      Price     Amount     Tax (Rate)         Description
====================== ======== ========= ========== ================== ====================================
                                          4.31        (2.10%)           PQ20 ETIQ ULTRA RESIS METAXXDC
                       1.00     65.00     75.00      10.00              Platinum web hosting package Down...
XXX81125600010         1.00     250.01    275.51     25.50 (10.20%)     a long string describing the item
ABC456                 200.30   8.101     1622.63    121.70 (7.50%)     Liquid perfection
                                                                        CARTOUCHE L NR BROTHER TN247BK
====================== ======== ========= ========== ================== ====================================

Page Predictions
================

Page 0
------
:Locale: fr; fr; EUR;
:Document type: INVOICE
:Invoice number: 0042004801351
:Reference numbers:
:Invoice date: 2020-02-17
:Invoice due date: 2020-02-17
:Supplier name:
:Supplier address:
:Supplier company registrations: 501124705; FR33501124705
:Supplier payment details: FR7640254025476501124705368;
:Customer name:
:Customer address:
:Customer company registrations:
:Taxes: 97.98 20.00%
:Total net: 489.97
:Total taxes: 97.98
:Total amount: 587.95

:Line Items:
====================== ======== ========= ========== ================== ====================================
Code                   QTY      Price     Amount     Tax (Rate)         Description
====================== ======== ========= ========== ================== ====================================
                                          4.31        (2.10%)           PQ20 ETIQ ULTRA RESIS METAXXDC
                       1.00     65.00     75.00      10.00              Platinum web hosting package Down...
====================== ======== ========= ========== ================== ====================================

Page 1
------
:Locale: fr; fr; EUR;
:Document type: INVOICE
:Invoice number:
:Reference numbers: AD29094
:Invoice date:
:Invoice due date: 2020-02-17
:Supplier name: TURNPIKE DESIGNS CO.
:Supplier address: 156 University Ave, Toronto ON, Canada M5H 2H7
:Supplier company registrations:
:Supplier payment details:
:Customer name: JIRO DOI
:Customer address: 1954 Bloon Street West Toronto, ON, M6P 3K9 Canada
:Customer company registrations:
:Taxes: 193.20 8.00%
:Total net:
:Total taxes: 193.20
:Total amount: 2608.20

:Line Items:
====================== ======== ========= ========== ================== ====================================
Code                   QTY      Price     Amount     Tax (Rate)         Description
====================== ======== ========= ========== ================== ====================================
XXX81125600010         1.00     250.00    250.00      (10.00%)          a long string describing the item
ABC456                 200.30   8.101     1622.63    121.70 (7.50%)     Liquid perfection
                                                                        CARTOUCHE L NR BROTHER TN247BK
====================== ======== ========= ========== ================== ====================================
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
