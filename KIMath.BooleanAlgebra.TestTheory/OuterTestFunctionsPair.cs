using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra.TestTheory
{
    /// <summary>
    /// Пара множеств функций для вычислений теста по распознаванию (отделению) функций в этих множествах
    /// </summary>
    internal class OuterTestFunctionsPair
    {
        /// <summary>
        /// Множество функций алгебры логики
        /// </summary>
        public List<BooleanFunction> FuncsA { get; set; }

        /// <summary>
        /// Множество функций алгебры логики
        /// </summary>
        public List<BooleanFunction> FuncsB { get; set; }

        /// <summary>
        /// Тест завершён
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public OuterTestFunctionsPair()
        {
            this.InitArrays();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="listA">Множество функций алгебры логики</param>
        /// <param name="listB">Множество функций алгебры логики</param>
        public OuterTestFunctionsPair(IEnumerable<BooleanFunction> listA, IEnumerable<BooleanFunction> listB)
        {
            this.InitArrays();
            this.FuncsA = listA.ToList();
            this.FuncsB = listB.ToList();
        }

        /// <summary>
        /// Инициализация массивов
        /// </summary>
        private void InitArrays()
        {
            this.FuncsA = new List<BooleanFunction>();
            this.FuncsB = new List<BooleanFunction>();
        }

        /// <summary>
        /// Определить содержит ли каждое из множеств функций хотя бы одну функцию
        /// В процессе выполнения функции вычисляется завершённость (IsCompleted) теста
        /// </summary>
        /// <returns>Пара множеств валидна</returns>
        public bool IsValid()
        {
            this.IsCompleted = this.FuncsA.Count == 0 || this.FuncsB.Count == 0;
            return this.FuncsA.Count > 0 || this.FuncsB.Count > 0;
        }

        /// <summary>
        /// Разделить множества функций на наборе
        /// </summary>
        /// <param name="input">Набор значений переменных</param>
        /// <returns>Множество результирующих пар множеств функций</returns>
        public List<OuterTestFunctionsPair> Separate(bool[] input)
        {
            List<OuterTestFunctionsPair> result = new List<OuterTestFunctionsPair>();
            OuterTestFunctionsPair ot0 = new OuterTestFunctionsPair();
            OuterTestFunctionsPair ot1 = new OuterTestFunctionsPair();
            foreach (BooleanFunction f in this.FuncsA)
            {
                if (f.GetByInput(input))
                {
                    ot1.FuncsA.Add(f);
                }
                else
                {
                    ot0.FuncsA.Add(f);
                }
            }
            foreach (BooleanFunction f in this.FuncsB)
            {
                if (f.GetByInput(input))
                {
                    ot1.FuncsB.Add(f);
                }
                else
                {
                    ot0.FuncsB.Add(f);
                }
            }
            if (ot0.IsValid())
            {
                result.Add(ot0);
            }
            if (ot1.IsValid())
            {
                result.Add(ot1);
            }
            return result;
        }
    }
}
