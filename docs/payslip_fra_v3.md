---
title: FR Payslip OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-fr-payslip-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Payslip API](https://platform.mindee.com/mindee/payslip_fra).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/payslip_fra/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Payslip sample](https://github.com/mindee/client-lib-test-data/blob/main/products/payslip_fra/default_sample.jpg?raw=true)

# Quick-Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Fr.Payslip;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the product asynchronously with auto-polling
var response = await mindeeClient
    .EnqueueAndParseAsync<PayslipV3>(inputSource);

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
:Mindee ID: a479e3e7-6838-4e82-9a7d-99289f34ec7f
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/payslip_fra v3.0
:Rotation applied: Yes

Prediction
==========
:Pay Period:
  :End Date: 2023-03-31
  :Month: 03
  :Payment Date: 2023-03-29
  :Start Date: 2023-03-01
  :Year: 2023
:Employee:
  :Address: 52 RUE DES FLEURS 33500 LIBOURNE FRANCE
  :Date of Birth:
  :First Name: Jean Luc
  :Last Name: Picard
  :Phone Number:
  :Registration Number:
  :Social Security Number: 123456789012345
:Employer:
  :Address: 1 RUE DU TONNOT 25210 DOUBS
  :Company ID: 12345678901234
  :Company Site:
  :NAF Code: 1234A
  :Name: DEMO COMPANY
  :Phone Number:
  :URSSAF Number:
:Bank Account Details:
  :Bank Name:
  :IBAN:
  :SWIFT:
:Employment:
  :Category: Cadre
  :Coefficient: 600,000
  :Collective Agreement: Construction -- Promotion
  :Job Title: Directeur Régional du Développement
  :Position Level: Niveau 5 Echelon 3
  :Seniority Date:
  :Start Date: 2022-05-01
:Salary Details:
  +--------------+-----------+--------------------------------------+--------+-----------+
  | Amount       | Base      | Description                          | Number | Rate      |
  +==============+===========+======================================+========+===========+
  | 6666.67      |           | Salaire de base                      |        |           |
  +--------------+-----------+--------------------------------------+--------+-----------+
  | 9.30         |           | Part patronale Mutuelle NR           |        |           |
  +--------------+-----------+--------------------------------------+--------+-----------+
  | 508.30       |           | Avantages en nature voiture          |        |           |
  +--------------+-----------+--------------------------------------+--------+-----------+
:Pay Detail:
  :Gross Salary: 7184.27
  :Gross Salary YTD: 18074.81
  :Income Tax Rate: 17.60
  :Income Tax Withheld: 1030.99
  :Net Paid: 3868.32
  :Net Paid Before Tax: 4899.31
  :Net Taxable: 5857.90
  :Net Taxable YTD: 14752.73
  :Total Cost Employer: 10486.94
  :Total Taxes and Deductions: 1650.36
:Paid Time Off:
  +-----------+--------+-------------+-----------+-----------+
  | Accrued   | Period | Type        | Remaining | Used      |
  +===========+========+=============+===========+===========+
  |           | N-1    | VACATION    |           |           |
  +-----------+--------+-------------+-----------+-----------+
  | 6.17      | N      | VACATION    | 6.17      |           |
  +-----------+--------+-------------+-----------+-----------+
  | 2.01      | N      | RTT         | 2.01      |           |
  +-----------+--------+-------------+-----------+-----------+
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

## Specific Fields
Fields which are specific to this product; they are not used in any other product.

### Bank Account Details Field
Information about the employee's bank account.

A `PayslipV3BankAccountDetail` implements the following attributes:

* **BankName** (`string`): The name of the bank.
* **Iban** (`string`): The IBAN of the bank account.
* **Swift** (`string`): The SWIFT code of the bank.
Fields which are specific to this product; they are not used in any other product.

### Employee Field
Information about the employee.

A `PayslipV3Employee` implements the following attributes:

* **Address** (`string`): The address of the employee.
* **DateOfBirth** (`string`): The date of birth of the employee.
* **FirstName** (`string`): The first name of the employee.
* **LastName** (`string`): The last name of the employee.
* **PhoneNumber** (`string`): The phone number of the employee.
* **RegistrationNumber** (`string`): The registration number of the employee.
* **SocialSecurityNumber** (`string`): The social security number of the employee.
Fields which are specific to this product; they are not used in any other product.

### Employer Field
Information about the employer.

A `PayslipV3Employer` implements the following attributes:

* **Address** (`string`): The address of the employer.
* **CompanyId** (`string`): The company ID of the employer.
* **CompanySite** (`string`): The site of the company.
* **NafCode** (`string`): The NAF code of the employer.
* **Name** (`string`): The name of the employer.
* **PhoneNumber** (`string`): The phone number of the employer.
* **UrssafNumber** (`string`): The URSSAF number of the employer.
Fields which are specific to this product; they are not used in any other product.

### Employment Field
Information about the employment.

A `PayslipV3Employment` implements the following attributes:

* **Category** (`string`): The category of the employment.
* **Coefficient** (`string`): The coefficient of the employment.
* **CollectiveAgreement** (`string`): The collective agreement of the employment.
* **JobTitle** (`string`): The job title of the employee.
* **PositionLevel** (`string`): The position level of the employment.
* **SeniorityDate** (`string`): The seniority date of the employment.
* **StartDate** (`string`): The start date of the employment.
Fields which are specific to this product; they are not used in any other product.

### Paid Time Off Field
Information about paid time off.

A `PayslipV3PaidTimeOff` implements the following attributes:

* **Accrued** (`double`): The amount of paid time off accrued in the period.
* **Period** (`string`): The paid time off period.

#### Possible values include:
 - N
 - N-1
 - N-2

* **PtoType** (`string`): The type of paid time off.

#### Possible values include:
 - VACATION
 - RTT
 - COMPENSATORY

* **Remaining** (`double`): The remaining amount of paid time off at the end of the period.
* **Used** (`double`): The amount of paid time off used in the period.
Fields which are specific to this product; they are not used in any other product.

### Pay Detail Field
Detailed information about the pay.

A `PayslipV3PayDetail` implements the following attributes:

* **GrossSalary** (`double`): The gross salary of the employee.
* **GrossSalaryYtd** (`double`): The year-to-date gross salary of the employee.
* **IncomeTaxRate** (`double`): The income tax rate of the employee.
* **IncomeTaxWithheld** (`double`): The income tax withheld from the employee's pay.
* **NetPaid** (`double`): The net paid amount of the employee.
* **NetPaidBeforeTax** (`double`): The net paid amount before tax of the employee.
* **NetTaxable** (`double`): The net taxable amount of the employee.
* **NetTaxableYtd** (`double`): The year-to-date net taxable amount of the employee.
* **TotalCostEmployer** (`double`): The total cost to the employer.
* **TotalTaxesAndDeductions** (`double`): The total taxes and deductions of the employee.
Fields which are specific to this product; they are not used in any other product.

### Pay Period Field
Information about the pay period.

A `PayslipV3PayPeriod` implements the following attributes:

* **EndDate** (`string`): The end date of the pay period.
* **Month** (`string`): The month of the pay period.
* **PaymentDate** (`string`): The date of payment for the pay period.
* **StartDate** (`string`): The start date of the pay period.
* **Year** (`string`): The year of the pay period.
Fields which are specific to this product; they are not used in any other product.

### Salary Details Field
Detailed information about the earnings.

A `PayslipV3SalaryDetail` implements the following attributes:

* **Amount** (`double`): The amount of the earning.
* **Base** (`double`): The base rate value of the earning.
* **Description** (`string`): The description of the earnings.
* **Number** (`double`): The number of units in the earning.
* **Rate** (`double`): The rate of the earning.

# Attributes
The following fields are extracted for Payslip V3:

## Bank Account Details
**BankAccountDetails**([PayslipV3BankAccountDetail](#bank-account-details-field)): Information about the employee's bank account.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.BankAccountDetails.Value);
```

## Employee
**Employee**([PayslipV3Employee](#employee-field)): Information about the employee.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Employee.Value);
```

## Employer
**Employer**([PayslipV3Employer](#employer-field)): Information about the employer.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Employer.Value);
```

## Employment
**Employment**([PayslipV3Employment](#employment-field)): Information about the employment.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Employment.Value);
```

## Paid Time Off
**PaidTimeOff**(List<[PayslipV3PaidTimeOff](#paid-time-off-field)>): Information about paid time off.

```csharp
foreach (var PaidTimeOffElem in result.Document.Inference.Prediction.PaidTimeOff)
{
    System.Console.WriteLine(PaidTimeOffElem.Value);
}
```

## Pay Detail
**PayDetail**([PayslipV3PayDetail](#pay-detail-field)): Detailed information about the pay.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.PayDetail.Value);
```

## Pay Period
**PayPeriod**([PayslipV3PayPeriod](#pay-period-field)): Information about the pay period.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.PayPeriod.Value);
```

## Salary Details
**SalaryDetails**(List<[PayslipV3SalaryDetail](#salary-details-field)>): Detailed information about the earnings.

```csharp
foreach (var SalaryDetailsElem in result.Document.Inference.Prediction.SalaryDetails)
{
    System.Console.WriteLine(SalaryDetailsElem.Value);
}
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
