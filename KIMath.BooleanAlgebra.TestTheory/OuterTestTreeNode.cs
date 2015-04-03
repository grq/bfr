using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra.TestTheory
{
    /// <summary>
    /// Узел дерева для определения тестов для распознавания (отделения) функций между множествами функций
    /// </summary>
    internal class OuterTestTreeNode
    {
        /// <summary>
        /// Пары множеств функций для распознавания
        /// </summary>
        public List<OuterTestFunctionsPair> Pairs { get; set; }

        /// <summary>
        /// Множества наборов переменных
        /// </summary>
        public List<bool[]> History { get; set; }

        /// <summary>
        /// Тест завершён
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Текущий набор значений переменных
        /// </summary>
        public bool[] Input { get; set; }

        public long BiggestInputDec { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pairs">Пары множеств функций для распознавания (отделения)</param>
        /// <param name="input">Текущий набор значений переменных</param>
        /// <param name="parent">Родительский узел</param>
        public OuterTestTreeNode(IEnumerable<OuterTestFunctionsPair> pairs, bool[] input = null, OuterTestTreeNode parent = null)
        {
            this.Pairs = pairs.ToList();
            if (input != null)
            {
                this.Input = input;
            }
            this.BiggestInputDec = -1;
            this.History = new List<bool[]>();
            if (parent != null)
            {
                this.BiggestInputDec = parent.BiggestInputDec;
                this.History.AddRange(parent.History);
            }
        }

        /// <summary>
        /// Получить тест (результат)
        /// </summary>
        /// <returns>Тест</returns>
        public BooleanFunctionTest GetTest()
        {
            return new BooleanFunctionTest(this.History);
        }

        /// <summary>
        /// Сделать прогон в поиске тестов. Прогоны используются для того, что бы небыло необходимости вычислять тесты рекурсивно.
        /// Рекурсивное вычисление тестов затрудняет ограниченное количество доступной машине памяти.
        /// </summary>
        /// <returns>Множество результирующих тестов для следующей итерации</returns>
        public OuterTestTreeNode Process()
        {
            if (BooleanAlgebraHelper.BinaryToDec(this.Input) > this.BiggestInputDec) // убираем возрастающий индекс
            {
                List<OuterTestFunctionsPair> result = new List<OuterTestFunctionsPair>();
                this.BiggestInputDec = BooleanAlgebraHelper.BinaryToDec(this.Input);
                this.History.Add(this.Input);
                foreach (OuterTestFunctionsPair pair in this.Pairs)
                {
                    result.AddRange(pair.Separate(this.Input));
                }
                this.IsCompleted = true;
                result.ForEach(x => this.IsCompleted = this.IsCompleted && x.IsCompleted);
                if (result.Count > 0)
                {
                    return new OuterTestTreeNode(result.Where(x => !x.IsCompleted).ToList(), null, this);
                }
            }
            return null;
        }

        /// <summary>
        /// Создать дочерний узер на определённом наборе переменных
        /// </summary>
        /// <param name="input">Набор значений переменных</param>
        /// <returns>Дочерний тест</returns>
        public OuterTestTreeNode CreateChildNode(bool[] input)
        {
            return new OuterTestTreeNode(this.Pairs, input, this);
        }
    }
}
