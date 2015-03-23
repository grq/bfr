using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    public class MinimalTestOuter : MinimalTestCommon
    {
        public bool Comleted { get; set; }

        public int Variables { get; set; }

        public List<List<BooleanFunction>> FunctionsLists { get; set; }

        public bool[] TestInput { get; set; }

        public string HistoryString { get; set; }

        public MinimalTestOuter()
            : base()
        {
            this.History = new List<bool[]>();
        }

        public MinimalTestOuter(int variables)
            : base()
        {
            this.Variables = variables;
            this.History = new List<bool[]>();
        }

        public List<MinimalTestOuter> Process()
        {
            List<MinimalTestOuter> result = new List<MinimalTestOuter>();
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
                        List<BooleanFunction> positiveFunctionsIn = new List<BooleanFunction>();
                        List<BooleanFunction> negativeFunctionsIn = new List<BooleanFunction>();
                        List<BooleanFunction> positiveFunctionsOut = new List<BooleanFunction>();
                        List<BooleanFunction> negativeFunctionsOut = new List<BooleanFunction>();
                        foreach (BooleanFunction func in list)
                        {
                            if (func.GetByInput(input))
                            {
                                if (func.Mark == "In")
                                    positiveFunctionsIn.Add(func);
                                else if (func.Mark == "Out")
                                    positiveFunctionsOut.Add(func);
                            }
                            else
                            {
                                if (func.Mark == "In")
                                    negativeFunctionsIn.Add(func);
                                else if (func.Mark == "Out")
                                    negativeFunctionsOut.Add(func);
                            }
                        }
                        if (positiveFunctionsOut.Count > 0 && positiveFunctionsIn.Count > 0)
                        {
                            lists.Add(positiveFunctionsOut.Concat(positiveFunctionsIn).ToList());
                        }
                        if (negativeFunctionsOut.Count > 0 && negativeFunctionsIn.Count > 0)
                        {
                            lists.Add(negativeFunctionsOut.Concat(negativeFunctionsIn).ToList());
                        }
                    }
                    MinimalTestOuter test = new MinimalTestOuter(this.Variables);
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
