# Mindee .NET API Library Changelog

## v3.18.3 - 2024-07-23
### Fixes
* :bug: fix broken reference on worker service initialization


## v3.18.2 - 2024-07-22
### Fixes
*  :bug: fix RestSharp's restclient being given improper scope for use in workers


## v3.18.1 - 2024-07-19
### Fixes
*  :bug: fix issue that prevented the API from working in workers without manually adding RestClient as a dependency.


## v3.18.0 - 2024-07-18
### Changes
* :sparkles: add support for US Healthcare card
* :sparkles: add support for Us Mail V2
* :sparkles: update financial document to v1.9
* :sparkles: update invoice to v4.7

### Fixes
* :bug: fix restsharp dependency issue (#219)
* :bug: fix uncommon bug where api calls would sometime stay opened for too long & cause timeouts
* :bug: fix image not loading properly when using the PDF Extraction feature
* :recycle: better notifications in case of hanging connections
* :arrow_up: update System.Text.Json to cover vulnerability
* :recycle: update many tests


## v3.17.0 - 2024-06-26
### Changes
* :sparkles: add support for invoice splitter auto-extraction
* :sparkles: add support for multi-receipts auto-extraction
* :sparkles: add support for getting the raw OCR as a string in CLI

### Fixes
* :bug: fix pdf indexes being improperly flagged as invalid in some splitPdf configurations
* :recycle: fix company registration display to include version
* :recycle: update image/pdf handling & System.text.json dependencies


## v3.16.0 - 2024-06-10
### Changes
* :sparkles: add support for getting the raw OCR as a string


## v3.15.0 - 2024-05-28
### Changes
* :sparkles: add support for US Mail


## v3.14.0 - 2024-05-16
### Changes
* :sparkles: update receipt to 5.2 and financial document to 1.7


## v3.13.0 - 2024-04-30
### Changes
* :sparkles: update invoice to 4.6 and financial doc to 1.6


## v3.12.0 - 2024-04-08
### Changes
* :sparkles: add support for loading webhook responses


## v3.11.1 - 2024-04-05
### Changes
* :zap: increase max retries in CLI to 60

### Fixes
* :bug: fix for writing custom classes to JSON


## v3.11.0 - 2024-03-26
### Changes
* :white_check_mark: compatibility with .NET 8.0
* :sparkles: update Invoice to v4.5
* :sparkles: add support for generated documents


## v3.10.0 - 2024-03-06
### Changes
* :sparkles: add support for error in job processing
* :sparkles: add resume v1
* :sparkles: add EU driver license v1
* :sparkles: add International ID v2
* :sparkles: add FR Carte Grise v1


## v3.9.0 - 2024-01-30
### Changes
* :sparkles: add RawValue to string fields
* :arrow_up: update invoice to 4.4


## v3.8.0 - 2023-12-20
### Changes
* :recycle tweak default async sample delays & retry
* :sparkles: add International ID


## v3.7.0 - 2023-11-22
### Changes
* :sparkles: add ability to parse URLs

### Fixes
* :bug: properly throw exceptions on predict_async API route


## v3.6.0 - 2023-11-17
### Changes
* :sparkles: add NPages property to Document class
* :sparkles: add pageId to custom doc values


## v3.5.0 - 2023-11-13
### Changes
* :sparkles: add support for us payroll check register


## v3.4.0 - 2023-10-20
### Changes
* :sparkles: add string helper functions to custom documents

### Fixes
* :bug: add missing confidence and polygon data to line items


## v3.3.0 - 2023-09-22
### Changes
* :recycle: simplify async product example
* :sparkles: add raw string response in response object
* :loud_sound: add global logging system
* :sparkles: add support for barcode reader v1
* :sparkles: add support for multi receipt detector v1

### Fixes
* :bug: fix for parsing invalid dates
* :bug: fix for empty elements in PositionField


## v3.2.0 - 2023-09-13
### Changes
* :sparkles: add support for FR ID card v2
* :sparkles: add support for US W9 v1
* :sparkles: add built-in async call polling
* :sparkles: add DecimalField
* :loud_sound: better logging
* :sparkles: add a dateObject property to DateField class

### Fixes
* :bug: fix for alignment issues on ToString
* :bug: StringField value should be null in case of empty string


## v3.1.0 - 2023-08-08
### Changes
* :sparkles: add support for US driver license v1


## v3.0.0 - 2023-07-28
### ¡Breaking Changes!
* :art: :boom: harmonize response type with other libraries
* :art: :boom: change appsettings main key to 'Mindee'
* :art: :boom: separate HTTP classes, move products to Product namespace
* :fire: :art: :boom: init of client should be done directly
* :recycle: :art: :boom: send inputSource instances directly to parse methods
* :recycle: :boom: seal product classes
* :recycle: :boom: use PredictOptions object to allow more flexibility in the client API

### Changes
* :coffin: remove shipping container
* :sparkles: support async and invoice splitter v1
* :art: rework CLI
* :sparkles: update to latest version of passport, invoice, receipt, financial doc
* :sparkles: add support for fr bank account details v2
* :white_check_mark: add tests for empty responses
* :white_check_mark: add more relevent integration tests

### Fixes
* :bug: fix page output in us bank check


## v2.2.1 - 2023-05-30
### Changes
* :arrow_up: update RestSharp to 109.0.1


## v2.2.0 - 2023-03-16
### Changes
* :children_crossing: Improve the UX when in CLI
* :memo: prediction class documentation updates
* :sparkles: add support for FR bank account details
* :sparkles: adding summary for table in custom document
* :recycle: major rework of line items detection and generation

### Fixes
* :bug: fix running sample code


## v2.1.2 - 2023-03-09
### Fixes
* :bug: custom document default version must be '1', not '1.0'


## v2.1.1 - 2023-03-08
### Fixes
* :bug: fix trouble with .NET 4.8/4.7.2 usage


## v2.1.0 - 2023-02-20
### ¡Breaking Changes!
* :boom: some properties renaming for French ID card and Carte Vitale

### Changes
* :sparkles: support lines items post processing reconstruction for API Builder
* :sparkles: support Polygon object for field coordinates
* :sparkles: support Math Bbox concept

### Fixes
* :bug::boom: details property in Error become an object because it can handle both string and object
* :bug::arrow_up: update RestSharp to fix a bug on .NET 7 #120 


## v2.0.0 - 2023-02-02
### ¡Breaking Changes!
* :boom: drop Invoice v3 support
* :boom: prediction model classes must be replaced by inference model to use OTS APIs
* :boom: renaming Date to DateField

### Changes
* :sparkles: support Financial Document V1
* :sparkles: support Proof of Address V1
* :sparkles: enable the use of custom time out when calling Mindee API
* :sparkles: improve MindeeClient by using IPredictable instead of instance of MindeeApi
* :sparkles: expose a static helper to instantiate a new MindeeClient
* :sparkles: improve summaries
* :sparkles: support classification type for custom document
* :recycle: renaming AllWord to Word

### Fixes
* :bug:! Details property in Error become an object because it can handle both string and object


## v1.4.0 - 2023-01-04
### Changes
* :sparkles: Support Receipt V4.1
* :sparkles: Support Invoice V4.1


## v1.3.0 - 2022-12-22

### Changes
* :white_check_mark: Cropper API summary
* :sparkles: Adding total taxes amount in Invoice V3 and V4
* :sparkles: Adding Value in Locale for Receipt
* :sparkles: Add Mrz and FullName on Passport
* :sparkles: Support FR ID Card V1
* :sparkles: Support Shipping Container V1
* :sparkles: Support US Bank Check V1
* :sparkles: Support EU License plates V1


## v1.2.0 - 2022-12-07
### Changes
* :sparkles: Add Invoice V4 support


## v1.1.0 - 2022-11-28
### Changes
* :boom: Changing the settings section name from MindeeApiSettings to MindeeSettings.
* :boom: IConfiguration is replaced by IOptions<MindeeSettings> in MindeeClient.
* :sparkles: Add cropper support
* :memo: Publish technical documentation on GitHub pages


## v1.0.0 - 2022-11-17
* :tada: First release
