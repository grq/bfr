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
            TestOmegaComplexityProcessor();
            Console.ReadKey();
        }

        static void TestOmegaComplexityProcessor()
        {
            string sequence = "31415926535897932384626433832795028841971693993751";
            OmegaComplexityProcessor ocp = new OmegaComplexityProcessor(sequence);
            Console.WriteLine("Omega 0: {0}", ocp.Omega0);
            Console.WriteLine("Omega 1:");
            foreach (int a in ocp.Omega1)
            {
                Console.WriteLine(a);
            }
            //Console.WriteLine("Absolute Omega 2: {0}", ocp.AbsoluteOmega2);
            Console.WriteLine("Omega 3:");
            foreach (List<int> a in ocp.Omega3)
            {
                foreach (int b in a)
                {
                    Console.WriteLine(b);
                }
            }
            //Console.WriteLine("Absolute Omega 3: {0}", ocp.AbsoluteOmega3);
        }

        static void ShowLinear()
        {
            int c = 1;
            for (int i = 0; i < 65536; i++)
            {
                BooleanFunction f = new BooleanFunction(i, 4);
                if (f.IsLinear)
                {
                    Console.WriteLine("{0}) {1}", c, f);
                    Console.WriteLine();
                    c++;
                }
            }
            Console.WriteLine("DONE");
        }

        static void HasRepeat(string[] arr)
        {
            for(int i = 0;i<arr.Length - 1;i++)
            {
                for (int j = i+1; j < arr.Length; j++)
                {
                    if (arr[i] == arr[j])
                        Console.WriteLine("ERROR: " + arr[i] + ". INDEX: " + (i + 1) + ", " + (j + 1));
                }
            }
            Console.WriteLine("GOOD");
        }
    }
}
