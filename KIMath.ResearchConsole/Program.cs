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
            for (int i = 0; i < 256;i++ )
            {
                BooleanFunction f = new BooleanFunction(i, 3);
                if (f.IsLinear)
                    Console.WriteLine(f);
            }




                Console.ReadKey();
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
