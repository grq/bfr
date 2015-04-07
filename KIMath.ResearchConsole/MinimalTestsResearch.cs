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
        /// Результаты представляются в виде .txt файла.
        /// </summary>
        /// <param name="variables">Число переменных</param>
        public static void ProcessMinimalInnerTests_TextFile(int variables)
        {
            StringBuilder resultSb = new StringBuilder();
            PostClassBooleanFunctions[] postClasses = ProcessorClassBooleanFunctions.GetPostClasses(variables).ToArray();
            foreach(PostClassBooleanFunctions postClass in postClasses)
            {
                InnerTestProcessor processor = new InnerTestProcessor(postClass, variables); 
                resultSb.AppendLine(string.Format("Класс {0}. Мощность: {1}. Минимальная длина теста: {2}. Число минимальных тестов: {3}.{4}",
                     postClass.PostPropertiesString, postClass.Functions.Count, processor.MinimalTestLength, processor.MinimalTests.Count,
                     processor.MinimalTests.Count > 0 ? " Тесты:" : string.Empty));
                foreach(BooleanFunctionTest test in processor.MinimalTests)
                {
                    resultSb.AppendLine(test.ToString());
                }
                resultSb.AppendLine();
            }
            string resultString = resultSb.ToString();
            string resultFileName = string.Format("ProcessMinimalInnerTests_TextFile_{0}_var.txt", variables);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(resultFileName))
            {
                file.Write(resultString);
            }
            ResearchUtilities.WriteTitle("РЕЗУЛЬТАТЫ");
            Console.Write(resultString);
            Console.WriteLine("Результаты сохранены в файл {0}", resultFileName);
            ResearchUtilities.WriteTitle("ЭКСПЕРИМЕНТ ЗАКОНЧЕН");
        }

        /// <summary>
        /// Для каждого класса функций алгебры логики, определённых сочетаниями свойств Поста
        /// вычислить минимальные тесты для отделения функций внутри каждого класса.
        /// Результаты представляются в виде .docx файла.
        /// </summary>
        /// <param name="variables">Число переменных</param>
        public static void ProcessMinimalInnerTests_WordFile(int variables)
        {
            Console.WriteLine("Идёт создание Word-файла...");
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
                PostClassBooleanFunctions[] postClasses = ProcessorClassBooleanFunctions.GetPostClasses(variables).ToArray();
                foreach (PostClassBooleanFunctions postClass in postClasses)
                {
                    InnerTestProcessor processor = new InnerTestProcessor(postClass, variables);
                    Word.Range rng1 = document.Range(document.Content.End - 1, document.Content.End);
                    rng1.Paste();
                    ResearchUtilities.ReplaceWordText(application, "88888", count.ToString());
                    ResearchUtilities.ReplaceWordText(application, "zzz", postClass.PostPropertiesString);
                    ResearchUtilities.ReplaceWordText(application, "7777", variables.ToString());
                    ResearchUtilities.ReplaceWordText(application, "W", processor.MinimalTests.Count.ToString());
                    ResearchUtilities.ReplaceWordText(application, "q", processor.MinimalTestLength.ToString());
                    count++;
                }
                rng.Delete();
                application.Documents.Save();
                application.Visible = true;
                Console.WriteLine("Word-файл создан. Нажмите любую клавишу для закрытия.");
                Console.ReadKey();
                application.Quit();
            }
            catch (Exception ex)
            {
                application.Quit();
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Для любого количества классов функций алгебры логики, определить тесты для отделения функций между классами
        /// </summary>
        /// <param name="variables"></param>
        public static void ProcessMinimalOuterTestsForSelected(int variables, List<PostClassBooleanFunctions> postClasses)
        {
            OuterTestProcessor otp = new OuterTestProcessor(variables, postClasses);
            ResearchUtilities.WriteTitle("ФУНКЦИИ КЛАССОВ");
            foreach (PostClassBooleanFunctions postClass in otp.PostClasses)
            {
                Console.WriteLine("Класс {0}:\n", postClass.PostPropertiesString);
                postClass.Functions.ToList().ForEach(x => Console.WriteLine(x.ToString()));
                Console.WriteLine();
            }
            ResearchUtilities.WriteTitle("ТУПИКОВЫЕ ТЕСТЫ");
            otp.DeadlockTests.ToList().ForEach(x => Console.WriteLine(x.ToString()));
            Console.WriteLine();
            ResearchUtilities.WriteTitle("МИНИМАЛЬНЫЕ ТЕСТЫ");
            Console.WriteLine("Минимальная длина теста: {0}\n", otp.MinimalTestLength);
            for (int i = 0; i < otp.MinimalTests.Count; i++)
            {
                Console.WriteLine("--- Тест {0} ---\n\n{1}\n\nПроверка:\n---", i + 1, otp.MinimalTests[i]);
                foreach (PostClassBooleanFunctions postClass in otp.PostClasses)
                {
                    postClass.Functions.ToList().ForEach(x => Console.WriteLine(BooleanAlgebraHelper.BinaryToString(x.GetByInputs(otp.MinimalTests[i].Inputs))));
                    Console.WriteLine("---");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Вычисление минимальных тестов для распознавания (отделение) функций между классами.
        /// Эксперимент проводится для всех классов функций алгебры логики по сочетаниям свойств Поста для функций от заданного числа переменных.
        /// Для проведения эксперимента задаётся число классов в одном сочетании
        /// </summary>
        /// <param name="variables">Число переменных</param>
        /// <param name="capacity">Число классов в одном сочетании</param>
        public static void ProcessMinimalOuterTests(int variables, int capacity)
        {
            string dateString = DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss");
            string resultFileName = string.Format("ProcessMinimalOuterTests_{0}.txt", dateString);
            string title = string.Format(
@"Вычисление минимальных тестов для распознавания (отделение) функций между классами.
Эксперимент проводится для всех классов функций алгебры логики по сочетаниям свойств Поста для функций от {0} переменных.
Эксперимент проводится для всех возможных сочетаний по {1} класса (-ов)",
                variables, capacity);
            StringBuilder resultString = new StringBuilder();
            Console.WriteLine(title);
            Console.WriteLine();
            resultString.AppendLine(title);
            resultString.AppendLine();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(resultFileName))
            {
                file.Write(resultString.ToString());
            }
            List<PostClassBooleanFunctions> classes = ProcessorClassBooleanFunctions.GetPostClasses(variables).ToList();
            List<List<PostClassBooleanFunctions>> combinations = BooleanAlgebraHelper.GetAllCombinations<PostClassBooleanFunctions>(classes, capacity);
            foreach(List<PostClassBooleanFunctions> combination in combinations)
            {
                OuterTestProcessor processor = new OuterTestProcessor(variables, combination, true);
                string classesString = string.Format("Классы: {0}", string.Join(" ", combination.Select(x => x.PostPropertiesString)));
                Console.WriteLine(classesString);
                resultString.AppendLine(classesString);
                string result = string.Format("Длина минимального теста: {0}. Число минимальных тестов: {1}.\r\nМинимальные тесты:", processor.MinimalTestLength, processor.MinimalTests.Count);
                Console.WriteLine(result);
                resultString.AppendLine(result);
                foreach(BooleanFunctionTest test in processor.MinimalTests)
                {
                    Console.WriteLine(test);
                    resultString.AppendLine(test.ToString());
                }
                Console.WriteLine();
                resultString.AppendLine();
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(resultFileName))
                {
                    file.Write(resultString.ToString());
                }
            }
        }

        /// <summary>
        /// Вычисляются все возможные сочетания классов Поста от N переменных. Число классов в каждом сочетании - CAPACITY.
        /// Для каждого сочетания вычисляются минимальные тесты.
        /// Внутри каждого сочетания для каждой пары классов вычисляются минимальные тесты, и получается объединение множеств тестов.
        /// Объединение тестов среди пар сравнивается с минимальными тестами между CAPACITY классов.
        /// </summary>
        /// <param name="variables">Число переменных</param>
        /// <param name="capacity">Число классов в одном сочетании</param>
        public static void CompareMinimalOuterTestsBetweenNandCapacity(int variables, int capacity)
        {
            string dateString = DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss");
            string resultFileName = string.Format("CompareMinimalOuterTestsBetweenNandCapacity_{0}.txt", dateString);
            string title = string.Format(
@"Вычисляются все возможные сочетания классов Поста от {0} переменных. Число классов в каждом сочетании - {1}.
Для каждого сочетания вычисляются минимальные тесты.
Внутри каждого сочетания для каждой пары классов вычисляются минимальные тесты, и получается объединение множеств тестов.
Объединение тестов среди пар сравнивается с минимальными тестами между {1} классов.", variables, capacity);
            StringBuilder resultString = new StringBuilder();
            Console.WriteLine(title);
            Console.WriteLine();
            resultString.AppendLine(title);
            resultString.AppendLine();
            int iteration = 1;
            List<PostClassBooleanFunctions> classes = ProcessorClassBooleanFunctions.GetPostClasses(variables).ToList();
            List<List<PostClassBooleanFunctions>> combinations = BooleanAlgebraHelper.GetAllCombinations<PostClassBooleanFunctions>(classes, capacity);
            foreach (List<PostClassBooleanFunctions> combination in combinations)
            {
                OuterTestProcessor totalProcessor = new OuterTestProcessor(variables, combination);
                List<BooleanFunctionTest> totalTests = totalProcessor.MinimalTests;
                List<List<PostClassBooleanFunctions>> subCombinations = BooleanAlgebraHelper.GetAllCombinations<PostClassBooleanFunctions>(combination, 2);
                List<List<BooleanFunctionTest>> subTests = new List<List<BooleanFunctionTest>>();
                foreach(List<PostClassBooleanFunctions> subCombination in subCombinations)
                {
                    OuterTestProcessor subProcessor = new OuterTestProcessor(variables, subCombination);
                    subTests.Add(subProcessor.MinimalTests);
                }
                List<BooleanFunctionTest> subTestsIntersection = SetTheoryHelper.GetIntersection<BooleanFunctionTest>(subTests).ToList();
                bool isIntersection = BooleanAlgebraHelper.CollectionsAreEqualNotOrdered(totalTests, subTestsIntersection);
                bool isOriginalIncludesIntersection = SetTheoryHelper.IsInclusion(totalTests, subTestsIntersection);
                bool isIntersectionIncludesOriginal = SetTheoryHelper.IsInclusion(subTestsIntersection, totalTests);
                string resultTitle = string.Format("{0}) Для классов {1}", iteration, string.Join(" - ", combination.Select(x => x.PostPropertiesString)));
                resultString.AppendLine(resultTitle);
                Console.WriteLine(resultTitle);
                resultString.AppendLine();
                if (isIntersection)
                {
                    var a = isIntersection;
                }
                string result1 = string.Format("Является пересечением: {0}", isIntersection ? "ДААААААААААААААААААААААААААААААААА" : "Нет");
                string result2 = string.Format("Пересечение входит в множество оригинальных тестов: {0}", isOriginalIncludesIntersection ? "ДААААААААААААААААААААААААААААААААА" : "Нет");
                string result3 = string.Format("Множество оригинальных тестов входит в пересечение: {0}", isIntersectionIncludesOriginal ? "ДААААААААААААААААААААААААААААААААА" : "Нет");
                resultString.AppendLine(result1);
                resultString.AppendLine(result2);
                resultString.AppendLine(result3);
                resultString.AppendLine();
                Console.WriteLine(result1);
                Console.WriteLine(result2);
                Console.WriteLine(result3);
                Console.WriteLine();
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(resultFileName))
                {
                    file.Write(resultString.ToString());
                }
                iteration++;
            }
        }

        public static void TestToFunction(int variables)
        {
            int capacity = 2;
            List<PostClassBooleanFunctions> classes = ProcessorClassBooleanFunctions.GetPostClasses(variables).ToList();
            List<List<PostClassBooleanFunctions>> combinations = BooleanAlgebraHelper.GetAllCombinations<PostClassBooleanFunctions>(classes, capacity);
            foreach(List<PostClassBooleanFunctions> combination in combinations)
            {
                OuterTestProcessor processor = new OuterTestProcessor(variables, combination);
                List<BooleanFunctionTest> minimalTests = processor.MinimalTests;
                ClassBooleanFunctions functionsOfTests = new ClassBooleanFunctions();
                foreach(BooleanFunctionTest test in minimalTests)
                {
                    var starter = GetFunctionByInputs(variables, test.Inputs);
                    functionsOfTests.AddFunction(new BooleanFunction(starter, variables));
                }

                ResearchUtilities.WriteTitle(string.Format("КЛАССЫ: {0}", string.Join(" ", combination.Select(x => x.PostPropertiesString))));

                /* Показатели Omega0 */
                if (functionsOfTests.Omega0G0Average == functionsOfTests.Omega0G1Average)
                {
                    Console.WriteLine("Omega0 g0 и g1 среднее: {0}", functionsOfTests.Omega0G0Average);
                }
                else
                {
                    Console.WriteLine("Omega0 g0 среднее {0}", functionsOfTests.Omega0G0Average);
                    Console.WriteLine("Omega0 g1 среднее {0}", functionsOfTests.Omega0G1Average);
                }
                if (functionsOfTests.Omega0G0Min == functionsOfTests.Omega0G1Min)
                {
                    Console.WriteLine("Omega0 g0 и g1 наименьшее: {0}", functionsOfTests.Omega0G0Min);
                }
                else
                {
                    Console.WriteLine("Omega0 g0 наименьшее {0}", functionsOfTests.Omega0G0Min);
                    Console.WriteLine("Omega0 g1 наименьшее {0}", functionsOfTests.Omega0G1Min);
                }
                if (functionsOfTests.Omega0G0Max == functionsOfTests.Omega0G1Max)
                {
                    Console.WriteLine("Omega0 g0 и g1 наибольшее: {0}", functionsOfTests.Omega0G0Max);
                }
                else
                {
                    Console.WriteLine("Omega0 g0 наибольшее {0}", functionsOfTests.Omega0G0Max);
                    Console.WriteLine("Omega0 g1 наибольшее {0}", functionsOfTests.Omega0G1Max);
                }
                Console.WriteLine();


                /* Показатели Omega 1*/
                if (functionsOfTests.Omega1G0Average == functionsOfTests.Omega1G1Average)
                {
                    Console.WriteLine("Omega1 g0 и g1 среднее: {0}", functionsOfTests.Omega1G0Average);
                }
                else
                {
                    Console.WriteLine("Omega1 g0 среднее {0}", functionsOfTests.Omega1G0Average);
                    Console.WriteLine("Omega1 g1 среднее {0}", functionsOfTests.Omega1G1Average);
                }
                if (functionsOfTests.Omega1G0Min == functionsOfTests.Omega1G1Min)
                {
                    Console.WriteLine("Omega1 g0 и g1 наименьшее: {0}", functionsOfTests.Omega1G0Min);
                }
                else
                {
                    Console.WriteLine("Omega1 g0 наименьшее {0}", functionsOfTests.Omega1G0Min);
                    Console.WriteLine("Omega1 g1 наименьшее {0}", functionsOfTests.Omega1G1Min);
                }
                if (functionsOfTests.Omega1G0Max == functionsOfTests.Omega1G1Max)
                {
                    Console.WriteLine("Omega1 g0 и g1 наибольшее: {0}", functionsOfTests.Omega1G0Max);
                }
                else
                {
                    Console.WriteLine("Omega1 g0 наибольшее {0}", functionsOfTests.Omega1G0Max);
                    Console.WriteLine("Omega1 g1 наибольшее {0}", functionsOfTests.Omega1G1Max);
                }
                Console.WriteLine();

                /* Показатели Omega2 */
                if (functionsOfTests.Omega2G0Average == functionsOfTests.Omega2G1Average)
                {
                    Console.WriteLine("Omega2 g0 и g1 среднее: {0}", functionsOfTests.Omega2G0Average);
                }
                else
                {
                    Console.WriteLine("Omega2 g0 среднее {0}", functionsOfTests.Omega2G0Average);
                    Console.WriteLine("Omega2 g1 среднее {0}", functionsOfTests.Omega2G1Average);
                }
                if (functionsOfTests.Omega2G0Min == functionsOfTests.Omega2G1Min)
                {
                    Console.WriteLine("Omega2 g0 и g1 наименьшее: {0}", functionsOfTests.Omega2G0Min);
                }
                else
                {
                    Console.WriteLine("Omega2 g0 наименьшее {0}", functionsOfTests.Omega2G0Min);
                    Console.WriteLine("Omega2 g1 наименьшее {0}", functionsOfTests.Omega2G1Min);
                }
                if (functionsOfTests.Omega2G0Max == functionsOfTests.Omega2G1Max)
                {
                    Console.WriteLine("Omega2 g0 и g1 наибольшее: {0}", functionsOfTests.Omega2G0Max);
                }
                else
                {
                    Console.WriteLine("Omega2 g0 наибольшее {0}", functionsOfTests.Omega2G0Max);
                    Console.WriteLine("Omega2 g1 наибольшее {0}", functionsOfTests.Omega2G1Max);
                }
                Console.WriteLine();

                /* Показатели Omega3 */
                if (functionsOfTests.Omega3G0Average == functionsOfTests.Omega3G1Average)
                {
                    Console.WriteLine("Omega3 g0 и g1 среднее: {0}", functionsOfTests.Omega3G0Average);
                }
                else
                {
                    Console.WriteLine("Omega3 g0 среднее {0}", functionsOfTests.Omega3G0Average);
                    Console.WriteLine("Omega3 g1 среднее {0}", functionsOfTests.Omega3G1Average);
                }
                if (functionsOfTests.Omega3G0Min == functionsOfTests.Omega3G1Min)
                {
                    Console.WriteLine("Omega3 g0 и g1 наименьшее: {0}", functionsOfTests.Omega3G0Min);
                }
                else
                {
                    Console.WriteLine("Omega3 g0 наименьшее {0}", functionsOfTests.Omega3G0Min);
                    Console.WriteLine("Omega3 g1 наименьшее {0}", functionsOfTests.Omega3G1Min);
                }
                if (functionsOfTests.Omega3G0Max == functionsOfTests.Omega3G1Max)
                {
                    Console.WriteLine("Omega3 g0 и g1 наибольшее: {0}", functionsOfTests.Omega3G0Max);
                }
                else
                {
                    Console.WriteLine("Omega3 g0 наибольшее {0}", functionsOfTests.Omega3G0Max);
                    Console.WriteLine("Omega3 g1 наибольшее {0}", functionsOfTests.Omega3G1Max);
                }
                Console.WriteLine();

                /* Показатели повторности */
                Console.WriteLine("Повторность средняя: {0}", functionsOfTests.RepetivityAverage);
                Console.WriteLine("Повторность наименьшая: {0}", functionsOfTests.RepetivityMin);
                Console.WriteLine("Повторность наибольшая: {0}", functionsOfTests.RepetivityMax);

                Console.WriteLine();
            }
        }

        private static bool[] GetFunctionByInputs(int variables, List<bool[]> inputs)
        {
            List<bool[]> allInputs = BooleanAlgebraHelper.GetAllInputs(variables);
            List<bool> result = new List<bool>();
            foreach(bool[] input in allInputs)
            {
                bool[] contains = inputs.FirstOrDefault(x => BooleanAlgebraHelper.CollectionsAreEqualOrdered<bool>(x, input));
                result.Add(contains != null);
            }
            return result.ToArray();
        }
    }
}
