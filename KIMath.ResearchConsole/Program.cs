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
            //MinimalTestsResearch.ProcessMinimalOuterTests(3);
            NewTest();
            Console.ReadKey();
        }

        static void NewTest()
        {
            int vars = 3;
            List<PostClassBooleanFunctions> postClasses = ProcessorClassBooleanFunctions.GetPostClasses(vars).ToList();
            List<BooleanFunction> F1 = new List<BooleanFunction>() { new BooleanFunction("01111111", vars), new BooleanFunction("01011111", vars) };
            List<BooleanFunction> F2 = new List<BooleanFunction>() { new BooleanFunction("00111111", vars) };
            List<BooleanFunction> F3 = new List<BooleanFunction>() { new BooleanFunction("10111111", vars) };
            List<bool[]> allInputs = BooleanAlgebraHelper.GetAllInputs(vars);

            List<FunctionsTestPair> basePairs = new List<FunctionsTestPair>();
            basePairs.Add(new FunctionsTestPair() { FuncsA = F1, FuncsB = F2 });
            basePairs.Add(new FunctionsTestPair() { FuncsA = F1, FuncsB = F3 });
            basePairs.Add(new FunctionsTestPair() { FuncsA = F2, FuncsB = F3 });

            List<TreeNode> resultNodes = new List<TreeNode>();

            List<TreeNode> nodes = new List<TreeNode>();
            List<TreeNode> completed = new List<TreeNode>();
            foreach (bool[] input in allInputs)
            {
                TreeNode node = new TreeNode(basePairs, input);
                TreeNode resNode = node.Process();
                if (node.Completed)
                {
                    completed.Add(node);
                }
                else if (node.ResultPairs != null)
                {
                    nodes.Add(resNode);
                }
            }
            while(nodes.Count > 0)
            {
                List<TreeNode> newNodes = new List<TreeNode>();
                foreach(TreeNode node in nodes)
                {
                    foreach (bool[] input in allInputs)
                    {
                        TreeNode childNode = node.CreateChildNode(input);
                        TreeNode resultTreeNode = childNode.Process();
                        if (childNode.Completed)
                        {
                            completed.Add(childNode);
                        }
                        else if (resultTreeNode != null)
                        {
                            newNodes.Add(resultTreeNode);
                        }
                    }
                }
                nodes = newNodes;
            }
            var res = completed.Select(x => x.HistoryString).Distinct();
        }

        static void Test()
        {
            int vars = 3;
            List<PostClassBooleanFunctions> postClasses = ProcessorClassBooleanFunctions.GetPostClasses(vars).ToList();
            var list1 = postClasses[6];
            var list2 = postClasses[1];
            var list3 = postClasses[3];


            List<BooleanFunction> functions = new List<BooleanFunction>();
            // помечаем функции первого класса
            list1.Functions.ToList().ForEach(x => x.Mark = "1");
            // помечаем функции второго класса
            list2.Functions.ToList().ForEach(x => x.Mark = "2");
            list3.Functions.ToList().ForEach(x => x.Mark = "3");
            // складываем все функции в один массив
            functions.AddRange(list1.Functions);
            functions.AddRange(list2.Functions);
            functions.AddRange(list3.Functions);
            // убираем ненужные наборы переменных
            List<bool[]> inputs = TestTheoryCommon.ExcludeInputs(functions, vars);
            List<DeadlockTestOuter> completed = new List<DeadlockTestOuter>(); // незаконченные тесты
            List<DeadlockTestOuter> undone = new List<DeadlockTestOuter>(); // законченные тесты
            DeadlockTestOuter test = new DeadlockTestOuter(vars, functions, inputs);
            test.Marks = new List<string>() { "1", "2", "3" };
            // первая итерация тестов
            undone = test.Process2();
            int y = 0;
            while (undone.Count > 0)
            {
                y++;
                //Console.Write(y + " ");
                List<DeadlockTestOuter> newUndone = new List<DeadlockTestOuter>();
                foreach (DeadlockTestOuter t in undone)
                {
                    newUndone.AddRange(t.Process2());
                    if (t.Comleted)
                        completed.Add(t);
                }
                undone = newUndone;
            }
            Console.WriteLine();
            int minTestLength = 0;
            List<string> res = null;
            List<List<bool[]>> tests = new List<List<bool[]>>();
            if (completed.Count > 0)
            {
                minTestLength = completed.Select(x => x.History.Count).Min();
                tests = completed.Select(x => x.History).ToList();
                res = completed.Where(x => x.History.Count == minTestLength).Select(x => x.HistoryString).Distinct().ToList();
            }
            string info1 = string.Format("{4}) Класс 1: {0} ({1}) - Класс 2: {2} ({3}) - Класс 3: {5} ({6}):",
                list1.PostPropertiesString, functions.Where(x => x.Mark == "1").ToList().Count, 
                list2.PostPropertiesString, functions.Where(x => x.Mark == "2").ToList().Count, 1,
                list3.PostPropertiesString, functions.Where(x => x.Mark == "3").ToList().Count);

            string info2 = string.Format("Минимальная длина теста: {0}. Число минимальных тестов: {1}.", minTestLength, res != null ? res.Count.ToString() : "");


            Console.WriteLine(info1);
            Console.WriteLine(info2);
            if (res != null)
            {
                foreach (string inp in res)
                {
                    Console.Write("( " + inp + ") ");
                    Console.WriteLine();
                }
            }

            // проверка
            var deadlocktest = tests[0];
            Console.WriteLine("K1 - K2");
            foreach (BooleanFunction f1 in list1.Functions)
            {
                string f1String = BooleanAlgebraHelper.BinaryToString(f1.GetByInputs(deadlocktest));
                foreach (BooleanFunction f2 in list2.Functions)
                {
                    string f2String = BooleanAlgebraHelper.BinaryToString(f2.GetByInputs(deadlocktest));
                   // Console.WriteLine("{0} - {1}", f1String, f2String);
                    if (f1String == f2String)
                    {
                        Console.WriteLine("{0} - {1}", f1String, f2String);
                        Console.WriteLine("Error!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                }
            }

            Console.WriteLine("K1 - K3");
            foreach (BooleanFunction f1 in list1.Functions)
            {
                string f1String = BooleanAlgebraHelper.BinaryToString(f1.GetByInputs(deadlocktest));
                foreach (BooleanFunction f2 in list3.Functions)
                {
                    string f2String = BooleanAlgebraHelper.BinaryToString(f2.GetByInputs(deadlocktest));
                  //  Console.WriteLine("{0} - {1}", f1String, f2String);
                    if (f1String == f2String)
                    {
                        Console.WriteLine("{0} - {1}", f1String, f2String);
                        Console.WriteLine("Error!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                }
            }

            Console.WriteLine("K2 - K3");
            foreach (BooleanFunction f1 in list2.Functions)
            {
                string f1String = BooleanAlgebraHelper.BinaryToString(f1.GetByInputs(deadlocktest));
                foreach (BooleanFunction f2 in list3.Functions)
                {
                    string f2String = BooleanAlgebraHelper.BinaryToString(f2.GetByInputs(deadlocktest));
                  //  Console.WriteLine("{0} - {1}", f1String, f2String);
                    if (f1String == f2String)
                    {
                        Console.WriteLine("{0} - {1}", f1String, f2String);
                        Console.WriteLine("Error!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                }
            }

            Console.WriteLine("DONE");
        }
    }
}
