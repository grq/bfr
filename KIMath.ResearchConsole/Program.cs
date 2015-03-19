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

        static void CreateOmega23Processor()
        {
            string _sequence = "31415926535897932384626433832795028841971693993751";
            //string _sequence = "nahsdasdsakjirjewomgksdmijnuiewnq;lkewrjnkngsd";

            List<int>  _omega2 = new List<int>();
            while (_omega2.LastOrDefault() != 1)
            {
                int length = _omega2.Count + 1;
                Console.WriteLine("Length {0}", length);
                int changes = 1;
                int lastChange = 0;
                List<ReccurentForm> currentForms = new List<ReccurentForm>();
                for (int start = 0; start < _sequence.Length - length; start++)
                {
                    ReccurentForm form = new ReccurentForm(_sequence, start, length);
                    if (currentForms.Contains(form))
                    {
                        changes++;
                        Console.WriteLine(start - lastChange);
                        lastChange = start;
                        currentForms = new List<ReccurentForm>();
                    }
                    currentForms.Add(form);
                }
                Console.WriteLine(_sequence.Length - length - lastChange);
                Console.WriteLine("Length {0} Total Changes: {1}", length, changes);
                Console.WriteLine();
                _omega2.Add(changes);
            }
        }

        static void TestOmegaComplexityProcessor()
        {
            string sequence = "31415926535897932384626433832795028841971693993751";
            OmegaComplexityProcessor ocp = new OmegaComplexityProcessor(sequence);
            Console.WriteLine("Omega 0: {0}", ocp.Omega0);
            Console.WriteLine("Omega 1:");
            foreach(int a in ocp.Omega1)
            {
                Console.WriteLine(a);
            }
            Console.WriteLine("Absolute Omega 2: {0}", ocp.AbsoluteOmega2);
            Console.WriteLine("Absolute Omega 3: {0}", ocp.AbsoluteOmega3);
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
