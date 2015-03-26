using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra.TestTheory
{
    /// <summary>
    /// Базовый класс. Содержит логику для логирования процесса вычислений.
    /// </summary>
    public abstract class CommonProcessor
    {
        /// <summary>
        /// Число переменных
        /// </summary>
        public int Variables { get; protected set; }

        protected List<BooleanFunctionTest> _deadlockTests;

        protected List<BooleanFunctionTest> _minimalTests;

        protected int? _minimalTestLength;

        protected bool _useConsole;

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

        protected virtual void ProcessDeadlockTests()
        {
            throw new NotImplementedException();
        }

        protected void ProcessMinimalTests()
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

        protected void ConsoleWrite(string text = "")
        {
            if (this._useConsole)
            {
                Console.Write(text);
            }
        }

        protected void ConsoleWriteLine(string text = "")
        {
            if (this._useConsole)
            {
                Console.WriteLine(text);
            }
        }
    }
}
