using KIMath.BooleanAlgebra;
using KIMath.BooleanAlgebra.TestTheory; //todo remove
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
            //MinimalTestsResearch.ProcessMinimalOuterTests(3, 8);
            //var oldtt = MinimalTestsResearch.GetOldHash(4);
            //var newt = MinimalTestsResearch.GetNewHash(3);

            string resultFileName = string.Format("result_{0}.txt", DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss"));
            int variables = 4;
            List<int> hash = new List<int>();
            List<PostClassBooleanFunctions> classes = ProcessorClassBooleanFunctions.GetPostClasses(variables).ToList();
            StringBuilder sb = new StringBuilder();
            List<List<PostClassBooleanFunctions>> combinations = BooleanAlgebraHelper.GetAllCombinations<PostClassBooleanFunctions>(classes, 2);
            foreach (List<PostClassBooleanFunctions> combination in combinations)
            {
                OuterTestProcessor otp = new OuterTestProcessor(variables, combination, true);
                sb.AppendLine(string.Format("{0} {1} {2} {3}", combination[0].PostPropertiesString, combination[1].PostPropertiesString,
                         otp.MinimalTestLength, otp.MinimalTests.Count));
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(resultFileName))
                {
                    file.Write(sb.ToString());
                }
                hash.Add(otp.MinimalTestLength);
                hash.Add(otp.MinimalTests.Count);
            }
            var a = hash;

            //List<int> hash = new List<int>();
            //List<PostClassBooleanFunctions> combination = new List<PostClassBooleanFunctions>();
            //combination.Add(ProcessorClassBooleanFunctions.GetPostClass("00000", 4));
            //combination.Add(ProcessorClassBooleanFunctions.GetPostClass("00100", 4));
            //OuterTestProcessor otp = new OuterTestProcessor(4, combination);
            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(resultFileName))
            //{
            //    string str = string.Format("{0} {1} {2} {3}")
            //    file.Write(resultString);
            //}
            //hash.Add(otp.MinimalTestLength);
            //hash.Add(otp.MinimalTests.Count);

            int[] oldt = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 2, 6, 1, 6, 1, 1, 2, 1, 2, 1, 2, 1, 
                2, 6, 1, 6, 1, 1, 2, 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 12, 1, 2, 1, 2, 
                1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 2, 1, 8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 4, 1, 4, 1, 1, 6, 1, 6, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 12, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 4, 1, 1, 6, 1, 1, 2, 1, 2, 
                6, 1, 3, 2, 6, 1, 6, 1, 1, 2, 1, 2, 4, 9, 6, 1, 2, 9, 6, 1, 3, 8, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 
                1, 2, 1, 2, 2, 12, 3, 8, 3, 8, 1, 6, 3, 8, 2, 12 };
            //bool res = BooleanAlgebraHelper.CollectionAreEquals<int>(oldt, oldtt);

            //Console.WriteLine(res);

            //List<PostClassBooleanFunctions> clss = new List<PostClassBooleanFunctions>();
            //clss.Add(ProcessorClassBooleanFunctions.GetPostClass("11101", 3));
            //clss.Add(ProcessorClassBooleanFunctions.GetPostClass("11110", 3));
            //clss.Add(ProcessorClassBooleanFunctions.GetPostClass("11111", 3));
            //MinimalTestsResearch.ProcessMinimalOuterTestsForSelected(3, clss);
            Console.WriteLine("DONE");
            Console.ReadKey();

            /* 
             
1 1
1 1
1 1
1 1
1 1
1 1
1 2
1 2
14 1
14 1
1 2
1 2
1 2
1 2
14 1
14 1
1 2
1 2
1 2
1 1
1 1
1 1
1 1
1 1
1 1
1 1
1 1
3 224
1 2
1 2
1 2
1 1
1 1
1 1
1 1
1 1
1 1
1 1
1 1
1 2
1 2
1 16
1 1
1 1
1 1
1 1
1 1
1 5
1 1
1 1
14 1
14 1
1 1
1 1
1 1
1 1
1 1
1 1
1 1
1 1
3 224
1 1
1 1
1 1
1 1
1 1
1 1
1 1
1 1
1 1
1 1
1 1
1 1
1 1
1 5
1 1
1 1
14 1
1 2
1 2
14 1
8 1
14 1
14 1
1 2
1 2
8 1
14 1
3 24
8 1
7 128
1 2
1 2
1 2
1 2
1 2
1 2
1 2
1 2
4 16
7 128
7 128
2 24
4 16
3 224



             
             */
        }
    }
}
