using KIMath.BooleanAlgebra;
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
            //MinimalTestsResearch.ProcessInnerTests(3);
            var a = BooleanAlgebraHelper.GetAllInputs(4);
            Console.ReadKey();
        }

        static void AnalyzeOmegaComplexity()
        {
        }
    }
}
