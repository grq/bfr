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

        public bool IsDeadlock { get; set; }

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
            this.FuncsA = new List<BooleanFunction>();
            this.FuncsB = new List<BooleanFunction>();
        }

        public bool IsValid()
        {
            this.IsDeadlock = this.FuncsA.Count == 0 || this.FuncsB.Count == 0;
            return this.FuncsA.Count > 0 || this.FuncsB.Count > 0;
        }

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
