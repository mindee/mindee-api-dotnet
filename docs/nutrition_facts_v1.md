---
title: Nutrition Facts Label OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-nutrition-facts-label-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Nutrition Facts Label API](https://platform.mindee.com/mindee/nutrition_facts).

The [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/nutrition_facts/default_sample.jpg) can be used for testing purposes.
![Nutrition Facts Label sample](https://github.com/mindee/client-lib-test-data/blob/main/products/nutrition_facts/default_sample.jpg?raw=true)

# Quick-Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.NutritionFactsLabel;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the product asynchronously with auto-polling
var response = await mindeeClient
    .EnqueueAndParseAsync<NutritionFactsLabelV1>(inputSource);

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

### AmountField
An amount field `AmountField` extends `BaseField`, but also implements:
* **Value** (`double?`): corresponds to the field value. Can be `null` if no value was extracted.

## Specific Fields
Fields which are specific to this product; they are not used in any other product.

### Added Sugars Field
The amount of added sugars in the product.

A `NutritionFactsLabelV1AddedSugar` implements the following attributes:

* **DailyValue** (`double`): DVs are the recommended amounts of added sugars to consume or not to exceed each day.
* **Per100G** (`double`): The amount of added sugars per 100g of the product.
* **PerServing** (`double`): The amount of added sugars per serving of the product.
Fields which are specific to this product; they are not used in any other product.

### Calories Field
The amount of calories in the product.

A `NutritionFactsLabelV1Calorie` implements the following attributes:

* **DailyValue** (`double`): DVs are the recommended amounts of calories to consume or not to exceed each day.
* **Per100G** (`double`): The amount of calories per 100g of the product.
* **PerServing** (`double`): The amount of calories per serving of the product.
Fields which are specific to this product; they are not used in any other product.

### Cholesterol Field
The amount of cholesterol in the product.

A `NutritionFactsLabelV1Cholesterol` implements the following attributes:

* **DailyValue** (`double`): DVs are the recommended amounts of cholesterol to consume or not to exceed each day.
* **Per100G** (`double`): The amount of cholesterol per 100g of the product.
* **PerServing** (`double`): The amount of cholesterol per serving of the product.
Fields which are specific to this product; they are not used in any other product.

### Dietary Fiber Field
The amount of dietary fiber in the product.

A `NutritionFactsLabelV1DietaryFiber` implements the following attributes:

* **DailyValue** (`double`): DVs are the recommended amounts of dietary fiber to consume or not to exceed each day.
* **Per100G** (`double`): The amount of dietary fiber per 100g of the product.
* **PerServing** (`double`): The amount of dietary fiber per serving of the product.
Fields which are specific to this product; they are not used in any other product.

### nutrients Field
The amount of nutrients in the product.

A `NutritionFactsLabelV1Nutrient` implements the following attributes:

* **DailyValue** (`double`): DVs are the recommended amounts of nutrients to consume or not to exceed each day.
* **Name** (`string`): The name of nutrients of the product.
* **Per100G** (`double`): The amount of nutrients per 100g of the product.
* **PerServing** (`double`): The amount of nutrients per serving of the product.
* **Unit** (`string`): The unit of measurement for the amount of nutrients.
Fields which are specific to this product; they are not used in any other product.

### Protein Field
The amount of protein in the product.

A `NutritionFactsLabelV1Protein` implements the following attributes:

* **DailyValue** (`double`): DVs are the recommended amounts of protein to consume or not to exceed each day.
* **Per100G** (`double`): The amount of protein per 100g of the product.
* **PerServing** (`double`): The amount of protein per serving of the product.
Fields which are specific to this product; they are not used in any other product.

### Saturated Fat Field
The amount of saturated fat in the product.

A `NutritionFactsLabelV1SaturatedFat` implements the following attributes:

* **DailyValue** (`double`): DVs are the recommended amounts of saturated fat to consume or not to exceed each day.
* **Per100G** (`double`): The amount of saturated fat per 100g of the product.
* **PerServing** (`double`): The amount of saturated fat per serving of the product.
Fields which are specific to this product; they are not used in any other product.

### Serving Size Field
The size of a single serving of the product.

A `NutritionFactsLabelV1ServingSize` implements the following attributes:

* **Amount** (`double`): The amount of a single serving.
* **Unit** (`string`): The unit for the amount of a single serving.
Fields which are specific to this product; they are not used in any other product.

### sodium Field
The amount of sodium in the product.

A `NutritionFactsLabelV1Sodium` implements the following attributes:

* **DailyValue** (`double`): DVs are the recommended amounts of sodium to consume or not to exceed each day.
* **Per100G** (`double`): The amount of sodium per 100g of the product.
* **PerServing** (`double`): The amount of sodium per serving of the product.
* **Unit** (`string`): The unit of measurement for the amount of sodium.
Fields which are specific to this product; they are not used in any other product.

### Total Carbohydrate Field
The total amount of carbohydrates in the product.

A `NutritionFactsLabelV1TotalCarbohydrate` implements the following attributes:

* **DailyValue** (`double`): DVs are the recommended amounts of total carbohydrates to consume or not to exceed each day.
* **Per100G** (`double`): The amount of total carbohydrates per 100g of the product.
* **PerServing** (`double`): The amount of total carbohydrates per serving of the product.
Fields which are specific to this product; they are not used in any other product.

### Total Fat Field
The total amount of fat in the product.

A `NutritionFactsLabelV1TotalFat` implements the following attributes:

* **DailyValue** (`double`): DVs are the recommended amounts of total fat to consume or not to exceed each day.
* **Per100G** (`double`): The amount of total fat per 100g of the product.
* **PerServing** (`double`): The amount of total fat per serving of the product.
Fields which are specific to this product; they are not used in any other product.

### Total Sugars Field
The total amount of sugars in the product.

A `NutritionFactsLabelV1TotalSugar` implements the following attributes:

* **DailyValue** (`double`): DVs are the recommended amounts of total sugars to consume or not to exceed each day.
* **Per100G** (`double`): The amount of total sugars per 100g of the product.
* **PerServing** (`double`): The amount of total sugars per serving of the product.
Fields which are specific to this product; they are not used in any other product.

### Trans Fat Field
The amount of trans fat in the product.

A `NutritionFactsLabelV1TransFat` implements the following attributes:

* **DailyValue** (`double`): DVs are the recommended amounts of trans fat to consume or not to exceed each day.
* **Per100G** (`double`): The amount of trans fat per 100g of the product.
* **PerServing** (`double`): The amount of trans fat per serving of the product.

# Attributes
The following fields are extracted for Nutrition Facts Label V1:

## Added Sugars
**AddedSugars**([NutritionFactsLabelV1AddedSugar](#added-sugars-field)): The amount of added sugars in the product.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.AddedSugars.Value);
```

## Calories
**Calories**([NutritionFactsLabelV1Calorie](#calories-field)): The amount of calories in the product.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Calories.Value);
```

## Cholesterol
**Cholesterol**([NutritionFactsLabelV1Cholesterol](#cholesterol-field)): The amount of cholesterol in the product.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Cholesterol.Value);
```

## Dietary Fiber
**DietaryFiber**([NutritionFactsLabelV1DietaryFiber](#dietary-fiber-field)): The amount of dietary fiber in the product.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.DietaryFiber.Value);
```

## nutrients
**Nutrients**(List<[NutritionFactsLabelV1Nutrient](#nutrients-field)>): The amount of nutrients in the product.

```csharp
foreach (var NutrientsElem in result.Document.Inference.Prediction.Nutrients)
{
    System.Console.WriteLine(NutrientsElem.Value);
}
```

## Protein
**Protein**([NutritionFactsLabelV1Protein](#protein-field)): The amount of protein in the product.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Protein.Value);
```

## Saturated Fat
**SaturatedFat**([NutritionFactsLabelV1SaturatedFat](#saturated-fat-field)): The amount of saturated fat in the product.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.SaturatedFat.Value);
```

## Serving per Box
**ServingPerBox**: The number of servings in each box of the product.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.ServingPerBox.Value);
```

## Serving Size
**ServingSize**([NutritionFactsLabelV1ServingSize](#serving-size-field)): The size of a single serving of the product.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.ServingSize.Value);
```

## sodium
**Sodium**([NutritionFactsLabelV1Sodium](#sodium-field)): The amount of sodium in the product.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Sodium.Value);
```

## Total Carbohydrate
**TotalCarbohydrate**([NutritionFactsLabelV1TotalCarbohydrate](#total-carbohydrate-field)): The total amount of carbohydrates in the product.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.TotalCarbohydrate.Value);
```

## Total Fat
**TotalFat**([NutritionFactsLabelV1TotalFat](#total-fat-field)): The total amount of fat in the product.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.TotalFat.Value);
```

## Total Sugars
**TotalSugars**([NutritionFactsLabelV1TotalSugar](#total-sugars-field)): The total amount of sugars in the product.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.TotalSugars.Value);
```

## Trans Fat
**TransFat**([NutritionFactsLabelV1TransFat](#trans-fat-field)): The amount of trans fat in the product.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.TransFat.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
