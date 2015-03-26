using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra.TestTheory
{
    public class OuterTestProcessor: CommonProcessor
    {
        public int Variables { get; private set; }

        public bool Result { get; private set; }

        public List<PostClassBooleanFunctions> PostClasses { get; private set; }

        public OuterTestProcessor(int variables, params PostClassBooleanFunctions[] list)
        {
            this.Variables = variables;
            this.PostClasses = list.ToList();
        }

        public OuterTestProcessor(int variables, IEnumerable<PostClassBooleanFunctions> postClasses)
        {
            this.Variables = variables;
            this.PostClasses = postClasses.ToList();
        }

        private List<int> Process()
        {
            List<int> resultHash = new List<int>();
            List<PostClassBooleanFunctions> postClasses = ProcessorClassBooleanFunctions.GetPostClasses(this.Variables).ToList();
            Console.WriteLine("Functions detected");
            Console.WriteLine();
            postClasses = postClasses.OrderBy(x => BooleanAlgebraHelper.BinaryToDec(x.PostPropertiesString)).ToList();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("result3.txt"))
            {
                int count = 0;
                for (int a = 0; a < postClasses.Count - 1; a++)
                {
                    PostClassBooleanFunctions firstClass = postClasses[a];
                    for (int b = a + 1; b < postClasses.Count; b++)
                    {
                        PostClassBooleanFunctions secondClass = postClasses[b];
                        if (true)
                        {
                            count++;
                            List<BooleanFunction> functions = new List<BooleanFunction>();
                            // помечаем функции первого класса
                            firstClass.Functions.ToList().ForEach(x => x.Mark = "In");
                            // помечаем функции второго класса
                            secondClass.Functions.ToList().ForEach(x => x.Mark = "Out");
                            // складываем все функции в один массив
                            functions.AddRange(firstClass.Functions);
                            functions.AddRange(secondClass.Functions);
                            // убираем ненужные наборы переменных
                            List<bool[]> inputs = TestTheoryCommon.ExcludeInputs(functions, this.Variables);
                            List<OuterTest> completed = new List<OuterTest>(); // незаконченные тесты
                            List<OuterTest> undone = new List<OuterTest>(); // законченные тесты
                            OuterTest test = new OuterTest(this.Variables, functions, inputs);
                            // первая итерация тестов
                            undone = test.Process();
                            int y = 0;
                            while (undone.Count > 0)
                            {
                                y++;
                                //Console.Write(y + " ");
                                List<OuterTest> newUndone = new List<OuterTest>();
                                foreach (OuterTest t in undone)
                                {
                                    newUndone.AddRange(t.Process());
                                    if (t.Comleted)
                                        completed.Add(t);
                                }
                                undone = newUndone;
                            }
                            Console.WriteLine();
                            int minTestLength = 0;
                            List<string> res = null;
                            if (completed.Count > 0)
                            {
                                minTestLength = completed.Select(x => x.History.Count).Min();
                                res = completed.Where(x => x.History.Count == minTestLength).Select(x => x.HistoryString).Distinct().ToList();
                            }
                            string info1 = string.Format("{4}) Класс 1: {0} ({1}) - Класс 2: {2} ({3}):",
                                firstClass, functions.Where(x => x.Mark == "In").ToList().Count, secondClass, functions.Where(x => x.Mark == "Out").ToList().Count, count);

                            string info2 = string.Format("Минимальная длина теста: {0}. Число минимальных тестов: {1}.", minTestLength, res != null ? res.Count.ToString() : "");

                            resultHash.Add(minTestLength);
                            resultHash.Add(res != null ? res.Count : 0);

                            file.WriteLine(info1);
                            file.WriteLine(info2);
                            Console.WriteLine(info1);
                            Console.WriteLine(info2);
                            if (res != null)
                            {
                                foreach (string inp in res)
                                {
                                    file.Write("( " + inp + ") ");
                                    //Console.Write("( " + inp + ") ");
                                    file.WriteLine();
                                    //Console.WriteLine();
                                }
                            }
                            file.WriteLine();
                        }
                    }
                }
            }
            Console.WriteLine("Done");
            return resultHash;
        }

        private List<int> Process2()
        {
            List<int> resultHash = new List<int>();
            List<PostClassBooleanFunctions> postClasses = ProcessorClassBooleanFunctions.GetPostClasses(this.Variables).ToList();
            Console.WriteLine("Functions detected");
            Console.WriteLine();
            postClasses = postClasses.OrderBy(x => BooleanAlgebraHelper.BinaryToDec(x.PostPropertiesString)).ToList();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("result3.txt"))
            {
                int count = 0;
                for (int a = 0; a < postClasses.Count - 1; a++)
                {
                    PostClassBooleanFunctions firstClass = postClasses[a];
                    for (int b = a + 1; b < postClasses.Count; b++)
                    {
                        PostClassBooleanFunctions secondClass = postClasses[b];
                        if (true)
                        {
                            count++;
                            List<BooleanFunction> functions = new List<BooleanFunction>();
                            // помечаем функции первого класса
                            firstClass.Functions.ToList().ForEach(x => x.Mark = "In");
                            // помечаем функции второго класса
                            secondClass.Functions.ToList().ForEach(x => x.Mark = "Out");
                            // складываем все функции в один массив
                            functions.AddRange(firstClass.Functions);
                            functions.AddRange(secondClass.Functions);
                            // убираем ненужные наборы переменных
                            List<bool[]> inputs = TestTheoryCommon.ExcludeInputs(functions, this.Variables);
                            List<OuterTest> completed = new List<OuterTest>(); // незаконченные тесты
                            List<OuterTest> undone = new List<OuterTest>(); // законченные тесты
                            OuterTest test = new OuterTest(this.Variables, functions, inputs);
                            // первая итерация тестов
                            undone = test.Process2();
                            int y = 0;
                            while (undone.Count > 0)
                            {
                                y++;
                                //Console.Write(y + " ");
                                List<OuterTest> newUndone = new List<OuterTest>();
                                foreach (OuterTest t in undone)
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
                            if (completed.Count > 0)
                            {
                                minTestLength = completed.Select(x => x.History.Count).Min();
                                res = completed.Where(x => x.History.Count == minTestLength).Select(x => x.HistoryString).Distinct().ToList();
                            }
                            string info1 = string.Format("{4}) Класс 1: {0} ({1}) - Класс 2: {2} ({3}):",
                                firstClass, functions.Where(x => x.Mark == "In").ToList().Count, secondClass, functions.Where(x => x.Mark == "Out").ToList().Count, count);

                            string info2 = string.Format("Минимальная длина теста: {0}. Число минимальных тестов: {1}.", minTestLength, res != null ? res.Count.ToString() : "");

                            resultHash.Add(minTestLength);
                            resultHash.Add(res != null ? res.Count : 0);

                            file.WriteLine(info1);
                            file.WriteLine(info2);
                            Console.WriteLine(info1);
                            Console.WriteLine(info2);
                            if (res != null)
                            {
                                foreach (string inp in res)
                                {
                                    file.Write("( " + inp + ") ");
                                    //Console.Write("( " + inp + ") ");
                                    file.WriteLine();
                                    //Console.WriteLine();
                                }
                            }
                            file.WriteLine();
                        }
                    }
                }
            }
            Console.WriteLine("Done");
            return resultHash;
        }

        protected override void ProcessDeadlockTests()
        {
            List<BooleanFunction> allFunctions = this.PostClasses.SelectMany(x => x.Functions).ToList();
            List<bool[]> inputs = TestTheoryCommon.ExcludeInputs(allFunctions, this.Variables);
            List<OuterTestFunctionsPair> basePairs = new List<OuterTestFunctionsPair>();
            for (int i = 0; i < this.PostClasses.Count - 1; i++)
            {
                for (int j = i + 1; j < this.PostClasses.Count; j++)
                {
                    basePairs.Add(new OuterTestFunctionsPair(this.PostClasses[i].Functions, this.PostClasses[j].Functions));
                }
            }
            List<OuterTestTreeNode> currentNodes = new List<OuterTestTreeNode>();
            List<OuterTestTreeNode> deadLockNodes = new List<OuterTestTreeNode>();
            foreach (bool[] input in inputs)
            {
                OuterTestTreeNode node = new OuterTestTreeNode(basePairs, input);
                OuterTestTreeNode resNode = node.Process();
                if (node.Completed)
                {
                    deadLockNodes.Add(node);
                }
                else if (node.ResultPairs != null)
                {
                    currentNodes.Add(resNode);
                }
            }
            while (currentNodes.Count > 0)
            {
                List<OuterTestTreeNode> newNodes = new List<OuterTestTreeNode>();
                foreach (OuterTestTreeNode node in currentNodes)
                {
                    foreach (bool[] input in inputs)
                    {
                        OuterTestTreeNode childNode = node.CreateChildNode(input);
                        OuterTestTreeNode resultTreeNode = childNode.Process();
                        if (childNode.Completed)
                        {
                            deadLockNodes.Add(childNode);
                        }
                        else if (resultTreeNode != null)
                        {
                            newNodes.Add(resultTreeNode);
                        }
                    }
                }
                currentNodes = newNodes;
            }
            this._deadlockTests = deadLockNodes.Select(x => x.GetTest()).Distinct().ToList();
        }
    }
}
