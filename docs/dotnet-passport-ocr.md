The .NET OCR SDK supports the [passport API](https://developers.mindee.com/docs/passport-ocr) for extracting data from passports.

Using this [sample passport](https://files.readme.io/4a16b1d-passport_pic.jpg) below, we are going to illustrate how to extract the data that we want using the  OCR SDK.
![sample passport](https://files.readme.io/4a16b1d-passport_pic.jpg)

## Quick Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Passport;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<PassportV1>(inputSource);

// Print a summary of all the predictions
// System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

Output:
```
:Country Code: GBR
:ID Number: 707797979
:Given Name(s): HENERT
:Surname: PUDARSAN
:Date of Birth: 1995-05-20
:Place of Birth: CAMTETH
:Gender: M
:Date of Issue: 2012-04-22
:Expiry Date: 2017-04-22
:MRZ Line 1: P<GBRPUDARSAN<<HENERT<<<<<<<<<<<<<<<<<<<<<<<
:MRZ Line 2: 7077979792GBR9505209M1704224<<<<<<<<<<<<<<00
```

## Field Objects
Each `Field` object contains at a minimum the following attributes:

* `value` (string or number depending on the field type):
  Corresponds to the field value. Can be `null` if no value was extracted.
* `confidence` (Float):
  The confidence score of the field prediction.
* `bbox` (Array< Array< Float > >):
  Contains exactly 4 relative vertices coordinates (points) of a right rectangle containing the field in the document.

## Extracted Fields
Depending on the field type specified, additional attributes can be extracted from the `Passport` object.

Using the above [passport example](https://files.readme.io/4a16b1d-passport_pic.jpg), the following are the basic fields that can be extracted.

- [Birth Place](#birth-place)
- [Country](#country)
- [Dates (Expiry, Issuance, Birth)](#dates)
- [Gender](#gender)
- [Given Names](#given-names)
- [ID Number](#id)
- [Machine Readable Zone](#machine-readable-zone)
- [Surname](#surname)

### Birth Place

* **`BirthPlace`** (Field): Passport owner birthplace.

### Country
* **`Country`** (Field): Passport country in [ISO 3166-1 alpha-3 code format](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-3) (3 letters code).

### Dates
Date fields:
* can contain the `raw` attribute, which is the textual representation found on the document.
* have a `value` attribute which is the [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601) representation of the date, regardless of the `raw` contents.

The following date fields are available:
- **`ExpiryDate`**: Passport expiry date.

- **`IssuanceDate`**: Passport date of issuance.

- **`BirthDate`**: Passport's owner date of birth.

### Gender

- **`Gender`** (StringField): Passport owner's gender (M / F).

### Given Names

* **`GivenNames`** (List<StringField>): List of passport owner's given names.

### ID

* **`IdNumber`** (StringField): Passport identification number.

### Machine Readable Zone

* **`Mrz1`** (StringField): Passport first line of machine-readable zone.

* **`Mrz2`** (StringField): Passport second line of machine-readable zone.

### Surname
* **`Surname`** (StringField): Passport's owner surname.

&nbsp;
&nbsp;
**Questions?**
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
