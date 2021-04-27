using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace Pendant
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class PropertySelfReferenceAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "PSR-0001";
        internal static readonly LocalizableString Title = "Property Self Reference Violation";
        internal static readonly LocalizableString MessageFormat = "Violation: {0}";
        internal const string Category = "PropertySelfReferenceAnalyzer Category";

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        /// <summary>
        /// Initializes the PropertySelfReferenceAnalyzer
        /// </summary>
        /// <param name="context">The analysis context</param>
        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.PropertyDeclaration);
        }

        /// <summary>
        /// Analyzes a PropertyDeclarationSyntax Node for self-reference errors
        /// </summary>
        /// <param name="context">The context of the PropertyDeclarationSyntax Node</param>
        private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var propertyDeclaration = (PropertyDeclarationSyntax)context.Node;

            // Check the property's getter and setter for self-references
            var selfReference = from identifierDeclaration in context.Node.DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                                where identifierDeclaration.Identifier.ValueText == propertyDeclaration.Identifier.ValueText
                                select identifierDeclaration;

            if (selfReference.Any())
            {
                // If a self-reference is found, create a diagnostic to report it
                var primaryLocation = propertyDeclaration.Identifier.GetLocation();
                var additionalLocations = from sr in selfReference select sr.Identifier.GetLocation();
                var diagnostic = Diagnostic.Create(
                    Rule,
                    primaryLocation,
                    additionalLocations,
                    "Properties should not be referencing themselves, check your getter."
                    );
                context.ReportDiagnostic(diagnostic);
            }
        }

    }
}
