using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra.TestTheory
{
    /// <summary>
    /// Процессор для вычисления тестов для распознавания функций внутри множества функций
    /// </summary>
    public class InnerTestProcessor : CommonProcessor
    {
        /// <summary>
        /// Класс функций алгебры логики, для которого вычисляются тесты
        /// </summary>
        public PostClassBooleanFunctions PostClass { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="postClass">Класс функций</param>
        /// <param name="variables">Число переменных</param>
        /// <param name="useConsole">Логирование процесса вычислений в консоль</param>
        public InnerTestProcessor(PostClassBooleanFunctions postClass, int variables, bool useConsole = false)
        {
            this.PostClass = postClass;
            this.Variables = variables;
            this._useConsole = useConsole;
        }

        protected override void ProcessDeadlockTests()
        {
            this.ConsoleWriteLine(string.Format("Вычисление для класса: {0}", this.PostClass.PostPropertiesString));
            List<bool[]> inputs = TestTheoryCommon.ExcludeInputs(this.PostClass.Functions, this.Variables);
            /* Завершенные тесты */
            List<InnerTestTreeNode> completed = new List<InnerTestTreeNode>();
            /* Незавершенные тесте */
            List<InnerTestTreeNode> undone = new List<InnerTestTreeNode>();
            /* Создаём новый тест */
            InnerTestTreeNode test = new InnerTestTreeNode(this.Variables, this.PostClass.Functions, inputs);
            /* Первая итерация вычисления тестов */
            undone = test.Process();
            /* Переменная число итераций */
            this.ConsoleWriteLine("Итерации вычисления тестов:");
            int y = 0;
            /* Пока есть незавершенные тесты */
            while (undone.Count > 0)
            {
                /* Увеличиваем номер итерации на 1 */
                y++;
                this.ConsoleWrite(y + " ");
                /* Создаём новый массив тестов M */
                List<InnerTestTreeNode> newUndone = new List<InnerTestTreeNode>();
                /* Для каждого теста t */
                foreach (InnerTestTreeNode t in undone)
                {
                    /* В массив M добавляем результат итерации теста t */
                    newUndone.AddRange(t.Process());
                    /* Если тест t завершен */
                    if (t.IsCompleted)
                    {
                        /* Добавляем его в массив завершенных */
                        completed.Add(t);
                    }
                }
                /* Массиву незавершенных тестов присваиваем массив M */
                undone = newUndone;
            }
            this.ConsoleWriteLine();
            this.ConsoleWriteLine();
            this._deadlockTests = completed.Select(x => new BooleanFunctionTest(x.History)).Distinct().ToList();
        }
    }
}
