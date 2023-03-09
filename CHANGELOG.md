# CHANGELOG

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

* :sparkles: Add Invoice V4 support

## v1.1.0 - 2022-11-28

* :boom: Changing the settings section name from MindeeApiSettings to MindeeSettings.
* :boom: IConfiguration is replaced by IOptions<MindeeSettings> in MindeeClient.
* :sparkles: Add cropper support
* :memo: Publish technical documentation on GitHub pages

## v1.0.0 - 2022-11-17

* :tada: First release
