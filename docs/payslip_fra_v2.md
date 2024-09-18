---
title: FR Payslip OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-fr-payslip-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Payslip API](https://platform.mindee.com/mindee/payslip_fra).

The [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/payslip_fra/default_sample.jpg) can be used for testing purposes.
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
    .EnqueueAndParseAsync<PayslipV2>(inputSource);

// Print a summary of all the predictions
System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());

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

A `PayslipV2BankAccountDetail` implements the following attributes:

* **BankName** (`string`): The name of the bank.
* **Iban** (`string`): The IBAN of the bank account.
* **Swift** (`string`): The SWIFT code of the bank.
Fields which are specific to this product; they are not used in any other product.

### Employee Field
Information about the employee.

A `PayslipV2Employee` implements the following attributes:

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

A `PayslipV2Employer` implements the following attributes:

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

A `PayslipV2Employment` implements the following attributes:

* **Category** (`string`): The category of the employment.
* **Coefficient** (`double`): The coefficient of the employment.
* **CollectiveAgreement** (`string`): The collective agreement of the employment.
* **JobTitle** (`string`): The job title of the employee.
* **PositionLevel** (`string`): The position level of the employment.
* **StartDate** (`string`): The start date of the employment.
Fields which are specific to this product; they are not used in any other product.

### Pay Detail Field
Detailed information about the pay.

A `PayslipV2PayDetail` implements the following attributes:

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

A `PayslipV2PayPeriod` implements the following attributes:

* **EndDate** (`string`): The end date of the pay period.
* **Month** (`string`): The month of the pay period.
* **PaymentDate** (`string`): The date of payment for the pay period.
* **StartDate** (`string`): The start date of the pay period.
* **Year** (`string`): The year of the pay period.
Fields which are specific to this product; they are not used in any other product.

### PTO Field
Information about paid time off.

A `PayslipV2Pto` implements the following attributes:

* **AccruedThisPeriod** (`double`): The amount of paid time off accrued in this period.
* **BalanceEndOfPeriod** (`double`): The balance of paid time off at the end of the period.
* **UsedThisPeriod** (`double`): The amount of paid time off used in this period.
Fields which are specific to this product; they are not used in any other product.

### Salary Details Field
Detailed information about the earnings.

A `PayslipV2SalaryDetail` implements the following attributes:

* **Amount** (`double`): The amount of the earnings.
* **Base** (`double`): The base value of the earnings.
* **Description** (`string`): The description of the earnings.
* **Rate** (`double`): The rate of the earnings.

# Attributes
The following fields are extracted for Payslip V2:

## Bank Account Details
**BankAccountDetails**([PayslipV2BankAccountDetail](#bank-account-details-field)): Information about the employee's bank account.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.BankAccountDetails.Value);
```

## Employee
**Employee**([PayslipV2Employee](#employee-field)): Information about the employee.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Employee.Value);
```

## Employer
**Employer**([PayslipV2Employer](#employer-field)): Information about the employer.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Employer.Value);
```

## Employment
**Employment**([PayslipV2Employment](#employment-field)): Information about the employment.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Employment.Value);
```

## Pay Detail
**PayDetail**([PayslipV2PayDetail](#pay-detail-field)): Detailed information about the pay.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.PayDetail.Value);
```

## Pay Period
**PayPeriod**([PayslipV2PayPeriod](#pay-period-field)): Information about the pay period.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.PayPeriod.Value);
```

## PTO
**Pto**([PayslipV2Pto](#pto-field)): Information about paid time off.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Pto.Value);
```

## Salary Details
**SalaryDetails**(List<[PayslipV2SalaryDetail](#salary-details-field)>): Detailed information about the earnings.

```csharp
foreach (var SalaryDetailsElem in result.Document.Inference.Prediction.SalaryDetails)
{
    System.Console.WriteLine(SalaryDetailsElem.Value);
}
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
