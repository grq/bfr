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
            MinimalTestsResearch.ProcessMinimalOuterTests(3, 3);
            Console.ReadKey();
        }
    }
}
