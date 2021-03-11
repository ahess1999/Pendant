using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Pendant
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer1 : DiagnosticAnalyzer
    {

        public const string DiagnosticId = "IDE0011";
        internal static readonly LocalizableString Title = "Analyzer1 Title";
        internal static readonly LocalizableString MessageFormat = "Analyzer1 '{0}'";
        internal const string Category = "Analyzer1 Category";
        
        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        /// <summary>
        /// Initializes the Code Analysis
        /// </summary>
        /// <param name="context">The analysis context</param>
        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxTreeAction(syntaxTreeContext =>
            {
                // Iterate through all statements in the tree
                var root = syntaxTreeContext.Tree.GetRoot(syntaxTreeContext.CancellationToken);
                foreach (var statement in root.DescendantNodes().OfType<StatementSyntax>())
                {
                    // Skip analyzing block statements 
                    if (statement is BlockSyntax)
                    {
                        continue;
                    }
                 
                    // Report issues for all statements that are nested within a statement
                    // but not a block statement
                    if (statement.Parent is StatementSyntax && !(statement.Parent is BlockSyntax))
                    {
                        var diagnostic = Diagnostic.Create(Rule, statement.GetFirstToken().GetLocation());

                        syntaxTreeContext.ReportDiagnostic(diagnostic);
                    }
                }
            });

        }


    }
}
