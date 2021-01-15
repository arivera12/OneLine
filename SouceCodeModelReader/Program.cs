using System;

namespace SouceCodeModelReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    class ClassModelsCollector : CSharpSyntaxWalker
    {
        public ClassesInformation ClassesInformation { get; set; }
        public ClassModelsCollector()
        {
            ClassesInformation = new ClassesInformation();
        }
        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            base.VisitMethodDeclaration(node);  
        }
        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            var classnode = node.Parent as ClassDeclarationSyntax;
            if (!ClassesInformation.Classes.ContainsKey(classnode.Identifier.ValueText))
            {
                ClassesInformation.Classes.Add(classnode.Identifier.ValueText, new List<PropertyInformation>());
            }
            ClassesInformation.Classes[classnode.Identifier.ValueText].Add(new PropertyInformation()
            {
                PropertyName = node.Identifier.ValueText,
                PropertyType = node.Type.ToString(),
                PropertyModifiers = node.Modifiers.Select(s => s.ValueText)
            });
        }
    }
    class ClassesInformation
    {
        public ClassesInformation()
        {
            Classes = new Dictionary<string, IList<PropertyInformation>>();
        }
        public Dictionary<string, IList<PropertyInformation>> Classes { get; set; }
    }
    class PropertyInformation
    {
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public IEnumerable<string> PropertyModifiers { get; set; }
    }

    class Program
    {
        static void Main()
        {
            var code = @"
                using System;
                using System.Collections.Generic;
                using System.Linq;
                using System.Text;

                namespace HelloWorld
                {
                    public class MyAwesomeModel
                    {
                        public string MyProperty {get;set;}
                        public int MyProperty1 {get;set;}
                        public myClass MyProperty2 {get;set;}
                        public void MyMethod() {}
                    }
                    public class MyAwesomeViewModel
                    {
                        public string MyProperty {get;set;}
                        public int MyProperty1 {get;set;}
                        public myClass MyProperty2 {get;set;} 
                    }
                }";

            var tree = CSharpSyntaxTree.ParseText(code);

            var root = (CompilationUnitSyntax)tree.GetRoot();
           
            var classModelsCollector = new ClassModelsCollector();
            classModelsCollector.Visit(root);

            Console.WriteLine(JsonSerializer.Serialize(classModelsCollector.ClassesInformation.Classes, new JsonSerializerOptions() { WriteIndented = true }));
        }
    }
}
