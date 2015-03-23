using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra.TestTheory
{
    /// <summary>
    /// Процессор для вычисления тупиковых и минимальных тестов
    /// </summary>
    public class InnerTestProcessor : CommonProcessor
    {
        /// <summary>
        /// Класс функций алгебры логики, для которого вычисляются тесты
        /// </summary>
        public PostClassBooleanFunctions PostClass { get; private set; }

        /// <summary>
        /// Число переменных
        /// </summary>
        public int Variables { get; private set; }

        /// <summary>
        /// Тупиковые тесты
        /// </summary>
        public List<BooleanFunctionTest> DeadlockTests
        {
            get
            {
                if (this._deadlockTests == null)
                {
                    this.ProcessDeadlockTests();
                }
                return this._deadlockTests;
            }
        }

        /// <summary>
        /// Минимальные тесты
        /// </summary>
        public List<BooleanFunctionTest> MinimalTests
        {
            get
            {
                if (this._minimalTests == null)
                {
                    this.ProcessMinimalTests();
                }
                return this._minimalTests;
            }
        }

        /// <summary>
        /// Длина минимального теста
        /// </summary>
        public int MinimalTestLength
        {
            get
            {
                if (!this._minimalTestLength.HasValue)
                {
                    this.ProcessMinimalTests();
                }
                return this._minimalTestLength.Value;
            }
        }

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

        private List<BooleanFunctionTest> _deadlockTests;

        private List<BooleanFunctionTest> _minimalTests;

        private int? _minimalTestLength;

        private void ProcessDeadlockTests()
        {
            this.ConsoleWriteLine(string.Format("Вычисление для класса: {0}", this.PostClass.PostPropertiesString));
            List<bool[]> inputs = TestTheoryCommon.ExcludeInputs(this.PostClass.Functions, this.Variables);
            /* Завершенные тесты */
            List<DeadlockTestInner> completed = new List<DeadlockTestInner>();
            /* Незавершенные тесте */
            List<DeadlockTestInner> undone = new List<DeadlockTestInner>();
            /* Создаём новый тест */
            DeadlockTestInner test = new DeadlockTestInner(this.Variables, this.PostClass.Functions, inputs);
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
                List<DeadlockTestInner> newUndone = new List<DeadlockTestInner>();
                /* Для каждого теста t */
                foreach (DeadlockTestInner t in undone)
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
            this.ConsoleWriteLine();
            this.ConsoleWriteLine();
            this._deadlockTests = completed.Select(x => new BooleanFunctionTest(x.History)).Distinct().ToList();
        }

        private void ProcessMinimalTests()
        {
            if (this._deadlockTests == null)
            {
                this.ProcessDeadlockTests();
            }
            if (this._deadlockTests.Count == 0)
            {
                this._minimalTestLength = 0;
                this._minimalTests = new List<BooleanFunctionTest>();
            }
            else
            {
                this._minimalTestLength = this._deadlockTests.Select(x => x.Length).Min();
                this._minimalTests = this._deadlockTests.Where(x => x.Length == this._minimalTestLength).Distinct().ToList();
            }
        }
    }
}
