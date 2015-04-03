using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    /// <summary>
    /// Узел дерева для определения тестов для распознавания функций в классе
    /// </summary>
    internal class InnerTestTreeNode
    {
        /// <summary>
        /// Число переменных
        /// </summary>
        public int Variables { get; set; }

        /// <summary>
        /// Множество функций, для которых вычисляются тесты
        /// </summary>
        public List<List<BooleanFunction>> FunctionsLists { get; set; }

        /// <summary>
        /// Набор значений переменных текущего узла дерева
        /// </summary>
        public bool[] TestInput { get; private set; }

        /// <summary>
        /// Тест завершён
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// История тестов в наборах значений переменных
        /// </summary>
        public List<bool[]> History { get; set; }

        /// <summary>
        /// Наборы значений переменных на которых необходимо производить вычисление
        /// </summary>
        public List<bool[]> Inputs { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public InnerTestTreeNode() 
            : base()
        {
            this.History = new List<bool[]>();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="variables">Число переменных</param>
        public InnerTestTreeNode(int variables)
            : base()
        {
            this.Variables = variables;
            this.History = new List<bool[]>();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="variables">Число переменных</param>
        /// <param name="functions">Множество функций</param>
        /// <param name="inputs">Множество наборов, на которых производить сравнение</param>
        public InnerTestTreeNode(int variables, IEnumerable<BooleanFunction> functions, List<bool[]> inputs)
            : base()
        {
            this.Variables = variables;
            this.FunctionsLists = new List<List<BooleanFunction>>() { functions.ToList() };
            this.Inputs = inputs;
            this.History = new List<bool[]>();
        }

        /// <summary>
        /// Сделать прогон в поиске минимальных тестов. Прогоны используются для того, что бы небыло необходимости вычислять тесте рекурсивно.
        /// Рекурсивное вычисление тестов затрудняет ограниченное количество доступной машине памяти.
        /// </summary>
        /// <returns>Множество результирующих тестов для следующей итерации</returns>
        public List<InnerTestTreeNode> Process()
        {
            List<InnerTestTreeNode> result = new List<InnerTestTreeNode>();
            this.IsCompleted = this.FunctionsLists.Count == 0;
            if (this.IsCompleted)
            {
                this.History = this.History.OrderBy(x => BooleanAlgebraHelper.BinaryToDec(x)).ToList();
            }
            else
            {
                for (int i = 0; i < this.Inputs.Count; i++)
                {
                    bool[] input = this.Inputs[i];
                    List<List<BooleanFunction>> lists = new List<List<BooleanFunction>>();
                    foreach (var list in this.FunctionsLists)
                    {
                        List<BooleanFunction> positiveFunctions = new List<BooleanFunction>();
                        List<BooleanFunction> negativeFunctions = new List<BooleanFunction>();
                        foreach (BooleanFunction func in list)
                        {
                            if (func.GetByInput(input))
                                positiveFunctions.Add(func);
                            else
                                negativeFunctions.Add(func);
                        }
                        if (positiveFunctions.Count > 1)
                            lists.Add(positiveFunctions);
                        if (negativeFunctions.Count > 1)
                            lists.Add(negativeFunctions);
                    }
                    InnerTestTreeNode test = new InnerTestTreeNode(this.Variables);
                    test.History = this.History.Concat(new List<bool[]>() { input }).ToList();
                    test.FunctionsLists = lists;
                    test.Inputs = this.Inputs.GetRange(i + 1, this.Inputs.Count - i - 1);
                    result.Add(test);
                }
            }
            return result;
        }
    }
}
