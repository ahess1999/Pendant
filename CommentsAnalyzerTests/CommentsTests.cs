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

        /// <summary>
        /// Verifies whether the Comments Analyzer can detect proper XML commenting conventions on properties
        /// </summary>
        [TestMethod]
        public void TestForCommentsInProperties()
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
            /// <summary>
            /// 
            /// </summary>
            public int Agedfvcvxv { get { return Age; } set { value = Age; } }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "Comments",
                Message = String.Format("Violation: Must include a summary in xml summary comment with no extra new lines."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 19, 13)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Comments Analyzer can detect proper XML commenting conventions on fields
        /// </summary>
        [TestMethod]
        public void TestForCommentsInFields()
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
            /// <summary>
            /// 
            /// </summary>
            public int test;
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "Comments",
                Message = String.Format("Violation: Must include a summary in xml summary comment with no extra new lines."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 19, 13)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Comments Analyzer can detect proper XML commenting conventions on interfaces
        /// </summary>
        [TestMethod]
        public void TestForCommentsInInterfaces()
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
            /// <summary>
            /// 
            /// </summary>
            public interface Itest {}
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "Comments",
                Message = String.Format("Violation: Must include a summary in xml summary comment with no extra new lines."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 19, 13)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Comments Analyzer can detect proper XML commenting conventions on structs
        /// </summary>
        [TestMethod]
        public void TestForCommentsInStructs()
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
            /// <summary>
            /// 
            /// </summary>
            struct Test 
            {

            }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "Comments",
                Message = String.Format("Violation: Must include a summary in xml summary comment with no extra new lines."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 19, 13)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }


        /// <summary>
        /// Verifies whether the Comments Analyzer can detect proper XML commenting conventions on Enums
        /// </summary>
        [TestMethod]
        public void TestForCommentsInEnums()
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
            /// <summary>
            /// 
            /// </summary>
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
                Message = String.Format("Violation: Must include a summary in xml summary comment with no extra new lines."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 19, 13)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }


        /// <summary>
        /// Verifies whether the Comments Analyzer can detect proper XML commenting conventions on classes
        /// </summary>
        [TestMethod]
        public void TestForCommentsInClasses()
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
        /// 
        /// </summary>
        class TypeName
        {   

        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "Comments",
                Message = String.Format("Violation: Must include a summary in xml summary comment with no extra new lines."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 14, 9)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }


        /// <summary>
        /// Verifies whether the Comments Analyzer can detect proper XML commenting conventions on methods
        /// </summary>
        [TestMethod]
        public void TestForCommentsInMethods()
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
            /// <summary>
            /// 
            /// </summary>
            public void Test()
            {
                break;
            }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "Comments",
                Message = String.Format("Violation: Must include a summary in xml summary comment with no extra new lines."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 19, 13)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        /// Verifies whether the Comments Analyzer can detect proper XML commenting conventions on methods
        /// </summary>
        [TestMethod]
        public void TestForCommentsInMethodsForParameters()
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
            /// <summary>
            /// fdsf
            /// </summary>
            /// <param name=""test""></param>
            public void Test(int test)
            {
                break;
            }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "Comments",
                Message = String.Format("Violation: Parameters must have a definition in an xml summary comment."),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 20, 13)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }
    }
}
