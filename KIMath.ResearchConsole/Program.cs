﻿using KIMath.BooleanAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.ResearchConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            AnalyzeOmegaComplexity();
            Console.ReadKey();
        }

        static void AnalyzeOmegaComplexity()
        {
            var a = ProcessorClassBooleanFunctions.GetAllPostClasses(4);
            var b = a;
        }
    }
}
