---
title: Resume OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-resume-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Resume API](https://platform.mindee.com/mindee/resume).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/resume/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Resume sample](https://github.com/mindee/client-lib-test-data/blob/main/products/resume/default_sample.jpg?raw=true)

# Quick-Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Resume;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the product asynchronously with auto-polling
var response = await mindeeClient
    .EnqueueAndParseAsync<ResumeV1>(inputSource);

// Print a summary of all the predictions
System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());

```

**Output (RST):**
```rst
########
Document
########
:Mindee ID: bc80bae0-af75-4464-95a9-2419403c75bf
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/resume v1.0
:Rotation applied: No

Prediction
==========
:Document Language: ENG
:Document Type: RESUME
:Given Names: Christopher
:Surnames: Morgan
:Nationality:
:Email Address: christoper.m@gmail.com
:Phone Number: +44 (0) 20 7666 8555
:Address: 177 Great Portland Street, London W5W 6PQ
:Social Networks:
  +----------------------+----------------------------------------------------+
  | Name                 | URL                                                |
  +======================+====================================================+
  | LinkedIn             | linkedin.com/christopher.morgan                    |
  +----------------------+----------------------------------------------------+
:Profession: Senior Web Developer
:Job Applied:
:Languages:
  +----------+----------------------+
  | Language | Level                |
  +==========+======================+
  | SPA      | Fluent               |
  +----------+----------------------+
  | ZHO      | Beginner             |
  +----------+----------------------+
  | DEU      | Intermediate         |
  +----------+----------------------+
:Hard Skills: HTML5
              PHP OOP
              JavaScript
              CSS
              MySQL
:Soft Skills: Project management
              Strong decision maker
              Innovative
              Complex problem solver
              Creative design
              Service-focused
:Education:
  +-----------------+---------------------------+-----------+----------+---------------------------+-------------+------------+
  | Domain          | Degree                    | End Month | End Year | School                    | Start Month | Start Year |
  +=================+===========================+===========+==========+===========================+=============+============+
  | Computer Inf... | Bachelor                  |           |          | Columbia University, NY   |             | 2014       |
  +-----------------+---------------------------+-----------+----------+---------------------------+-------------+------------+
:Professional Experiences:
  +-----------------+------------+---------------------------+-----------+----------+----------------------+-------------+------------+
  | Contract Type   | Department | Employer                  | End Month | End Year | Role                 | Start Month | Start Year |
  +=================+============+===========================+===========+==========+======================+=============+============+
  | Full-Time       |            | Luna Web Design, New York | 05        | 2019     | Web Developer        | 09          | 2015       |
  +-----------------+------------+---------------------------+-----------+----------+----------------------+-------------+------------+
:Certificates:
  +------------+--------------------------------+---------------------------+------+
  | Grade      | Name                           | Provider                  | Year |
  +============+================================+===========================+======+
  |            | PHP Framework (certificate)... |                           | 2014 |
  +------------+--------------------------------+---------------------------+------+
  |            | Programming Languages: Java... |                           |      |
  +------------+--------------------------------+---------------------------+------+
```

# Field Types
## Standard Fields
These fields are generic and used in several products.

### BaseField
Each prediction object contains a set of fields that inherit from the generic `BaseField` class.
A typical `BaseField` object will have the following attributes:

* **Confidence** (`double?`): the confidence score of the field prediction.
* **BoundingBox** (`BoundingBox`): contains exactly 4 relative vertices (points) coordinates of a right rectangle containing the field in the document.
* **Polygon** (`Polygon`): contains the relative vertices coordinates (`Polygon` extends `List<Point>`) of a polygon containing the field in the image.
* **PageId** (`int?`): the ID of the page, always `null` when at document-level.

> **Note:** A `Point` simply refers to a List of `double`.


Aside from the previous attributes, all basic fields have access to a custom `ToString` method that can be used to print their value as a string.


### ClassificationField
The classification field `ClassificationField` extends `BaseField`, but also implements:
* **Value** (`strong`): corresponds to the field value.

> Note: a classification field's `value is always a `string`.

### StringField
The text field `StringField` extends `BaseField`, but also implements:
* **Value** (`string`): corresponds to the field value.
* **RawValue** (`string`): corresponds to the raw value as it appears on the document.

## Specific Fields
Fields which are specific to this product; they are not used in any other product.

### Certificates Field
The list of certificates obtained by the candidate.

A `ResumeV1Certificate` implements the following attributes:

* **Grade** (`string`): The grade obtained for the certificate.
* **Name** (`string`): The name of certification.
* **Provider** (`string`): The organization or institution that issued the certificate.
* **Year** (`string`): The year when a certificate was issued or received.
Fields which are specific to this product; they are not used in any other product.

### Education Field
The list of the candidate's educational background.

A `ResumeV1Education` implements the following attributes:

