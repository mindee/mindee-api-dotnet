# Mindee .NET Client Library Changelog


## v3.37.2 - 2026-01-19
### Fixes
* :bug: fix some older .NET versions not compiling properly


## v3.37.1 - 2026-01-15
### Changes
* :broom: add code quality checks & reformat entire codebase


## v3.37.0 - 2026-01-06
### Changes
* :sparkles: add loading local input source from base64
* :sparkles: add support for data schema parameter


## v3.36.1 - 2025-12-15
### Changes
* :white_check_mark: add initial testing on .NET 10
* :white_check_mark: test loading webP files
### Fixes
* :bug: allow retrieval of the inference ID


## v3.36.0 - 2025-12-02
### Changes
* :sparkles: add support for text_context in active options return


## v3.35.0 - 2025-11-19
### Changes
* :memo: update readme links
* :sparkles: add RAG metadata in V2 results
* :sparkles: improved v2 error messages and exceptions
* :sparkles: add text_context option on V2 enqueue
### Fixes
* :bug: fix for C# 14 and .NET 10


## v3.34.0 - 2025-10-01
### Changes
* :loud_sound: better logging for v2
* :sparkles: allow comparing v2 field confidence score


## v3.33.0 - 2025-09-25
### Fixes
* :bug: fix for setting inference options
* :bug: ensure consistent number formatting
### ¡Breaking Changes!
* :coffin: remove obsolete US Mail V2
* :bug: make sure DI uses separate services for v1 and v2 clients
    * :recycle: :boom: Use dedicated settings class for v2 (normally not used directly)
    * :recycle: :boom: Use dedicated settings section for v2 (normally not used directly)


## v3.32.0 - 2025-09-03
### Changes
* :sparkles: add inference options
### ¡Breaking Changes!
* :recycle: :boom: update raw text output from server


## v3.31.0 - 2025-08-27
### Changes
* :sparkles: add typed properties to list and object
### Fixes
* :memo: fix page options doc


## v3.30.0 - 2025-08-04
### Changes
* :sparkles: add support for page count & mime type, fix v2 field naming conventions
### Fixes
* :bug: fix for field attributes being at the wrong level


## v3.29.0 - 2025-07-29
### Changes
* :sparkles: Add support for mindee API V2 client & features
* :wrench: Tweak CI & testing
* :recycle: Uniformize variable naming across files


## v3.29.0-rc4 - 2025-07-22
### Changes
* :sparkles: add support for field locations & confidence score
* :recycle: change name of main calling methods (e.g. `EnqueueAndParseAsync` becomes `EnqueueAndGetInferenceAsync`)
* :sparkles: :boom: upgrade page options usage to match other SDKs (page indexes now start at 0)
### Fixes
* :bug: fix tests
* :recycle: update internal syntaxes


## v3.29.0-rc3 - 2025-07-09
### Changes
* :sparkles: merge enqueue parameters into a single object
### Fixes
* :recycle: `field` property of `ObjectField`s now deserializes into an `InferenceFields` instance.
* :recycle: remove redundant logic
* :memo: fix documentation typos
* :recycle: fix code sample


## v3.29.0-rc2 - 2025-07-03
### Changes
* :sparkles: add __very__ basic printing of results
### Fix
* :coffin: remove unneeded V2 classes
* :recycle: use an abstract class for the API to harmonize JSON parsing, logging
* :bug: use correct route for GET on job


## v3.29.0-rc1 - 2025-06-30
### Changes
* :alembic: add experimental support for V2 API
* :alembic: add experimental support for V2 inferences
* :memo: add sample codes
* :wrench: add proper logging in V2 HTTP calls
* :arrow_up: bump all MS dependencies
* :recycle: tweak the way sanity testing is done
### Fix
* :recycle: fix some issues with logging
* :arrow_up: update some testing dependencies
* :recycle: add fallback support for logging on older windows installs


