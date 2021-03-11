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
    public class INotifyPropertyChangedAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "INotifyPropertyChangedAnalyzer";
        internal static readonly LocalizableString Title = "INotifyPropertyChangedAnalyzer Title";
        internal static readonly LocalizableString MessageFormat = "INotifyPropertyChangedAnalyzer '{0}'";
        internal const string Category = "INotifyPropertyChangedAnalyzer Category";

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeInterfaceIsImplemented, SyntaxKind.ClassDeclaration);
        }

        private static void AnalyzeInterfaceIsImplemented(SyntaxNodeAnalysisContext context)
        {
            var classDeclaration = (ClassDeclarationSyntax)context.Node;

            foreach(var interfaceName in classDeclaration.DescendantNodes())
            {
                if(interfaceName is BaseListSyntax)
                {
                    var baseListName = (BaseListSyntax)interfaceName;
                    
                    foreach(var simpleBase in baseListName.DescendantNodes())
                    {
                        if(simpleBase is SimpleBaseTypeSyntax)
                        {
                            var simpleBaseName = (SimpleBaseTypeSyntax)simpleBase;

                            foreach(var id in simpleBaseName.DescendantNodes())
                            {
                                if(id is IdentifierNameSyntax)
                                {
                                    var identName = (IdentifierNameSyntax)id;
                                    
                                    if(identName.Identifier.ValueText == "INotifyPropertyChanged")
                                    {
                                        //Creates a diagnostic at the location of the struct name
                                        var diagnostic = Diagnostic.Create(Rule, identName.Identifier.GetLocation(), "Test Violation");
                                        //Reports the problem in the code
                                        context.ReportDiagnostic(diagnostic);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
