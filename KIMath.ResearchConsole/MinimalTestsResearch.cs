﻿using KIMath.BooleanAlgebra;
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
    }
}
