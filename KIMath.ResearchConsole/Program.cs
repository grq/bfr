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
            Test();
            Console.ReadKey();
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
            if (completed.Count > 0)
            {
                minTestLength = completed.Select(x => x.History.Count).Min();
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
        }
    }
}
