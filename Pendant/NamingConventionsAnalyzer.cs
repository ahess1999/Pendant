/* NamingConventionsAnalyzer.cs
 * Author: Austin Hess
 */
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
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
    public class NamingConventionsAnalyzer : DiagnosticAnalyzer
    {
        int t;
        private int e;
        protected int f;
        /// <summary>
        /// Public list that stores all the errors found within the open document
        /// </summary>
        public static List<Error> ErrorList = new List<Error>();

        /// <summary>
        /// The Id for the diagnostic
        /// </summary>
        public const string SupportDiagnostic = "NamingConventions";

        /// <summary>
        /// Title that displays when a diagnostic error is found
        /// </summary>
        internal static readonly LocalizableString Title = "Naming Convention Violation";

        /// <summary>
        /// Message layout, displays "Violation" and which one it is
        /// </summary>
        internal static readonly LocalizableString MessageFormat = "Violation: {0}";

        /// <summary>
        /// The category of the diagnostic so that you can cipher through which is which
        /// </summary>
        internal const string Category = "NamingConventionAnalyzer Category";

        /// <summary>
        /// Creates the rule for the diagnostic
        /// </summary>
         internal static DiagnosticDescriptor SupportRule = new DiagnosticDescriptor(SupportDiagnostic, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);

        /// <summary>
        /// An immutable array of the diagnostics that returns a new ImmutableArray with the new rule added
        /// </summary>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(SupportRule); } }

        /// <summary>
        /// Initializer for the analyzer
        /// </summary>
        /// <param name="context">The analysis that runs methods constantly when the page is open.</param>
        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeNamingConventionOfProperties, SyntaxKind.PropertyDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeNamingConventionOfFieldDeclarations, SyntaxKind.FieldDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeNamingConventionsOfParameters, SyntaxKind.ClassDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeNamingConventionsOfInterfaces, SyntaxKind.NamespaceDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeNamingConventionsOfStructs, SyntaxKind.NamespaceDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeNamingConventionsOfEnums, SyntaxKind.NamespaceDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeNamingConventionsOfClasses, SyntaxKind.NamespaceDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeNamingConventionsOfMethods, SyntaxKind.ClassDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeNamingConventionsOfLocalVariables, SyntaxKind.MethodDeclaration);
        }

        /// <summary>
        /// Analyzes the Property naming conventions to see if they comply with CIS 300 and CIS 400 at KSU
        /// </summary>
        /// <param name="context">The context of the Property</param>
        private static void AnalyzeNamingConventionOfProperties(SyntaxNodeAnalysisContext context)
        {
            //Finds the property delcarations within the cs file
            var propertyDeclaration = (PropertyDeclarationSyntax)context.Node;
            //A regular expression that makes sure something is only letters and no numbers
            var letterCheck = new Regex("^[a-zA-Z]*$");
            //Loop that checks each of the property delcartion's identifier and creates a diagnostic if it finds a problem
            var nameOfParameter = from identifierDelcaration in context.Node.DescendantNodes().OfType<IdentifierNameSyntax>()
                                  where letterCheck.IsMatch(identifierDelcaration.Identifier.ValueText) == true && identifierDelcaration.Identifier.ValueText.Length < 5
                                  select identifierDelcaration;

            //If any diagnostic problems are found
            if (nameOfParameter.Any())
            {
                Error e = new Error("Property Error", "Naming Convention");
                ErrorList.Add(e);
                System.Diagnostics.Debug.WriteLine("=====================");
                System.Diagnostics.Debug.WriteLine(e.ErrorMessage);
                System.Diagnostics.Debug.WriteLine("=====================");
                //Gets the location of the diagnostic
                var location = propertyDeclaration.Identifier.GetLocation();
                //Finds other locations
                var otherLocations = from nop in nameOfParameter select nop.Identifier.GetLocation();
                //Creates the diagnostic
                const string DiagnosticId = "NAM0001";
                DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                var diagnostic = Diagnostic.Create(
                    Rule,
                    location,
                    otherLocations,
                    propertyDeclaration.Identifier.ValueText
                    );
                context.ReportDiagnostic(diagnostic);
            }
        }

        /// <summary>
        /// Analyzes the Field naming conventions to see if they comply with CIS 300 and CIS 400 at KSU
        /// </summary>
        /// <param name="context">The context of the Property</param>
        private static void AnalyzeNamingConventionOfFieldDeclarations(SyntaxNodeAnalysisContext context)
        {
            var fieldDeclaration = (FieldDeclarationSyntax)context.Node;
            //Loops through all of the descendant nodes at the top level
            foreach(var varDeclaration in context.Node.DescendantNodes())
            {
                //Checks to see if any of the descendant nodes are of type VariableDeclarationSyntax
                if(varDeclaration is VariableDeclarationSyntax)
                {
                    //If it is of type VariableDeclarationSyntax, then loop through the descendant nodes of that node
                    foreach (var varDeclarator in varDeclaration.DescendantNodes())
                    {
                        //Checks to see if any of the descendant nodes are of type VariableDeclaratorSyntax
                        if(varDeclarator is VariableDeclaratorSyntax)
                        {
                            VariableDeclaratorSyntax identName = (VariableDeclaratorSyntax)varDeclarator;
                            if (identName.Identifier.ValueText != "")
                            {
                                //Checks to see if the first character of the field's name is an '_'
                                if (identName.Identifier.ValueText[0] != '_')
                                {
                                    const string DiagnosticId = "NAM0002";
                                    DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                                    //Creates a diagnostic at the location of the field name
                                    var diagnostic = Diagnostic.Create(Rule, identName.Identifier.GetLocation(), "Private Fields should start with an '_'");
                                    //Reports the problem in the code
                                    context.ReportDiagnostic(diagnostic);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Analyzes the Interface naming conventions to see if they comply with CIS 300 and CIS 400 at KSU
        /// </summary>
        /// <param name="context">The context of the Property</param>
        private static void AnalyzeNamingConventionsOfInterfaces(SyntaxNodeAnalysisContext context)
        {
            var namespaceDeclaration = (NamespaceDeclarationSyntax)context.Node;
            //Loops through all of the descendant nodes at the top level
            foreach (var interfaceName in context.Node.DescendantNodes())
            {
                //Checks to see if any of the descendant nodes are of type InterfaceDeclarationSyntax
                if (interfaceName is InterfaceDeclarationSyntax)
                {
                        InterfaceDeclarationSyntax identName = (InterfaceDeclarationSyntax)interfaceName;

                    if (identName.Identifier.ValueText != "")
                    {
                        //Check to make sure the first letter is 'I'
                        if (identName.Identifier.ValueText[0] != 'I')
                        {
                            const string DiagnosticId = "Nam0003";
                            DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                            //Creates a diagnostic at the location of the interface name
                            var diagnostic = Diagnostic.Create(Rule, identName.Identifier.GetLocation(), "Interfaces should start with an 'I'");
                            //Reports the problem in the code
                            context.ReportDiagnostic(diagnostic);
                        }
                        //Check to make sure that the name is for example "IFruit" and not "Ifruit"
                        if (identName.Identifier.ValueText[0] == 'I' && Char.IsLower(identName.Identifier.ValueText[1]))
                        {
                            const string DiagnosticId = "Nam0004";
                            DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                            //Creates a diagnostic at the location of the interface name
                            var diagnostic = Diagnostic.Create(Rule, identName.Identifier.GetLocation(), "Interfaces should start with an 'I' and the second letter should be capital");
                            //Reports the problem in the code
                            context.ReportDiagnostic(diagnostic);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Analyzes the Struct naming conventions to see if they comply with CIS 300 and CIS 400 at KSU
        /// </summary>
        /// <param name="context">The context of the Property</param>
        private static void AnalyzeNamingConventionsOfStructs(SyntaxNodeAnalysisContext context)
        {
            var namespaceDeclaration = (NamespaceDeclarationSyntax)context.Node;
            //Loops through all of the descendant nodes at the top level
            foreach (var structName in context.Node.DescendantNodes())
            {
                //Checks to see if any of the descendant nodes are of type StructDeclarationSyntax
                if (structName is StructDeclarationSyntax)
                {
                    StructDeclarationSyntax identName = (StructDeclarationSyntax)structName;

                    if (identName.Identifier.ValueText != "")
                    {
                        //Check to make sure the first letter is capitalized
                        if (Char.IsLower(identName.Identifier.ValueText[0]))
                        {
                            const string DiagnosticId = "Nam0005";
                            DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                            //Creates a diagnostic at the location of the struct name
                            var diagnostic = Diagnostic.Create(Rule, identName.Identifier.GetLocation(), "Struct names should begin with a capital letter");
                            //Reports the problem in the code
                            context.ReportDiagnostic(diagnostic);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Analyzes the Enum naming conventions to see if they comply with CIS 300 and CIS 400 at KSU
        /// </summary>
        /// <param name="context">The context of the Property</param>
        private static void AnalyzeNamingConventionsOfEnums(SyntaxNodeAnalysisContext context)
        {
            var namespaceDeclaration = (NamespaceDeclarationSyntax)context.Node;
            //Loops through all of the descendant nodes at the top level
            foreach (var enumName in context.Node.DescendantNodes())
            {
                //Checks to see if any of the descendant nodes are of type EnumDeclarationSyntax
                if (enumName is EnumDeclarationSyntax)
                {
                    EnumDeclarationSyntax identName = (EnumDeclarationSyntax)enumName;

                    if (identName.Identifier.ValueText != "")
                    {
                        //Check to make sure the first letter is capitalized
                        if (Char.IsLower(identName.Identifier.ValueText[0]))
                        {
                            const string DiagnosticId = "Nam0006";
                            DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                            //Creates a diagnostic at the location of the enum name
                            var diagnostic = Diagnostic.Create(Rule, identName.Identifier.GetLocation(), "Enum names should begin with a capital letter");
                            //Reports the problem in the code
                            context.ReportDiagnostic(diagnostic);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Analyzes the Class naming conventions to see if they comply with CIS 300 and CIS 400 at KSU
        /// </summary>
        /// <param name="context">The context of the Property</param>
        private static void AnalyzeNamingConventionsOfClasses(SyntaxNodeAnalysisContext context)
        {
            var namespaceDeclaration = (NamespaceDeclarationSyntax)context.Node;
            //Loops through all of the descendant nodes at the top level
            foreach (var className in context.Node.DescendantNodes())
            {
                //Checks to see if any of the descendant nodes are of type ClassDeclarationSyntax
                if (className is ClassDeclarationSyntax)
                {
                    ClassDeclarationSyntax identName = (ClassDeclarationSyntax)className;

                    if (identName.Identifier.ValueText != "")
                    {
                        //Check to make sure the first letter is capitalized
                        if (Char.IsLower(identName.Identifier.ValueText[0]))
                        {
                            const string DiagnosticId = "Nam0007";
                            DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                            //Creates a diagnostic at the location of the class name
                            var diagnostic = Diagnostic.Create(Rule, identName.Identifier.GetLocation(), "Class names should begin with a capital letter");
                            //Reports the problem in the code
                            context.ReportDiagnostic(diagnostic);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Analyzes the Method naming conventions to see if they comply with CIS 300 and CIS 400 at KSU
        /// </summary>
        /// <param name="context">The context of the Property</param>
        private static void AnalyzeNamingConventionsOfMethods(SyntaxNodeAnalysisContext context)
        {
            var classDeclaration = (ClassDeclarationSyntax)context.Node;
            //Loops through all of the descendant nodes at the top level
            foreach (var classes in classDeclaration.DescendantNodes())
            {
                //Checks to see if any of the descendant nodes are of type MethodDeclarationSyntax
                if (classes is MethodDeclarationSyntax)
                {
                    MethodDeclarationSyntax identName = (MethodDeclarationSyntax)classes;

                    if (identName.Identifier.ValueText != "")
                    {
                        //Check to make sure the first letter is capitalized
                        if (Char.IsLower(identName.Identifier.ValueText[0]))
                        {
                            const string DiagnosticId = "Nam0008";
                            DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                            //Creates a diagnostic at the location of the method name
                            var diagnostic = Diagnostic.Create(Rule, identName.Identifier.GetLocation(), "Method names should begin with a capital letter");
                            //Reports the problem in the code
                            context.ReportDiagnostic(diagnostic);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Analyzes the Parameter naming conventions to see if they comply with CIS 300 and CIS 400 at KSU
        /// </summary>
        /// <param name="context">The context of the Property</param>
        private static void AnalyzeNamingConventionsOfParameters(SyntaxNodeAnalysisContext context)
        {
            var classDeclaration = (ClassDeclarationSyntax)context.Node;
            //Loops through all of the descendant nodes at the top level
            foreach (var classNames in classDeclaration.DescendantNodes())
            {
                //Checks to see if any of the descendant nodes are of type MethodDeclarationSyntax
                if (classNames is MethodDeclarationSyntax)
                {
                    foreach(var methodNames in classNames.DescendantNodes())
                    {
                        //Checks to see if any of the descendant nodes are of type ParameterListSyntax
                        if (methodNames is ParameterListSyntax)
                        {
                            foreach(var parameterNames in methodNames.DescendantNodes())
                            {
                                //Checks to see if any of the descendant nodes are of type ParameterSyntax
                                if (parameterNames is ParameterSyntax)
                                {
                                    ParameterSyntax identName = (ParameterSyntax)parameterNames;

                                    if (identName.Identifier.ValueText != "")
                                    {
                                        //Check to make sure the first letter is not capitalized
                                        if (!Char.IsLower(identName.Identifier.ValueText[0]))
                                        {
                                            const string DiagnosticId = "Nam0009";
                                            DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                                            //Creates a diagnostic at the location of the parameter name
                                            var diagnostic = Diagnostic.Create(Rule, identName.Identifier.GetLocation(), "Parameter names should be in camel case");
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

        /// <summary>
        /// Analyzes the Local Variable naming conventions to see if they comply with CIS 300 and CIS 400 at KSU
        /// </summary>
        /// <param name="context">The context of the Property</param>
        private static void AnalyzeNamingConventionsOfLocalVariables(SyntaxNodeAnalysisContext context)
        {
            var methodDeclaration = (MethodDeclarationSyntax)context.Node;
            //Loops through all of the descendant nodes at the top level
            foreach (var block in methodDeclaration.DescendantNodes())
            {
                if(block is BlockSyntax)
                {
                    foreach (var localVariable in block.DescendantNodes())
                    {
                        //Checks to see if any of the descendant nodes are of type LocalDeclarationStatementSyntax
                        if (localVariable is LocalDeclarationStatementSyntax)
                        {
                            foreach(var variableDeclaration in localVariable.DescendantNodes())
                            {
                                //Checks to see if any of the descendant nodes are of type VariableDeclarationSyntax
                                if (variableDeclaration is VariableDeclarationSyntax)
                                {
                                    foreach(var variableDeclarator in variableDeclaration.DescendantNodes())
                                    {
                                        //Checks to see if any of the descendant nodes are of type VariableDeclaratorSyntax
                                        if (variableDeclarator is VariableDeclaratorSyntax)
                                        {
                                            VariableDeclaratorSyntax identName = (VariableDeclaratorSyntax)variableDeclarator;

                                            //Check to make sure the first letter is not capitalized
                                            if (!Char.IsLower(identName.Identifier.ValueText[0]))
                                            {
                                                const string DiagnosticId = "Nam0010";
                                                DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);
                                                //Creates a diagnostic at the location of the parameter name
                                                var diagnostic = Diagnostic.Create(Rule, identName.Identifier.GetLocation(), "Local variable names should be in camel case");
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
    }
}
