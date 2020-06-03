/*
 
MetroFramework - Modern UI for WinForms

Copyright (c) 2013 Jens Thiel, http://thielj.github.io/MetroFramework
Portions of this software are Copyright (c) 2011 Sven Walter, http://github.com/viperneo

Permission is hereby granted, free of charge, to any person obtaining a copy of 
this software and associated documentation files (the "Software"), to deal in the 
Software without restriction, including without limitation the rights to use, copy, 
modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
and to permit persons to whom the Software is furnished to do so, subject to the 
following conditions:

The above copyright notice and this permission notice shall be included in 
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 
 */

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle(MetroFrameworkAssembly.Title)]
[assembly: AssemblyDescription(MetroFrameworkAssembly.Description)]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(MetroFrameworkAssembly.Company)]
[assembly: AssemblyProduct(MetroFrameworkAssembly.Product)]
[assembly: AssemblyCopyright(MetroFrameworkAssembly.Copyright)]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

[assembly: CLSCompliant(true)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("9559a6f3-8cce-4644-a571-8aeeeb526094")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(MetroFrameworkAssembly.Version)]
[assembly: AssemblyFileVersion(MetroFrameworkAssembly.Version)]

[assembly:AllowPartiallyTrustedCallers]

[assembly: InternalsVisibleTo(MetroFramework.AssemblyRef.MetroFrameworkDesign)]
[assembly: InternalsVisibleTo(MetroFramework.AssemblyRef.MetroFrameworkFonts)]

internal static class MetroFrameworkAssembly
{
    internal const string Title = "MetroFramework.dll";
    internal const string Version = "1.2.0.3";
    internal const string Description = "Metro UI Framework for .NET WinForms";
    internal const string Copyright = "Copyright \x00a9 2013 Jens Thiel.  All rights reserved.";
    internal const string Company = "Jens Thiel";
    internal const string Product = "MetroFramework";
}

namespace MetroFramework
{
    internal static class AssemblyRef
    {

        // Design

        internal const string MetroFrameworkDesign = "MetroFramework.Design";

        // Fonts

        internal const string MetroFrameworkFonts = "MetroFramework.Fonts";

        internal const string MetroFrameworkFontResolver = "MetroFramework.Fonts.FontResolver, " + MetroFrameworkFonts;
    }
}