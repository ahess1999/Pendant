using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Pendant
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CommentsAnalyzer : DiagnosticAnalyzer
    {
        /// <summary>
        /// The Id for the diagnostic
        /// </summary>
        public const string DiagnosticId = "Comments";

        /// <summary>
        /// Title that displays when a diagnostic error is found
        /// </summary>
        internal static readonly LocalizableString Title = "Comment Violation";

        /// <summary>
        /// Message layout, displays "Violation" and which one it is
        /// </summary>
        internal static readonly LocalizableString MessageFormat = "Violation '{0}'";

        /// <summary>
        /// The category of the diagnostic so that you can cipher through which is which
        /// </summary>
        internal const string Category = "CommentsAnalyzer Category";


        /// <summary>
        /// Creates the rule for the diagnostic
        /// </summary>
        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);

        /// <summary>
        /// An immutable array of the diagnostics that returns a new ImmutableArray with the new rule added
        /// </summary>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

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
            context.RegisterSyntaxNodeAction(AnalyzeCommentOfMethods, SyntaxKind.MethodDeclaration);
        }

        /// <summary>
        /// Analyzes properties in the document to make sure they have proper XML Comment Documentation.
        /// </summary>
        /// <param name="context"> The syntax node we are checking </param>
        private static void AnalyzeCommentOfProperties(SyntaxNodeAnalysisContext context)
        {
            //Finds the property delcarations within the cs file
            var propertyDeclaration = (PropertyDeclarationSyntax)context.Node;

            if (context.Node.HasStructuredTrivia == false)
            {
                var diagnostic = Diagnostic.Create(Rule, propertyDeclaration.GetLocation(), "Properties must have an xml summary comment.");
                context.ReportDiagnostic(diagnostic);
            }
        }

        /// <summary>
        /// Analyzes fields in the document to make sure they have proper XML Comment Documentation.
        /// </summary>
        /// <param name="context"> The syntax node we are checking </param>
        private static void AnalyzeCommentOfFields(SyntaxNodeAnalysisContext context)
        {
            //Finds the property delcarations within the cs file
            var fieldDeclaration = (FieldDeclarationSyntax)context.Node;

            if(context.Node.HasStructuredTrivia == false)
            {
                var diagnostic = Diagnostic.Create(Rule, fieldDeclaration.GetLocation(), "Properties must have an xml summary comment.");
                context.ReportDiagnostic(diagnostic);
            }
        }

        /// <summary>
        /// Analyzes interfaces in the document to make sure they have proper XML Comment Documentation.
        /// </summary>
        /// <param name="context"> The syntax node we are checking </param>
        private static void AnalyzeCommentOfInterfaces(SyntaxNodeAnalysisContext context)
        {
            //Finds the property delcarations within the cs file
            var interfaceDeclaration = (InterfaceDeclarationSyntax)context.Node;

            if (context.Node.HasStructuredTrivia == false)
            {
                var diagnostic = Diagnostic.Create(Rule, interfaceDeclaration.GetLocation(), "Properties must have an xml summary comment.");
                context.ReportDiagnostic(diagnostic);
            }
        }

        /// <summary>
        /// Analyzes structs in the document to make sure they have proper XML Comment Documentation.
        /// </summary>
        /// <param name="context"> The syntax node we are checking </param>
        private static void AnalyzeCommentOfStructs(SyntaxNodeAnalysisContext context)
        {
            //Finds the property delcarations within the cs file
            var structDeclaration = (StructDeclarationSyntax)context.Node;

            if (context.Node.HasStructuredTrivia == false)
            {
                var diagnostic = Diagnostic.Create(Rule, structDeclaration.GetLocation(), "Properties must have an xml summary comment.");
                context.ReportDiagnostic(diagnostic);
            }
        }

        /// <summary>
        /// Analyzes enums in the document to make sure they have proper XML Comment Documentation.
        /// </summary>
        /// <param name="context"> The syntax node we are checking </param>
        private static void AnalyzeCommentOfEnums(SyntaxNodeAnalysisContext context)
        {
            //Finds the property delcarations within the cs file
            var enumDeclaration = (EnumDeclarationSyntax)context.Node;

            if (context.Node.HasStructuredTrivia == false)
            {
                var diagnostic = Diagnostic.Create(Rule, enumDeclaration.GetLocation(), "Properties must have an xml summary comment.");
                context.ReportDiagnostic(diagnostic);
            }
        }

        /// <summary>
        /// Analyzes classes in the document to make sure they have proper XML Comment Documentation.
        /// </summary>
        /// <param name="context"> The syntax node we are checking </param>
        private static void AnalyzeCommentOfClasses(SyntaxNodeAnalysisContext context)
        {
            //Finds the property delcarations within the cs file
            var classDeclaration = (ClassDeclarationSyntax)context.Node;

            if (context.Node.HasStructuredTrivia == false)
            {
                var diagnostic = Diagnostic.Create(Rule, classDeclaration.GetLocation(), "Properties must have an xml summary comment.");
                context.ReportDiagnostic(diagnostic);
            }
        }

        /// <summary>
        /// Analyzes methods in the document to make sure they have proper XML Comment Documentation.
        /// </summary>
        /// <param name="context"> The syntax node we are checking </param>
        private static void AnalyzeCommentOfMethods(SyntaxNodeAnalysisContext context)
        {
            //Finds the property delcarations within the cs file
            var methodDeclaration = (MethodDeclarationSyntax)context.Node;

            if (context.Node.HasStructuredTrivia == false)
            {
                var diagnostic = Diagnostic.Create(Rule, methodDeclaration.GetLocation(), "Properties must have an xml summary comment.");
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}