using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Pendant;
using TestHelper;
using Microsoft.CodeAnalysis;

namespace PropertySelfReferenceTests
{
    [TestClass]
    public class PropertySelfReferenceTests : DiagnosticVerifier
    {
        [TestMethod]
        public void TestForPropertySelfReference()
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
            public int Test { get { return Test; }  }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "PropertySelfReferenceAnalyzer",
                Message = String.Format("Violation: Properties should not be referencing themselves, check your getter."),
                Severity = DiagnosticSeverity.Error,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 13, 24),
                            new DiagnosticResultLocation("Test0.cs", 13, 44)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }
    }
}
