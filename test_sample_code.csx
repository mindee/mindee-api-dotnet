#!/usr/bin/env dotnet-script
#r "nuget: Dotnet.Script.Core, 1.4.0"

using System;

string baseContent = File.ReadAllText("docs/base.csx");

foreach(var fileName in Directory.GetFiles("docs/code_samples", "*.txt", SearchOption.AllDirectories))
{
    string content = File.ReadAllText(fileName);
    content = content.Replace("my-api-key", Args[0]);
    content = content.Replace("/path/to/the/file.ext", "tests/Ressources/pdf/blank_1.pdf");

    File.WriteAllText("_test.csx", baseContent + content, new UTF8Encoding(false /* Linux shebang can't handle BOM */));

    var assemblyLoadContext = new ScriptAssemblyLoadContext();
    var fileCommandOptions = new ExecuteScriptCommandOptions(
        new ScriptFile(AppContext.BaseDirectory + "_test.csx"),
        new[] { "" },
        OptimizationLevel.Debug,
        null,
        false,
        false
    ) {
        AssemblyLoadContext = assemblyLoadContext
    };

    var fileCommand = new ExecuteScriptCommand(ScriptConsole.Default);
    var r = await fileCommand.Run<int, CommandLineScriptGlobals>(fileCommandOptions);

    Console.WriteLine(r);
}
