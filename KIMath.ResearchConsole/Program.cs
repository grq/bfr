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
            MinimalTestsResearch.Experiment(3, 3);
            //string[] intersection = new string[] { "A", "B", "C" };
            //string[] array1 = new string[] { "C", "A", "B", "Q", "W", "E", "R", "T", "Y", "U" };
            //string[] array2 = new string[] { "B", "C", "A", "I", "O", "P", "S", "D", "F", "G" };
            //string[] array3 = new string[] { "A", "C", "B", "H", "J", "K", "L", "Z", "X", "V" };
            //List<string[]> arrays = new List<string[]>() { array1, array2, array3 };
            //List<string> result = BooleanAlgebraHelper.GetIntersection<string>(arrays);
            //Console.WriteLine(BooleanAlgebraHelper.CollectionsAreEqualOrdered(intersection, result));
            Console.ReadKey();
        }
    }
}
