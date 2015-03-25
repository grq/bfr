using KIMath.BooleanAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.ResearchConsole
{
    public class TreeNode
    {
        public List<FunctionsTestPair> Pairs { get; set; }

        public List<FunctionsTestPair> ResultPairs { get; set; }

        public List<bool[]> History { get; set; }

        public List<string> HistoryString { get; set; }

        public bool Completed { get; set; }

        public bool[] Input { get; set; }

        public TreeNode(IEnumerable<FunctionsTestPair> pairs, bool[] input = null, TreeNode parent = null)
        {
            this.Pairs = pairs.ToList();
            if (input != null)
            {
                this.Input = input;
            }
            this.BiggestInputDec = -1;
            this.History = new List<bool[]>();
            this.HistoryString = new List<string>();
            if (parent != null)
            {
                this.BiggestInputDec = parent.BiggestInputDec;
                this.History.AddRange(parent.History);
                this.HistoryString.AddRange(parent.HistoryString);
            }
        }

        public long BiggestInputDec { get; set; }

        public TreeNode Process()
        {
            List<FunctionsTestPair> result = new List<FunctionsTestPair>();
            string inputString = BooleanAlgebraHelper.BinaryToString(this.Input);

            // убрать возрастающий индекс

            if(BooleanAlgebraHelper.BinaryToDec(this.Input) > this.BiggestInputDec)

            //
            //if (!this.HistoryString.Contains(inputString))
            {
                this.BiggestInputDec = BooleanAlgebraHelper.BinaryToDec(this.Input);
                this.History.Add(this.Input);
                this.HistoryString.Add(inputString);
                this.HistoryString = this.HistoryString.OrderBy(x => x).ToList();

                foreach (FunctionsTestPair pair in this.Pairs)
                {
                    result.AddRange(pair.Separate(this.Input));
                }
                this.Completed = true;
                foreach (FunctionsTestPair pair in result)
                {
                    if (!pair.Completed)
                    {
                        this.Completed = false;
                        break;
                    }
                }
                this.ResultPairs = result;
                return new TreeNode(result.Where(x => !x.Completed).ToList(), null, this);
            }
            return null;
        }

        public TreeNode CreateChildNode(bool[] input)
        {
            return new TreeNode(this.Pairs, input, this);
        }
    }

    public class FunctionsTestPair
    {
        public List<BooleanFunction> FuncsA { get; set; }

        public List<BooleanFunction> FuncsB { get; set; }

        public List<FunctionsTestPair> Children { get; set; }

        public List<bool[]> History { get; set; }

        public bool Completed { get; set; }

        public FunctionsTestPair()
        {
            this.Children = new List<FunctionsTestPair>();
            this.FuncsA = new List<BooleanFunction>();
            this.FuncsB = new List<BooleanFunction>();
            this.History = new List<bool[]>();
        }

        public void IsCompleted()
        {
            this.Completed = this.FuncsA.Count == 0 || this.FuncsB.Count == 0;
        }

        public bool IsValid()
        {
            return this.FuncsA.Count > 0 || this.FuncsB.Count > 0;
        }

        public List<FunctionsTestPair> Separate(bool[] input)
        {
            List<FunctionsTestPair> result = new List<FunctionsTestPair>();
            this.History.Add(input);
            FunctionsTestPair ot0 = new FunctionsTestPair() { History = this.History };
            FunctionsTestPair ot1 = new FunctionsTestPair() { History = this.History };
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
            ot0.IsCompleted();
            ot1.IsCompleted();
            if (ot0.IsValid())
                result.Add(ot0);
            if (ot1.IsValid())
                result.Add(ot1);
            return result;
        }
    }
}
