using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Pendant;
using TestHelper;
using Microsoft.CodeAnalysis;

namespace CommentsAnalyzerTests
{
    [TestClass]
    public class CommentsTests : DiagnosticVerifier
    {
        [TestMethod]
        public void TestMethod1()
        {
            var test = @"class Class1 { }";

            VerifyCSharpDiagnostic(test);
        }

        /// <summary>
        /// Verifies whether the Comments Analyzer can detect proper XML commenting conventions on properties
        /// </summary>
        [TestMethod]
        public void TestForProperlyCommentedProperties()
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
        /// <summary>
        /// Sample Summary
        /// </summary>
        class TypeName
        {   
            public int Agedfvcvxv { get { return Age; } set { value = Age; } }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "Comments",
                Message = String.Format("Violation: Properties must have an xml summary comment."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 16, 13)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Comments Analyzer can detect proper XML commenting conventions on fields
        /// </summary>
        [TestMethod]
        public void TestForProperlyCommentedFields()
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
        /// <summary>
        /// Sample Summary
        /// </summary>
        class TypeName
        {   
            public int test;
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "Comments",
                Message = String.Format("Violation: Fields must have an xml summary comment."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 16, 13)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Comments Analyzer can detect proper XML commenting conventions on interfaces
        /// </summary>
        [TestMethod]
        public void TestForProperlyCommentedInterfaces()
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
        /// <summary>
        /// Sample Summary
        /// </summary>
        class TypeName
        {   
            public interface Itest {}
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "Comments",
                Message = String.Format("Violation: Interfaces must have an xml summary comment."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 16, 13)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Comments Analyzer can detect proper XML commenting conventions on structs
        /// </summary>
        [TestMethod]
        public void TestForProperlyCommentedStructs()
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
        /// <summary>
        /// Sample Summary
        /// </summary>
        class TypeName
        {   
            struct Test 
            {

            }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "Comments",
                Message = String.Format("Violation: Structs must have an xml summary comment."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 16, 13)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }


        /// <summary>
        /// Verifies whether the Comments Analyzer can detect proper XML commenting conventions on Enums
        /// </summary>
        [TestMethod]
        public void TestForProperlyCommentedEnums()
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
        /// <summary>
        /// Sample Summary
        /// </summary>
        class TypeName
        {   
            enum Test 
            {
                Low,
                Medium,
                High
            }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "Comments",
                Message = String.Format("Violation: Enums must have an xml summary comment."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 16, 13)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }


        /// <summary>
        /// Verifies whether the Comments Analyzer can detect proper XML commenting conventions on classes
        /// </summary>
        [TestMethod]
        public void TestForProperlyCommentedClasses()
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

        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "Comments",
                Message = String.Format("Violation: Classes must have an xml summary comment."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 11, 9)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }


        /// <summary>
        /// Verifies whether the Comments Analyzer can detect proper XML commenting conventions on methods
        /// </summary>
        [TestMethod]
        public void TestForProperlyCommentedMethods()
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
        /// <summary>
        /// Sample Summary
        /// </summary>
        class TypeName
        {   
            public void Test()
            {
                break;
            }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "Comments",
                Message = String.Format("Violation: Methods must have an xml summary comment."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 16, 13)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }
    }
}
