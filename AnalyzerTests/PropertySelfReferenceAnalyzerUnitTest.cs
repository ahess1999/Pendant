using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelper;
using Pendant;

namespace AnalyzerTests
{
    [TestClass]
    public class UnitTest : DiagnosticVerifier
    {

        //No diagnostics expected to show up
        [TestMethod]
        public void NoPropertySelfReferenceShouldNotTriggerDiagnostic()
        {
            var test = @"";

            VerifyCSharpDiagnostic(test);
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        public void PropertySelfReferenceShouldTriggerDiagnostic()
        {
            var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class TypeName
        {   
            public bool Flag {
                get { return Flag; }
            }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "PropertySelfReference",
                Message = String.Format("Property '{0}' references itself in its accessor method", "Flag"),
                Severity = DiagnosticSeverity.Error,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 13, 25),
                            new DiagnosticResultLocation("Test0.cs", 14, 30)
                        }
            };

            VerifyCSharpDiagnostic(test, expected);
        }
    }
}
