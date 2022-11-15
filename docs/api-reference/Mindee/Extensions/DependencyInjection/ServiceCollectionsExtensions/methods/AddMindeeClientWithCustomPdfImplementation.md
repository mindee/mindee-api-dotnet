﻿<!--  
  <auto-generated>   
    The contents of this file were generated by a tool.  
    Changes to this file may be list if the file is regenerated  
  </auto-generated>   
-->

# ServiceCollectionsExtensions.AddMindeeClientWithCustomPdfImplementation Method

**Declaring Type:** [ServiceCollectionsExtensions](../index.md)  
**Namespace:** [Mindee.Extensions.DependencyInjection](../../index.md)  
**Assembly:** Mindee  
**Assembly Version:** 1.0.0\-rc1

Configure the Mindee client in the DI with your own custom pdf implementation.

```csharp
public static IServiceCollection AddMindeeClientWithCustomPdfImplementation<TPdfOperationImplementation>(this IServiceCollection services);
```

## Type Parameters

`TPdfOperationImplementation`

Will be registered as a singleton.

## Parameters

`services`  IServiceCollection

## Remarks

The [MindeeClient](../../../../MindeeClient/index.md) instance is registered as a transient.

## Returns

IServiceCollection

___

*Documentation generated by [MdDocs](https://github.com/ap0llo/mddocs)*