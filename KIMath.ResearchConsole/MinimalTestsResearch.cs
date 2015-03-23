using KIMath.BooleanAlgebra;
using KIMath.BooleanAlgebra.TestTheory;
using KIMath.ResearchConsole.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace KIMath.ResearchConsole
{
    public static class MinimalTestsResearch
    {
        /// <summary>
        /// Для каждого класса функций алгебры логики, определённых сочетаниями свойств Поста
        /// вычислить минимальные тесты для отделения функций внутри каждого класса.
        /// Результаты представляются в виде .docx файла.
        /// </summary>
        /// <param name="variables">Число переменных</param>
        public static void ProcessMinimalInnerTests_WordFile(int variables)
        {
            Word.Application application = new Word.Application();
            Word.Document document;
            try
            {
                int count = 1;

                string resultFileName = "InnerTestsResult.docx";
                File.WriteAllBytes(resultFileName, Resources.Mockup_ProcessMinimalInnerTests_Word);
                document = application.Documents.Open(string.Format("{0}\\{1}", Directory.GetCurrentDirectory(), resultFileName));
                Word.Range rng = document.Range(0, document.Content.End - 1);
                rng.Copy();

                Dictionary<string, List<BooleanFunction>> classFunctions = new Dictionary<string, List<BooleanFunction>>();
                for (int i = 0; i < BooleanAlgebraHelper.FunctionsOfVariables(variables); i++)
                {
                    BooleanFunction func = new BooleanFunction(i, variables);
                    string className = func.GetPostClassString();
                    if (!classFunctions.Keys.Contains(className))
                        classFunctions.Add(className, new List<BooleanFunction>());
                    classFunctions[className].Add(func);
                }

                Console.WriteLine("Functions detected");
                Console.WriteLine();
                foreach (string postClass in classFunctions.Keys)
                {
                    List<bool[]> allInputs = new List<bool[]>();
                    for (int i = 0; i < Math.Pow(2, variables); i++)
                    {
                        allInputs.Add(BooleanAlgebraHelper.GetCompletedBinaryFormByVariables(i, variables));
                    }
                    List<bool[]> inputs = new List<bool[]>();
                    foreach (bool[] input in allInputs)
                    {
                        bool inputResult = false;
                        bool? comparer = null;
                        foreach (BooleanFunction func in classFunctions[postClass])
                        {
                            if (!comparer.HasValue)
                            {
                                comparer = func.GetByInput(input);
                            }
                            else
                            {
                                if (comparer.Value != func.GetByInput(input))
                                {
                                    inputResult = true;
                                    break;
                                }
                            }
                        }
                        if (inputResult)
                        {
                            inputs.Add(input);
                        }
                    }
                    List<MinimalTest> completed = new List<MinimalTest>();
                    List<MinimalTest> undone = new List<MinimalTest>();
                    MinimalTest test = new MinimalTest();
                    test.Variables = variables;
                    test.FunctionsLists = new List<List<BooleanFunction>>() { classFunctions[postClass] };
                    test.Inputs = inputs;
                    undone = test.Process();

                    int y = 0;
                    while (undone.Count > 0)
                    {
                        y++;
                        Console.Write(y + " ");
                        List<MinimalTest> newUndone = new List<MinimalTest>();
                        foreach (MinimalTest t in undone)
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
                    Word.Range rng1 = document.Range(document.Content.End - 1, document.Content.End);
                    rng1.Paste();
                    ResearchUtilities.ReplaceWordText(application, "00000", count.ToString());
                    ResearchUtilities.ReplaceWordText(application, "zzz", postClass);
                    ResearchUtilities.ReplaceWordText(application, "7777", variables.ToString());
                    ResearchUtilities.ReplaceWordText(application, "k", completed.Count.ToString());
                    ResearchUtilities.ReplaceWordText(application, "W", res != null ? res.Count.ToString() : "");
                    ResearchUtilities.ReplaceWordText(application, "q", minTestLength.ToString());
                    count++;
                    Console.WriteLine(postClass + ": " + minTestLength);
                }
                application.Documents.Save();
                application.Visible = true;
                Console.WriteLine("Done");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                application.Quit();
            }
        }

        /// <summary>
        /// Для каждого класса функций алгебры логики, определённых сочетаниями свойств Поста
        /// вычислить минимальные тесты для отделения функций внутри каждого класса.
        /// Результаты представляются в виде .txt файла.
        /// </summary>
        /// <param name="variables">Число переменных</param>
        public static void ProcessMinimalInnerTests_TextFile(int variables)
        {
            ResearchUtilities.WriteTitle("ЭКСПЕРИМЕНТ НАЧАТ");
            PostClassBooleanFunctions[] postClasses = ProcessorClassBooleanFunctions.GetPostClasses(variables).ToArray();
            Console.WriteLine("Получено разделение функций на классы");
            Console.WriteLine();
            StringBuilder resultString = new StringBuilder();
            ResearchUtilities.WriteTitle("ВЫЧИСЛЕНИЯ");
            foreach (PostClassBooleanFunctions postClass in postClasses)
            {
                Console.WriteLine("Вычисление для класса: {0}", postClass.PostPropertiesString);
                List<bool[]> inputs = ExcludeInputs(postClass.Functions, variables);
                /* Завершенные тесты */
                List<MinimalTest> completed = new List<MinimalTest>();
                /* Незавершенные тесте */
                List<MinimalTest> undone = new List<MinimalTest>();
                /* Создаём новый тест */
                MinimalTest test = new MinimalTest(variables, postClass.Functions, inputs);
                /* Первая итерация вычисления тестов */
                undone = test.Process();
                /* Переменная число итераций */
                Console.WriteLine("Итерации вычисления тестов:");
                int y = 0;
                /* Пока есть незавершенные тесты */
                while (undone.Count > 0)
                {
                    /* Увеличиваем номер итерации на 1 */
                    y++;
                    Console.Write(y + " ");
                    /* Создаём новый массив тестов M */
                    List<MinimalTest> newUndone = new List<MinimalTest>();
                    /* Для каждого теста t */
                    foreach (MinimalTest t in undone)
                    {
                        /* В массив M добавляем результат итерации теста t */
                        newUndone.AddRange(t.Process());
                        /* Если тест t завершен */
                        if (t.Comleted)
                        {
                            /* Добавляем его в массив завершенных */
                            completed.Add(t);
                        }
                    }
                    /* Массиву незавершенных тестов присваиваем массив M */
                    undone = newUndone;
                }
                Console.WriteLine();
                Console.WriteLine();
                int minTestLength = 0;
                List<string> res = null;
                if (completed.Count > 0)
                {
                    minTestLength = completed.Select(x => x.History.Count).Min();
                    res = completed.Where(x => x.History.Count == minTestLength).Select(x => x.HistoryString).Distinct().ToList();
                }
                resultString.AppendLine(string.Format("Класс {0}. Мощность: {1}. Минимальная длина теста: {2}. Число минимальных тестов: {3}.{4}",
                    postClass.PostPropertiesString, postClass.Functions.Count, minTestLength, res != null ? res.Count.ToString() : "0",
                    res != null ? " Тесты:" : string.Empty));
                if (res != null)
                {
                    foreach (string inp in res)
                    {
                        resultString.AppendLine(inp);
                    }
                }
                resultString.AppendLine();
            }
            string result = resultString.ToString();
            string resultFileName = string.Format("ProcessMinimalInnerTests_TextFile_{0}_var.txt", variables);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(resultFileName))
            {
                file.Write(result);
            }
            ResearchUtilities.WriteTitle("РЕЗУЛЬТАТЫ");
            Console.Write(result);
            Console.WriteLine("Результаты сохранены в файл {0}", resultFileName);
            ResearchUtilities.WriteTitle("ЭКСПЕРИМЕНТ ЗАКОНЧЕН");
        }

        /// <summary>
        /// Получить все наборы переменных, на которых значения хотя бы для двух функций различаются 
        /// </summary>
        /// <param name="functions">Множество функций</param>
        /// <param name="variables">Число переменных</param>
        private static List<bool[]> ExcludeInputs(IEnumerable<BooleanFunction> functions, int variables)
        {

            List<bool[]> allInputs = BooleanAlgebraHelper.GetAllInputs(variables);
            List<bool[]> inputs = new List<bool[]>();
            /* Для каждого набора переменных из всех возможных */
            foreach (bool[] input in allInputs)
            {
                bool inputResult = false;
                bool? comparer = null;
                /* Просматриваем каждую функцию */
                foreach (BooleanFunction func in functions)
                {
                    /* Если сравнитель не определён */
                    if (!comparer.HasValue)
                    {
                        /* Присваиваем ему значение первой функции на этом наборе */
                        comparer = func.GetByInput(input);
                    }
                    /* Если сравнитель определён */
                    else
                    {
                        /* Сравниваем его со значением функции на этом наборе */
                        if (comparer.Value != func.GetByInput(input))
                        {
                            /* Если значения не совпадают... */
                            inputResult = true;
                            break;
                        }
                    }
                }
                /* ...добавляем этот набор в результат */
                if (inputResult)
                {
                    inputs.Add(input);
                }
            }
            return inputs;
        }

        public static void UseProcessor(int variables)
        {
            PostClassBooleanFunctions[] postClasses = ProcessorClassBooleanFunctions.GetPostClasses(variables).ToArray();
            foreach(PostClassBooleanFunctions postClass in postClasses)
            {
                Console.WriteLine("-------- CLASS: " + postClass.PostPropertiesString);
                DeadlockTestProcessor processor = new DeadlockTestProcessor(postClass, variables);
                foreach(BooleanFunctionTest test in processor.MinimalTests)
                {
                    Console.WriteLine(test);
                }
            }
        }
    }
}
