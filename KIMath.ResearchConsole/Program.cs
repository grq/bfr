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
            string[] selfDualInputs = new string[] { "10010110" };
            foreach (string input in selfDualInputs)
            {
                BooleanFunction function = new BooleanFunction(input, (int)Math.Log(input.Length, 2));
                Console.WriteLine(function.IsSelfDual);
            }
            Console.ReadKey();
        }
    }
}
