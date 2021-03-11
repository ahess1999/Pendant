using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Pendant;
using TestHelper;

namespace AnalyzerTests
{
    [TestClass]
    public class NamingConventionsTests : DiagnosticVerifier
    {
        [TestMethod]
        public void TestMethod1()
        {
            var test = @"class Class1 { }";

            VerifyCSharpDiagnostic(test);
        }

        /// <summary>
        /// Verifies whether the Naming Conventions Analyzer can detect proper XML naming conventions on properties
        /// </summary>
        [TestMethod]
        public void TestForProperlyNamedProperties()
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
            public int Agedfvcvxv { get { return Age; } set { value = Age; } }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "NamingConventions",
                Message = String.Format("Violation: Agedfvcvxv"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 13, 24),
                            new DiagnosticResultLocation("Test0.cs", 13, 50),
                            new DiagnosticResultLocation("Test0.cs", 13, 71)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Naming Conventions Analyzer can detect proper XML naming conventions on fields
        /// </summary>
        [TestMethod]
        public void TestForProperlyNamedFieldDeclarations()
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
            int test = 0;
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "NamingConventions",
                Message = String.Format("Violation: Private Fields should start with an '_'"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 13, 17)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Naming Conventions Analyzer can detect proper XML naming conventions on interfaces
        /// </summary>
        [TestMethod]
        public void TestForProperlyNamedInterfaces()
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
            public interface test {}
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "NamingConventions",
                Message = String.Format("Violation: Interfaces should start with an 'I'"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 13, 30)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Naming Conventions Analyzer can detect proper XML naming conventions on interfaces, specifically the second letter being capitalized
        /// </summary>
        [TestMethod]
        public void TestForProperlyNamedInterfacesSecondLetter()
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
            public interface Itest {}
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "NamingConventions",
                Message = String.Format("Violation: Interfaces should start with an 'I' and the second letter should be capital"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 13, 30)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Naming Conventions Analyzer can detect proper XML naming conventions on structs
        /// </summary>
        [TestMethod]
        public void TestForProperlyNamedStructs()
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
            public struct test {}
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "NamingConventions",
                Message = String.Format("Violation: Struct names should begin with a capital letter"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 13, 27)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Naming Conventions Analyzer can detect proper XML naming conventions on enums
        /// </summary>
        [TestMethod]
        public void TestForProperlyNamedEnums()
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
            public enum test {}
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "NamingConventions",
                Message = String.Format("Violation: Enum names should begin with a capital letter"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 13, 25)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Naming Conventions Analyzer can detect proper XML naming conventions on classes
        /// </summary>
        [TestMethod]
        public void TestForProperlyNamedClasses()
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
            public class test {}
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "NamingConventions",
                Message = String.Format("Violation: Class names should begin with a capital letter"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 13, 26)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Naming Conventions Analyzer can detect proper XML naming conventions on methods
        /// </summary>
        [TestMethod]
        public void TestForProperlyNamedMethods()
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
            public void test() {}
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "NamingConventions",
                Message = String.Format("Violation: Method names should begin with a capital letter"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 13, 25)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Naming Conventions Analyzer can detect proper XML naming conventions on method parameters
        /// </summary>
        [TestMethod]
        public void TestForProperlyNamedParameters()
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
            public void Test(int TestTest) {}
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "NamingConventions",
                Message = String.Format("Violation: Parameter names should be in camel case"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 13, 34)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Naming Conventions Analyzer can detect proper XML naming conventions on local variables
        /// </summary>
        [TestMethod]
        public void TestForProperlyNamedLocalVariables()
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
            public void Test(int testTest) { int TestTomorrow; }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "NamingConventions",
                Message = String.Format("Violation: Local variable names should be in camel case"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 13, 50)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }
    }
}
