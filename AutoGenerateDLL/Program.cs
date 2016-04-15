﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoGenerateDLL
{
    class Program
    {
        static void Main(string[] args)
        {
            var parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.OutputAssembly = "AutoGen.dll";

            var r = CodeDomProvider.CreateProvider("CSharp")
                .CompileAssemblyFromSource(parameters, "public class B {public static int k=7;}");

            //verify generation
            Console.WriteLine(Assembly.LoadFrom("AutoGen.dll").GetType("B").GetField("k").GetValue(null));
        }
    }
}
