using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra.TestTheory
{
    internal class OuterTestTreeNode
    {
        public List<OuterTestFunctionsPair> Pairs { get; set; }

        public List<OuterTestFunctionsPair> ResultPairs { get; set; }

        public List<bool[]> History { get; set; }

        public List<string> HistoryString { get; set; }

        public bool Completed { get; set; }

        public bool[] Input { get; set; }

        public OuterTestTreeNode(IEnumerable<OuterTestFunctionsPair> pairs, bool[] input = null, OuterTestTreeNode parent = null)
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

        public BooleanFunctionTest GetTest()
        {
            return new BooleanFunctionTest(this.History);
        }

        public OuterTestTreeNode Process()
        {
            List<OuterTestFunctionsPair> result = new List<OuterTestFunctionsPair>();
            string inputString = BooleanAlgebraHelper.BinaryToString(this.Input);

            // убрать возрастающий индекс

            if (BooleanAlgebraHelper.BinaryToDec(this.Input) > this.BiggestInputDec)

            //
            //if (!this.HistoryString.Contains(inputString))
            {
                this.BiggestInputDec = BooleanAlgebraHelper.BinaryToDec(this.Input);
                this.History.Add(this.Input);
                this.HistoryString.Add(inputString);
                this.HistoryString = this.HistoryString.OrderBy(x => x).ToList();

                foreach (OuterTestFunctionsPair pair in this.Pairs)
                {
                    result.AddRange(pair.Separate(this.Input));
                }
                this.Completed = true;
                foreach (OuterTestFunctionsPair pair in result)
                {
                    if (!pair.Completed)
                    {
                        this.Completed = false;
                        break;
                    }
                }
                this.ResultPairs = result;
                return new OuterTestTreeNode(result.Where(x => !x.Completed).ToList(), null, this);
            }
            return null;
        }

        public OuterTestTreeNode CreateChildNode(bool[] input)
        {
            return new OuterTestTreeNode(this.Pairs, input, this);
        }
    }
}
