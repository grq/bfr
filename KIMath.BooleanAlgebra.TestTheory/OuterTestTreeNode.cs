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

        public List<bool[]> History { get; set; }

        public List<string> HistoryString 
        { 
            get
            {
                return this.History.Select(x => BooleanAlgebraHelper.BinaryToString(x)).OrderBy(x => x).ToList();
            }
        }

        public bool IsDeadlock { get; set; }

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
            if (BooleanAlgebraHelper.BinaryToDec(this.Input) > this.BiggestInputDec) // убираем возрастающий индекс
            {
                List<OuterTestFunctionsPair> result = new List<OuterTestFunctionsPair>();
                this.BiggestInputDec = BooleanAlgebraHelper.BinaryToDec(this.Input);
                this.History.Add(this.Input);
                foreach (OuterTestFunctionsPair pair in this.Pairs)
                {
                    result.AddRange(pair.Separate(this.Input));
                }
                this.IsDeadlock = true;
                result.ForEach(x => this.IsDeadlock = this.IsDeadlock && x.IsDeadlock);
                if (result.Count > 0)
                {
                    return new OuterTestTreeNode(result.Where(x => !x.IsDeadlock).ToList(), null, this);
                }
            }
            return null;
        }

        public OuterTestTreeNode CreateChildNode(bool[] input)
        {
            return new OuterTestTreeNode(this.Pairs, input, this);
        }
    }
}
