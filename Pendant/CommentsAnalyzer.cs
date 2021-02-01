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
            context.RegisterSyntaxNodeAction(CommentAnalyzer, SyntaxKind.ClassDeclaration);

        }

        /// <summary>
        /// Analyzes the Comment section (leading trivia) to see if they comply with CIS 300 and CIS 400 at KSU
        /// </summary>
        /// <param name="context">The context of the Property</param>
        private static void CommentAnalyzer(SyntaxNodeAnalysisContext context)
        {
            var commentDeclaration = (ClassDeclarationSyntax)context.Node;
            var leadingComment = context.Node.GetLeadingTrivia();

            /*
            if (leadingComment.Any())
            {

            }
            */
        }
    }
}