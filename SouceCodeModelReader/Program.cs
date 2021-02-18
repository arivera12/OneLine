using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SouceCodeModelReader
{
    class ClassModelsCollector : CSharpSyntaxWalker
    {
        public ClassesInformation ClassesInformation { get; set; }
        public ClassModelsCollector()
        {
            ClassesInformation = new ClassesInformation();
        }
        int count = 0;
        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var methodDeclaration = node.Ancestors().FirstOrDefault(x => x.Kind() == SyntaxKind.MethodDeclaration) as MethodDeclarationSyntax;
            if (methodDeclaration.Identifier.ValueText.Equals("OnModelCreating"))
            {
                base.VisitInvocationExpression(node);
                Console.WriteLine(++count);
               
            }
            
                //base.VisitInvocationExpression(node);
        }
        public override void VisitBlock(BlockSyntax node)
        {
            //var methodDeclaration = node.Parent as MethodDeclarationSyntax;
            //if (methodDeclaration.Identifier.ValueText.Equals("OnModelCreating"))
            //{
            //    foreach (var item in node.DescendantNodes())
            //    {

            //    }
            //}
            base.VisitBlock(node);
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
                        protected override void OnModelCreating(ModelBuilder modelBuilder)
                        {
                            modelBuilder.Entity<AccessLogs>(entity =>
                            {
                                entity.HasKey(e => e.AccessLogId)
                                    .HasName(""PK_AccessLog"");
                                entity.Property(e => e.AccessLogId).HasMaxLength(128);
                                entity.Property(e => e.AccessDate).HasColumnType(""datetime"");
                                entity.Property(e => e.AccessStatusId)
                                    .IsRequired()
                                    .HasMaxLength(128);
                                entity.Property(e => e.AccessTypeId)
                                    .IsRequired()
                                    .HasMaxLength(128);
                                entity.Property(e => e.AccessedUserId).HasMaxLength(450);
                                entity.Property(e => e.AllowedByUserId).HasMaxLength(450);
                                entity.Property(e => e.ComplexId)
                                    .IsRequired()
                                    .HasMaxLength(128);
                                entity.Property(e => e.CreatedBy)
                                    .IsRequired()
                                    .HasMaxLength(128);
                                entity.Property(e => e.CreatedOn).HasColumnType(""datetime"");
                                entity.Property(e => e.FullName).HasMaxLength(128);
                                entity.Property(e => e.InternalNotes).HasMaxLength(1024);
                                entity.Property(e => e.LastModifiedBy).HasMaxLength(128);
                                entity.Property(e => e.LastModifiedOn).HasColumnType(""datetime"");
                                entity.Property(e => e.LicensePlate).HasMaxLength(10);
                                entity.HasOne(d => d.AccessStatus)
                                    .WithMany(p => p.AccessLogs)
                                    .HasForeignKey(d => d.AccessStatusId)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName(""FK_AccessLog_AccessStatus"");
                                entity.HasOne(d => d.AccessType)
                                    .WithMany(p => p.AccessLogs)
                                    .HasForeignKey(d => d.AccessTypeId)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName(""FK_AccessLog_AccessTypes"");
                                entity.HasOne(d => d.AccessedUser)
                                    .WithMany(p => p.AccessLogsAccessedUser)
                                    .HasForeignKey(d => d.AccessedUserId)
                                    .HasConstraintName(""FK_AccessLog_AspNetUsers1"");
                                entity.HasOne(d => d.AllowedByUser)
                                    .WithMany(p => p.AccessLogsAllowedByUser)
                                    .HasForeignKey(d => d.AllowedByUserId)
                                    .HasConstraintName(""FK_AccessLog_AspNetUsers"");
                                entity.HasOne(d => d.Complex)
                                    .WithMany(p => p.AccessLogs)
                                    .HasForeignKey(d => d.ComplexId)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName(""FK_AccessLog_Complexes"");
                            });
                        }
                        modelBuilder.Entity<AccessStatuses>(entity =>
                        {
                                entity.HasKey(e => e.AccessStatusId)
                                .HasName(""PK_AccessStatus"");
                                entity.Property(e => e.AccessStatusId).HasMaxLength(128);
                                entity.Property(e => e.AccessStatusDescription)
                                    .IsRequired()
                                    .HasMaxLength(128);
                                entity.Property(e => e.CreatedBy)
                                    .IsRequired()
                                    .HasMaxLength(128);
                                entity.Property(e => e.CreatedOn).HasColumnType(""datetime"");
                                entity.Property(e => e.LastModifiedBy).HasMaxLength(128);
                                entity.Property(e => e.LastModifiedOn).HasColumnType(""datetime"");
                        });
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