## v3.28.0 - 2025-06-03
### Changes
* :sparkles: add support for address fields
* :sparkles: add support for Financial Document V1.12
* :sparkles: add support for Invoices V4.10
* :sparkles: add support for US Healthcare Cards V1.2
* :recycle: add support for nullable confidence scores


## v3.27.0 - 2025-04-22
### Changes
* :sparkles: add support for workflow polling (#304)
### Fixes
* Fix improper deserialization of date formats in workflow execution.


## v3.26.0 - 2025-04-16
### Changes
* :sparkles: add support for RAG parameter in workflow executions (#299)


## v3.25.0 - 2025-04-08
### Changes
* :sparkles: add support for Financial Document V1.12
* :sparkles: add support for Invoices V4.10
* :sparkles: add support for US Healthcare Cards V1.2
* :sparkles: add support for FR EnergyBill V1.2
* :sparkles: allow asynchronous custom products in CLI
* :coffin: deprecate EU License Plate
* :coffin: deprecate EU Driver License
* :coffin: deprecate Proof of Address
* :coffin: deprecate US W9
* :coffin: deprecate US Driver License
### Fixes
* :recycle: upgrade descriptions, documentation & CLI products
* :recycle: update data structure for Invoice Splitter


## v3.24.3 - 2025-02-13
### Changes
* :recycle: harmonise platform names with other client libs
* :arrow_up: add official support for .NET 9.0


## v3.24.2 - 2025-01-09
### Changes
* :recycle: increase async retry timers
### Fixes
* :bug: fix classifications not having a confidence score


## v3.24.1 - 2025-01-08
### Changes
* :memo: update reference documentation
### Fixes
* :bug: fix missing boolean accessor for generated object


## v3.24.0 - 2024-12-26
### Changes
* :sparkles: add support for us mail v3


## v3.23.0 - 2024-12-12
### Changes
* :sparkles: allow local downloading of remote sources
* :coffin: remove support for (FR) Carte Vitale V1 in favor of French Health Card V1


## v3.22.0 - 2024-11-28
### Changes
* :sparkles: add support for workflows
* :sparkles: add support for French Health Card V1
* :sparkles: add support for Driver License V1
* :sparkles: add support for Payslip FR V3
* :coffin: remove support for international id V1


## v3.21.0 - 2024-11-14
### Changes
* :sparkles: add support for business cards V1
* :sparkles: add support for delivery note V1.1
* :sparkles: add support for indian passport V1
* :sparkles: add support for resume V1.1
### Fixes
* :recycle: adjust default values for async delays


## v3.20.1 - 2024-10-21
### Fixes
:bug: fix worker services losing API settings & API key after an HTTP call


## v3.20.0 - 2024-10-11
### Changes
* :sparkles: add support for Financial Document v1.10
* :sparkles: add support for Invoice v4.8
* :sparkles: add support for image compression & resize
* :sparkles: add support for PDF compression
### Fixes
* :arrow_up: bump dependencies


## v3.19.0 - 2024-09-18
### Changes
* :sparkles: add support for BillOfLadingV1
* :sparkles: add support for (US) UsMailV2
* :sparkles: add support for (FR) EnergyBillV1
* :sparkles: add support for (FR) PayslipV1
* :sparkles: add support for NutritionFactsLabelV1
* :sparkles: add support for full text OCR response
### Fixes
* :bug: fixed a bug that prevented longer decimals from appearing in the string representation of some objects
* :bug: fixed a bug that caused non-table elements to unexpectedly appear truncated when printed to the console
* :memo: add classification detail to guide documentation


## v3.18.4 - 2024-08-30
### Changes
* :arrow_up: update Restsharp dependency to cover vulnerability
* :recycle: remove unused code


## v3.18.3 - 2024-07-23
### Fixes
* :bug: fix broken reference on worker service initialization


## v3.18.2 - 2024-07-22
### Fixes
* :bug: fix RestSharp's restclient being given improper scope for use in workers


## v3.18.1 - 2024-07-19
### Fixes
* :bug: fix issue that prevented the API from working in workers without manually adding RestClient as a dependency.


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
