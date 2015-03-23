using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    public abstract class MinimalTestCommon
    {
        public List<MinimalTestCommon> ChildTest { get; set; }

        public List<bool[]> History { get; set; }

        public List<bool[]> Inputs { get; set; }

        public MinimalTestCommon()
        {
            this.ChildTest = new List<MinimalTestCommon>();
        }

        public List<MinimalTestCommon> GetCompleteTests()
        {
            List<MinimalTestCommon> result = new List<MinimalTestCommon>();
            foreach (MinimalTest tst in this.ChildTest)
            {
                if (tst.ChildTest.Count == 0)
                {
                    result.Add(tst);
                }
                else
                {
                    result.AddRange(tst.GetCompleteTests());
                }
            }
            return result;
        }

        public List<MinimalTestCommon> GetCompleteTestsString()
        {
            List<MinimalTestCommon> result = new List<MinimalTestCommon>();
            foreach (MinimalTestCommon tst in this.ChildTest)
            {
                if (tst.ChildTest.Count == 0 && tst.History != this.Inputs)
                {
                    result.Add(tst);
                }
                else
                {
                    result.AddRange(tst.GetCompleteTests());
                }
            }
            return result;
        }
    }
}
