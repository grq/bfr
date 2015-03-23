﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    public class MinimalTest : MinimalTestCommon
    {
        public bool Comleted { get; set; }

        public int Variables { get; set; }

        public List<List<BooleanFunction>> FunctionsLists { get; set; }

        public bool[] TestInput { get; set; }

        public string HistoryString { get; set; }

        public MinimalTest() 
            : base()
        {
            this.History = new List<bool[]>();
        }

        public MinimalTest(int variables)
            : base()
        {
            this.Variables = variables;
            this.History = new List<bool[]>();
        }

        public MinimalTest(int variables, IEnumerable<BooleanFunction> functions, List<bool[]> inputs)
            : base()
        {
            this.Variables = variables;
            this.FunctionsLists = new List<List<BooleanFunction>>() { functions.ToList() };
            this.Inputs = inputs;
            this.History = new List<bool[]>();
        }

        public void GetAllMinimalTests()
        {

        }

        /// <summary>
        /// Сделать прогон в поиске минимальных тестов. Прогоны используются для того, что бы небыло необходимости вычислять тесте рекурсивно.
        /// Рекурсивное вычисление тестов затрудняет ограниченное количество доступной машине памяти.
        /// </summary>
        /// <returns></returns>
        public List<MinimalTest> Process()
        {
            List<MinimalTest> result = new List<MinimalTest>();
            this.Comleted = this.FunctionsLists.Count == 0;
            if (this.Comleted)
            {
                this.History = this.History.OrderBy(x => BooleanAlgebraHelper.BinaryToDec(x)).ToList();
                this.HistoryString = string.Join(string.Empty, this.History.Select(x => "{" + BooleanAlgebraHelper.BinaryToString(x, ", ") + "}; "));
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
                    MinimalTest test = new MinimalTest(this.Variables);
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
