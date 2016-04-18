using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CSharp;

namespace WfDesignerWpf.Services
{
    public class GenerateDllService
    {
        public void Generate(string className, IDictionary<string, Type> props, string dllsPath)
        {
            var csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } });
            var outputFile = $"{className}.dll";
            var parameters = CreateParameters(dllsPath, outputFile);
            var compileUnit = new CodeCompileUnit();
            var ns = new CodeNamespace($"Models.{className}");
            compileUnit.Namespaces.Add(ns);
            ns.Imports.Add(new CodeNamespaceImport("System"));

            var classType = new CodeTypeDeclaration(className);
            classType.Attributes = MemberAttributes.Public;
            ns.Types.Add(classType);

            foreach (var prop in props)
            {
                AddProperty(prop, classType);
            }

            var results = csc.CompileAssemblyFromDom(parameters, compileUnit);
            results.Errors.Cast<CompilerError>().ToList().ForEach(error => Console.WriteLine(error.ErrorText));
        }

        private static CompilerParameters CreateParameters(string dllsPath, string outputFile)
        {
            var parameters = new CompilerParameters(new[] {"mscorlib.dll", "System.Core.dll"}, outputFile, false);
            parameters.GenerateExecutable = false;
            parameters.CompilerOptions = $"/out:{dllsPath}\\{outputFile}";
            return parameters;
        }

        private static void AddProperty(KeyValuePair<string, Type> prop, CodeTypeDeclaration classType)
        {
            var fieldName = "_" + prop.Key;
            var field = new CodeMemberField(prop.Value, fieldName);
            classType.Members.Add(field);

            var property = new CodeMemberProperty();
            property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            property.Type = new CodeTypeReference(prop.Value);
            property.Name = prop.Key;
            property.GetStatements.Add(
                new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName)));
            property.SetStatements.Add(
                new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName),
                    new CodePropertySetValueReferenceExpression()));
            classType.Members.Add(property);
        }

    }
}