The .NET OCR SDK supports the [passport API](https://developers.mindee.com/docs/passport-ocr) for extracting data from passports.

Using this [sample passport](https://files.readme.io/4a16b1d-passport_pic.jpg) below, we are going to illustrate how to extract the data that we want using the  OCR SDK.
![sample passport](https://files.readme.io/4a16b1d-passport_pic.jpg)

## Quick Start
```csharp
// Load a file from disk and parse it

string path = "./4a16b1d-passport_pic.jpg";
var prediction = await _mindeeClient
                    .LoadDocument(File.OpenRead(Path), System.IO.Path.GetFileName(Path))
                    .ParseAsync<PassportV1Prediction>();

// Print a summary of the parsed data
System.Console.WriteLine(prediction.Inference.Prediction.ToString());
```

Output:
```
-----Passport data-----
Full name: HENERT PUDARSAN
Given names: HENERT
Surname: PUDARSAN
Country: GBR
ID Number: 707797979
Issuance date: 2012-04-22
Birth date: 1995-05-20
Expiry date: 2017-04-22
MRZ 1: P<GBRPUDARSAN<<HENERT<<<<<<<<<<<<<<<<<<<<<<<
MRZ 2: 7077979792GBR9505209M1704224<<<<<<<<<<<<<<00
MRZ: P<GBRPUDARSAN<<HENERT<<<<<<<<<<<<<<<<<<<<<<<7077979792GBR9505209M1704224<<<<<<<<<<<<<<00
----------------------
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

* **`birthPlace`** (Field): Passport owner birthplace.

### Country
* **`country`** (Field): Passport country in [ISO 3166-1 alpha-3 code format](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-3) (3 letters code).

### Dates
Date fields:
* can contain the `raw` attribute, which is the textual representation found on the document.
* have a `value` attribute which is the [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601)  representation of the date, regardless of the `raw` contents.

The following date fields are available:
- **`expiry_date`**: Passport expiry date.

- **`issuance_date`**: Passport date of issuance.

- **`birth_date`**: Passport's owner date of birth.

### Gender

- **`gender`** (Field): Passport owner's gender (M / F).

### Given Names

* **`givenNames`** (Array< Field >): List of passport owner's given names.

### ID

* **`id_number`** (Field): Passport identification number.

### Machine Readable Zone

* **`mrz1`** (Field): Passport first line of machine-readable zone.

* **`mrz2`** (Field): Passport second line of machine-readable zone.

### Surname
* **`surname`** (Field): Passport's owner surname.

&nbsp;
&nbsp;
**Questions?**
<img alt="Slack Logo Icon" style="display:inline!important" src="https://files.readme.io/5b83947-Slack.png" width="20" height="20">&nbsp;&nbsp;[Join our Slack](https://slack.mindee.com)
