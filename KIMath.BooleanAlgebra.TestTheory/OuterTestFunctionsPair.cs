using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra.TestTheory
{
    internal class OuterTestFunctionsPair
    {
        public List<BooleanFunction> FuncsA { get; set; }

        public List<BooleanFunction> FuncsB { get; set; }

        public List<OuterTestFunctionsPair> Children { get; set; }

        public List<bool[]> History { get; set; }

        public bool Completed { get; set; }

        public OuterTestFunctionsPair()
        {
            this.InitArrays();
        }

        public OuterTestFunctionsPair(IEnumerable<BooleanFunction> listA, IEnumerable<BooleanFunction> listB)
        {
            this.InitArrays();
            this.FuncsA = listA.ToList();
            this.FuncsB = listB.ToList();
        }

        private void InitArrays()
        {
            this.Children = new List<OuterTestFunctionsPair>();
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

        public List<OuterTestFunctionsPair> Separate(bool[] input)
        {
            List<OuterTestFunctionsPair> result = new List<OuterTestFunctionsPair>();
            this.History.Add(input);
            OuterTestFunctionsPair ot0 = new OuterTestFunctionsPair() { History = this.History };
            OuterTestFunctionsPair ot1 = new OuterTestFunctionsPair() { History = this.History };
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
