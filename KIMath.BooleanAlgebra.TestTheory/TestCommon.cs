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
    internal abstract class TestCommon
    {
        public List<TestCommon> ChildTest { get; set; }

        public List<bool[]> History { get; set; }

        public List<bool[]> Inputs { get; set; }

        public TestCommon()
        {
            this.ChildTest = new List<TestCommon>();
        }

        public List<TestCommon> GetCompleteTests()
        {
            List<TestCommon> result = new List<TestCommon>();
            foreach (InnerTest tst in this.ChildTest)
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

        public List<TestCommon> GetCompleteTestsString()
        {
            List<TestCommon> result = new List<TestCommon>();
            foreach (TestCommon tst in this.ChildTest)
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
