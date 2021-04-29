using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Immutable;
using System.Resources;
using System.Globalization;

namespace Pendant
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CommentsAnalyzer : DiagnosticAnalyzer
    {
        
        /// <summary>
        /// Initializs the rules for the diagnostic
        /// </summary>
        internal static DiagnosticDescriptor Rule01;
        internal static DiagnosticDescriptor Rule02;
        internal static DiagnosticDescriptor Rule03;
        internal static DiagnosticDescriptor Rule04;
        internal static DiagnosticDescriptor Rule05;
        internal static DiagnosticDescriptor Rule06;
        internal static DiagnosticDescriptor Rule07;
        internal static DiagnosticDescriptor Rule08;
        internal static DiagnosticDescriptor Rule09;

        /// <summary>
        /// An immutable array of the diagnostics that returns a new ImmutableArray with the new rules added
        /// </summary>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule01, Rule02, Rule03, Rule04, Rule05, Rule06, Rule07, Rule08, Rule09); } }

        /// <summary>
        /// Initializer for the analyzer
        /// </summary>
        /// <param name="context">The analysis that runs methods constantly when the page is open.</param>
        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeCommentOfFields, SyntaxKind.FieldDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeCommentOfProperties, SyntaxKind.PropertyDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeCommentOfInterfaces, SyntaxKind.InterfaceDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeCommentOfStructs, SyntaxKind.StructDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeCommentOfEnums, SyntaxKind.EnumDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeCommentOfClasses, SyntaxKind.ClassDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeCommentOfMethodsForxmlSummary, SyntaxKind.MethodDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeCommentOfMethodsForxmlParameters, SyntaxKind.MethodDeclaration);
        }

        /// <summary>
        /// Analyzes properties in the document to make sure they have proper XML Comment Documentation.
        /// </summary>
        /// <param name="context"> The syntax node we are checking </param>
        private static void AnalyzeCommentOfProperties(SyntaxNodeAnalysisContext context)
        {
            //Finds the property delcarations within the cs file
            var propertyDeclaration = (PropertyDeclarationSyntax)context.Node;
            // Holds the comment trivia for each method
            var comments = context.Node.DescendantTrivia().ToImmutableList();
            // The comments in the first position of the list stringified
            var comment1 = comments[1].ToString();
            // split by '/' version of comment1
            var com1 = comment1.Split('/');
            // The comments in to second position of the list stringified
            var comment2 = comments[2].ToString();
            // split by '/' version of comment2
            var com2 = comment2.Split('/');

            // Looks to see if there is an xml summary comment for the method(the first method will be in position 1 but the rest are in position 2)
            if (comment1.Contains("<summary>"))
            {
                // Looks to see if there is a blank section after summary tags to see if there is a typed summary
                if (com1[3].Trim().Length == 0)
                {
                    //const string DiagnosticId = "COM0001";
                    //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                    var diagnostic = Diagnostic.Create(Rule01, propertyDeclaration.GetLocation(), "Must include a summary in xml summary comment with no extra new lines.");
                    context.ReportDiagnostic(diagnostic);
                }

            }
            else if (comment2.Contains("<summary>"))
            {
                if (com2[3].Trim().Length == 0)
                {
                    //const string DiagnosticId = "COM0001";
                    //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                    var diagnostic = Diagnostic.Create(Rule01, propertyDeclaration.GetLocation(), "Must include a summary in xml summary comment with no extra new lines.");
                    context.ReportDiagnostic(diagnostic);
                }
            }
            else
            {
                //const string DiagnosticId = "COM0002";
                //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                var diagnostic = Diagnostic.Create(Rule02, propertyDeclaration.GetLocation(), "Properties must have an xml summary comment.");
                context.ReportDiagnostic(diagnostic);
            }
        }

        /// <summary>
        /// Analyzes fields in the document to make sure they have proper XML Comment Documentation.
        /// </summary>
        /// <param name="context"> The syntax node we are checking </param>
        private static void AnalyzeCommentOfFields(SyntaxNodeAnalysisContext context)
        {
            //Finds the field delcarations within the cs file
            var fieldDeclaration = (FieldDeclarationSyntax)context.Node;
            // Holds the comment trivia for each method
            var comments = context.Node.DescendantTrivia().ToImmutableList();
            // The comments in the first position of the list stringified
            var comment1 = comments[1].ToString();
            // split by '/' version of comment1
            var com1 = comment1.Split('/');
            // The comments in to second position of the list stringified
            var comment2 = comments[2].ToString();
            // split by '/' version of comment2
            var com2 = comment2.Split('/');

            // Looks to see if there is an xml summary comment for the method(the first method will be in position 1 but the rest are in position 2)
            if (comment1.Contains("<summary>"))
            {
                // Looks to see if there is a blank section after summary tags to see if there is a typed summary
                if (com1[3].Trim().Length == 0)
                {
                    //const string DiagnosticId = "COM0001";
                    //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                    var diagnostic = Diagnostic.Create(Rule01, fieldDeclaration.GetLocation(), "Must include a summary in xml summary comment with no extra new lines.");
                    context.ReportDiagnostic(diagnostic);
                }

            }
            else if (comment2.Contains("<summary>"))
            {
                if (com2[3].Trim().Length == 0)
                {
                    //const string DiagnosticId = "COM0001";
                    //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                    var diagnostic = Diagnostic.Create(Rule01, fieldDeclaration.GetLocation(), "Must include a summary in xml summary comment with no extra new lines.");
                    context.ReportDiagnostic(diagnostic);
                }
            }
            else
            {
                //const string DiagnosticId = "COM0003";
                //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                var diagnostic = Diagnostic.Create(Rule03, fieldDeclaration.GetLocation(), "Fields must have an xml summary comment.");
                context.ReportDiagnostic(diagnostic);
            }
        }

        /// <summary>
        /// Analyzes interfaces in the document to make sure they have proper XML Comment Documentation.
        /// </summary>
        /// <param name="context"> The syntax node we are checking </param>
        private static void AnalyzeCommentOfInterfaces(SyntaxNodeAnalysisContext context)
        {
            //Finds the interface delcarations within the cs file
            var interfaceDeclaration = (InterfaceDeclarationSyntax)context.Node;
            // Holds the comment trivia for each method
            var comments = context.Node.DescendantTrivia().ToImmutableList();
            // The comments in the first position of the list stringified
            var comment1 = comments[1].ToString();
            // split by '/' version of comment1
            var com1 = comment1.Split('/');
            // The comments in to second position of the list stringified
            var comment2 = comments[2].ToString();
            // split by '/' version of comment2
            var com2 = comment2.Split('/');

            // Looks to see if there is an xml summary comment for the method(the first method will be in position 1 but the rest are in position 2)
            if (comment1.Contains("<summary>"))
            {
                // Looks to see if there is a blank section after summary tags to see if there is a typed summary
                if (com1[3].Trim().Length == 0)
                {
                    //const string DiagnosticId = "COM0001";
                    //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                    var diagnostic = Diagnostic.Create(Rule01, interfaceDeclaration.GetLocation(), "Must include a summary in xml summary comment with no extra new lines.");
                    context.ReportDiagnostic(diagnostic);
                }

            }
            else if (comment2.Contains("<summary>"))
            {
                if (com2[3].Trim().Length == 0)
                {
                    //const string DiagnosticId = "COM0001";
                    //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                    var diagnostic = Diagnostic.Create(Rule01, interfaceDeclaration.GetLocation(), "Must include a summary in xml summary comment with no extra new lines.");
                    context.ReportDiagnostic(diagnostic);
                }
            }
            else
            {
                //const string DiagnosticId = "COM0004";
                //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                var diagnostic = Diagnostic.Create(Rule04, interfaceDeclaration.GetLocation(), "Interfaces must have an xml summary comment.");
                context.ReportDiagnostic(diagnostic);
            }
        }

        /// <summary>
        /// Analyzes structs in the document to make sure they have proper XML Comment Documentation.
        /// </summary>
        /// <param name="context"> The syntax node we are checking </param>
        private static void AnalyzeCommentOfStructs(SyntaxNodeAnalysisContext context)
        {
            //Finds the struct delcarations within the cs file
            var structDeclaration = (StructDeclarationSyntax)context.Node;
            // Holds the comment trivia for each method
            var comments = context.Node.DescendantTrivia().ToImmutableList();
            // The comments in the first position of the list stringified
            var comment1 = comments[1].ToString();
            // split by '/' version of comment1
            var com1 = comment1.Split('/');
            // The comments in to second position of the list stringified
            var comment2 = comments[2].ToString();
            // split by '/' version of comment2
            var com2 = comment2.Split('/');

            // Looks to see if there is an xml summary comment for the method(the first method will be in position 1 but the rest are in position 2)
            if (comment1.Contains("<summary>"))
            {
                // Looks to see if there is a blank section after summary tags to see if there is a typed summary
                if (com1[3].Trim().Length == 0)
                {
                    //const string DiagnosticId = "COM0001";
                    //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                    var diagnostic = Diagnostic.Create(Rule01, structDeclaration.GetLocation(), "Must include a summary in xml summary comment with no extra new lines.");
                    context.ReportDiagnostic(diagnostic);
                }

            }
            else if (comment2.Contains("<summary>"))
            {
                if (com2[3].Trim().Length == 0)
                {
                    //const string DiagnosticId = "COM0001";
                    //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                    var diagnostic = Diagnostic.Create(Rule01, structDeclaration.GetLocation(), "Must include a summary in xml summary comment with no extra new lines.");
                    context.ReportDiagnostic(diagnostic);
                }
            }
            else
            {
                //const string DiagnosticId = "COM0005";
                //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                var diagnostic = Diagnostic.Create(Rule05, structDeclaration.GetLocation(), "Structs must have an xml summary comment.");
                context.ReportDiagnostic(diagnostic);
            }
        }

        /// <summary>
        /// Analyzes enums in the document to make sure they have proper XML Comment Documentation.
        /// </summary>
        /// <param name="context"> The syntax node we are checking </param>
        private static void AnalyzeCommentOfEnums(SyntaxNodeAnalysisContext context)
        {
            //Finds the enum delcarations within the cs file
            var enumDeclaration = (EnumDeclarationSyntax)context.Node;
            // Holds the comment trivia for each method
            var comments = context.Node.DescendantTrivia().ToImmutableList();
            // The comments in the first position of the list stringified
            var comment1 = comments[1].ToString();
            // split by '/' version of comment1
            var com1 = comment1.Split('/');
            // The comments in to second position of the list stringified
            var comment2 = comments[2].ToString();
            // split by '/' version of comment2
            var com2 = comment2.Split('/');

            // Looks to see if there is an xml summary comment for the method(the first method will be in position 1 but the rest are in position 2)
            if (comment1.Contains("<summary>"))
            {
                // Looks to see if there is a blank section after summary tags to see if there is a typed summary
                if (com1[3].Trim().Length == 0)
                {
                    //const string DiagnosticId = "COM0001";
                    //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                    var diagnostic = Diagnostic.Create(Rule01, enumDeclaration.GetLocation(), "Must include a summary in xml summary comment with no extra new lines.");
                    context.ReportDiagnostic(diagnostic);
                }

            }
            else if (comment2.Contains("<summary>"))
            {
                if (com2[3].Trim().Length == 0)
                {
                    //const string DiagnosticId = "COM0001";
                    //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                    var diagnostic = Diagnostic.Create(Rule01, enumDeclaration.GetLocation(), "Must include a summary in xml summary comment with no extra new lines.");
                    context.ReportDiagnostic(diagnostic);
                }
            }
            else
            {
                //const string DiagnosticId = "COM0006";
                //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                var diagnostic = Diagnostic.Create(Rule06, enumDeclaration.GetLocation(), "Enums must have an xml summary comment.");
                context.ReportDiagnostic(diagnostic);
            }
        }

        /// <summary>
        /// Analyzes classes in the document to make sure they have proper XML Comment Documentation.
        /// </summary>
        /// <param name="context"> The syntax node we are checking </param>
        private static void AnalyzeCommentOfClasses(SyntaxNodeAnalysisContext context)
        {
            // Finds the method delcarations within the cs file
            var classDeclaration = (ClassDeclarationSyntax)context.Node;
            // Holds the comment trivia for each method
            var comments = context.Node.DescendantTrivia().ToImmutableArray();
            // The comments in the first position of the list stringified
            var comment1 = comments[1].ToString();
            // split by '/' version of comment1
            var com1 = comment1.Split('/');
            // The comments in to second position of the list stringified
            var comment2 = comments[2].ToString();
            // split by '/' version of comment2
            var com2 = comment2.Split('/');

            // Looks to see if there is an xml summary comment for the method(the first method will be in position 1 but the rest are in position 2)
            if (comment1.Contains("<summary>"))
            {
                // Looks to see if there is a blank section after summary tags to see if there is a typed summary
                if (com1[3].Trim().Length == 0)
                {
                    //const string DiagnosticId = "COM0001";
                    //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                    var diagnostic = Diagnostic.Create(Rule01, classDeclaration.GetLocation(), "Must include a summary in xml summary comment with no extra new lines.");
                    context.ReportDiagnostic(diagnostic);
                }

            }
            else if (comment2.Contains("<summary>"))
            {
                if (com2[3].Trim().Length == 0)
                {
                    //const string DiagnosticId = "COM0001";
                    //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                    var diagnostic = Diagnostic.Create(Rule01, classDeclaration.GetLocation(), "Must include a summary in xml summary comment with no extra new lines.");
                    context.ReportDiagnostic(diagnostic);
                }
            }
            else
            {
                //const string DiagnosticId = "COM0007";
                //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                var diagnostic = Diagnostic.Create(Rule07, classDeclaration.GetLocation(), "Classes must have an xml summary comment.");
                context.ReportDiagnostic(diagnostic);
            }
        }

        /// <summary>
        /// Analyzes methods in the document to make sure they have proper XML Comment Documentation.
        /// </summary>
        /// <param name="context"> The syntax node we are checking </param>
        private static void AnalyzeCommentOfMethodsForxmlSummary(SyntaxNodeAnalysisContext context)
        {
            // Finds the method delcarations within the cs file
            var methodDeclaration = (MethodDeclarationSyntax)context.Node;
            // Holds the comment trivia for each method
            var comments = context.Node.DescendantTrivia().ToImmutableArray();
            // The comments in the first position of the list stringified
            var comment1 = comments[1].ToString();
            // split by '/' version of comment1
            var com1 = comment1.Split('/');
            // The comments in to second position of the list stringified
            var comment2 = comments[2].ToString();
            // split by '/' version of comment2
            var com2 = comment2.Split('/');

            // Looks to see if there is an xml summary comment for the method(the first method will be in position 1 but the rest are in position 2)
            if (comment1.Contains("<summary>"))
            {
                // Looks to see if there is a blank section after summary tags to see if there is a typed summary
                if(com1[3].Trim().Length == 0)
                {
                    //const string DiagnosticId = "COM0001";
                    //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                    var diagnostic = Diagnostic.Create(Rule01, methodDeclaration.GetLocation(), "Must include a summary in xml summary comment with no extra new lines.");
                    context.ReportDiagnostic(diagnostic);
                }

            }
            else if (comment2.Contains("<summary>"))
            {
                if(com2[3].Trim().Length == 0)
                {
                    //const string DiagnosticId = "COM0001";
                    //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                    var diagnostic = Diagnostic.Create(Rule01, methodDeclaration.GetLocation(), "Must include a summary in xml summary comment with no extra new lines.");
                    context.ReportDiagnostic(diagnostic);
                }
            }
            else
            {
                //const string DiagnosticId = "COM0008";
                //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                var diagnostic = Diagnostic.Create(Rule08, methodDeclaration.GetLocation(), "Methods must have an xml summary comment.");
                context.ReportDiagnostic(diagnostic);
            }
        }

        /// <summary>
        /// Analyzes methods in the document to make sure they have proper XML Comment Documentation.
        /// </summary>
        /// <param name="context"> The syntax node we are checking </param>
        private static void AnalyzeCommentOfMethodsForxmlParameters(SyntaxNodeAnalysisContext context)
        {
            // Finds the method delcarations within the cs file
            var methodDeclaration = (MethodDeclarationSyntax)context.Node;
            // Holds the comment trivia for each method
            var comments = context.Node.DescendantTrivia().ToImmutableArray();
            // The comments in the first position of the list stringified
            var comment1 = comments[1].ToString();

            // The comments in to second position of the list stringified
            var comment2 = comments[2].ToString();

            // Looks to see if there is an xml summary comment for the method(the first method will be in position 1 but the rest are in position 2)
            if (comment1.Contains("<summary>") || comment2.Contains("<summary>"))
            {
                // Looks to see if any parameter is left blank in its description
                if (comment1.Contains("\"><") || comment2.Contains("\"><"))
                {
                    //const string DiagnosticId = "COM0009";
                    //DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                    var diagnostic = Diagnostic.Create(Rule09, methodDeclaration.GetLocation(), "Parameters must have a definition in an xml summary comment.");
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
        public CommentsAnalyzer()
        {
            ResourceManager rm = new ResourceManager("Pendant.PendantResources", typeof(CommentsAnalyzer).Assembly);
            Rule01 = new DiagnosticDescriptor("COM0001", rm.GetString("COM-Title"), rm.GetString("COM-MessageFormat"), rm.GetString("COM-Category"), DiagnosticSeverity.Warning, true);
            Rule02 = new DiagnosticDescriptor("COM0002", rm.GetString("COM-Title"), rm.GetString("COM-MessageFormat"), rm.GetString("COM-Category"), DiagnosticSeverity.Warning, true);
            Rule03 = new DiagnosticDescriptor("COM0003", rm.GetString("COM-Title"), rm.GetString("COM-MessageFormat"), rm.GetString("COM-Category"), DiagnosticSeverity.Warning, true);
            Rule04 = new DiagnosticDescriptor("COM0004", rm.GetString("COM-Title"), rm.GetString("COM-MessageFormat"), rm.GetString("COM-Category"), DiagnosticSeverity.Warning, true);
            Rule05 = new DiagnosticDescriptor("COM0005", rm.GetString("COM-Title"), rm.GetString("COM-MessageFormat"), rm.GetString("COM-Category"), DiagnosticSeverity.Warning, true);
            Rule06 = new DiagnosticDescriptor("COM0006", rm.GetString("COM-Title"), rm.GetString("COM-MessageFormat"), rm.GetString("COM-Category"), DiagnosticSeverity.Warning, true);
            Rule07 = new DiagnosticDescriptor("COM0007", rm.GetString("COM-Title"), rm.GetString("COM-MessageFormat"), rm.GetString("COM-Category"), DiagnosticSeverity.Warning, true);
            Rule08 = new DiagnosticDescriptor("COM0008", rm.GetString("COM-Title"), rm.GetString("COM-MessageFormat"), rm.GetString("COM-Category"), DiagnosticSeverity.Warning, true);
            Rule09 = new DiagnosticDescriptor("COM0009", rm.GetString("COM-Title"), rm.GetString("COM-MessageFormat"), rm.GetString("COM-Category"), DiagnosticSeverity.Warning, true);
        }
    }
}