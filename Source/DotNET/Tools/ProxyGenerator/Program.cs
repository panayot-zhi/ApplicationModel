// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cratis.Applications.ProxyGenerator;

Console.WriteLine("Cratis Proxy Generator\n");

if (args.Length != 2)
{
    Console.WriteLine("Usage: ");
    Console.WriteLine("  Cratis.ProxyGenerator <assembly> <output-path>");
    return 1;
}

var assemblyFile = args[0];
var outputPath = Path.GetFullPath(args[0]);

var result = await Generator.Generate(
    assemblyFile,
    outputPath,
    Console.WriteLine,
    Console.Error.WriteLine);
return result ? 0 : 1;