* **DegreeDomain** (`string`): The area of study or specialization.
* **DegreeType** (`string`): The type of degree obtained, such as Bachelor's, Master's, or Doctorate.
* **EndMonth** (`string`): The month when the education program or course was completed.
* **EndYear** (`string`): The year when the education program or course was completed.
* **School** (`string`): The name of the school.
* **StartMonth** (`string`): The month when the education program or course began.
* **StartYear** (`string`): The year when the education program or course began.
Fields which are specific to this product; they are not used in any other product.

### Languages Field
The list of languages that the candidate is proficient in.

A `ResumeV1Language` implements the following attributes:

* **Language** (`string`): The language's ISO 639 code.
* **Level** (`string`): The candidate's level for the language.
Fields which are specific to this product; they are not used in any other product.

### Professional Experiences Field
The list of the candidate's professional experiences.

A `ResumeV1ProfessionalExperience` implements the following attributes:

* **ContractType** (`string`): The type of contract for the professional experience.
* **Department** (`string`): The specific department or division within the company.
* **Employer** (`string`): The name of the company or organization.
* **EndMonth** (`string`): The month when the professional experience ended.
* **EndYear** (`string`): The year when the professional experience ended.
* **Role** (`string`): The position or job title held by the candidate.
* **StartMonth** (`string`): The month when the professional experience began.
* **StartYear** (`string`): The year when the professional experience began.
Fields which are specific to this product; they are not used in any other product.

### Social Networks Field
The list of social network profiles of the candidate.

A `ResumeV1SocialNetworksUrl` implements the following attributes:

* **Name** (`string`): The name of the social network.
* **Url** (`string`): The URL of the social network.

# Attributes
The following fields are extracted for Resume V1:

## Address
**Address**: The location information of the candidate, including city, state, and country.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Address.Value);
```

## Certificates
**Certificates**(List<[ResumeV1Certificate](#certificates-field)>): The list of certificates obtained by the candidate.

```csharp
foreach (var CertificatesElem in result.Document.Inference.Prediction.Certificates)
{
    System.Console.WriteLine(CertificatesElem.Value);
}
```

## Document Language
**DocumentLanguage**: The ISO 639 code of the language in which the document is written.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.DocumentLanguage.Value);
```

## Document Type
**DocumentType**: The type of the document sent.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.DocumentType.Value);
```

## Education
**Education**(List<[ResumeV1Education](#education-field)>): The list of the candidate's educational background.

```csharp
foreach (var EducationElem in result.Document.Inference.Prediction.Education)
{
    System.Console.WriteLine(EducationElem.Value);
}
```

## Email Address
**EmailAddress**: The email address of the candidate.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.EmailAddress.Value);
```

## Given Names
**GivenNames**: The candidate's first or given names.

```csharp
foreach (var GivenNamesElem in result.Document.Inference.Prediction.GivenNames)
{
    System.Console.WriteLine(GivenNamesElem.Value);
}
```

## Hard Skills
**HardSkills**: The list of the candidate's technical abilities and knowledge.

```csharp
foreach (var HardSkillsElem in result.Document.Inference.Prediction.HardSkills)
{
    System.Console.WriteLine(HardSkillsElem.Value);
}
```

## Job Applied
**JobApplied**: The position that the candidate is applying for.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.JobApplied.Value);
```

## Languages
**Languages**(List<[ResumeV1Language](#languages-field)>): The list of languages that the candidate is proficient in.

```csharp
foreach (var LanguagesElem in result.Document.Inference.Prediction.Languages)
{
    System.Console.WriteLine(LanguagesElem.Value);
}
```

## Nationality
**Nationality**: The ISO 3166 code for the country of citizenship of the candidate.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Nationality.Value);
```

## Phone Number
**PhoneNumber**: The phone number of the candidate.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.PhoneNumber.Value);
```

## Profession
**Profession**: The candidate's current profession.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Profession.Value);
```

## Professional Experiences
**ProfessionalExperiences**(List<[ResumeV1ProfessionalExperience](#professional-experiences-field)>): The list of the candidate's professional experiences.

```csharp
foreach (var ProfessionalExperiencesElem in result.Document.Inference.Prediction.ProfessionalExperiences)
{
    System.Console.WriteLine(ProfessionalExperiencesElem.Value);
}
```

## Social Networks
**SocialNetworksUrls**(List<[ResumeV1SocialNetworksUrl](#social-networks-field)>): The list of social network profiles of the candidate.

```csharp
foreach (var SocialNetworksUrlsElem in result.Document.Inference.Prediction.SocialNetworksUrls)
{
    System.Console.WriteLine(SocialNetworksUrlsElem.Value);
}
```

## Soft Skills
**SoftSkills**: The list of the candidate's interpersonal and communication abilities.

```csharp
foreach (var SoftSkillsElem in result.Document.Inference.Prediction.SoftSkills)
{
    System.Console.WriteLine(SoftSkillsElem.Value);
}
```

## Surnames
**Surnames**: The candidate's last names.

```csharp
foreach (var SurnamesElem in result.Document.Inference.Prediction.Surnames)
{
    System.Console.WriteLine(SurnamesElem.Value);
}
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
