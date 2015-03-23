using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    /// <summary>
    /// Тупиковый тест - Базовый класс
    /// </summary>
    public abstract class DeadlockTestCommon //todo internal
    {
        public List<DeadlockTestCommon> ChildTest { get; set; }

        public List<bool[]> History { get; set; }

        public List<bool[]> Inputs { get; set; }

        public DeadlockTestCommon()
        {
            this.ChildTest = new List<DeadlockTestCommon>();
        }

        public List<DeadlockTestCommon> GetCompleteTests()
        {
            List<DeadlockTestCommon> result = new List<DeadlockTestCommon>();
            foreach (DeadlockTestInner tst in this.ChildTest)
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

        public List<DeadlockTestCommon> GetCompleteTestsString()
        {
            List<DeadlockTestCommon> result = new List<DeadlockTestCommon>();
            foreach (DeadlockTestCommon tst in this.ChildTest)
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
