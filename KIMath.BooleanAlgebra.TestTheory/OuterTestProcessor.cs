using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra.TestTheory
{
    public class OuterTestProcessor: CommonProcessor
    {
        public int Variables { get; private set; }

        public bool Result { get; private set; }

        public List<PostClassBooleanFunctions> PostClasses { get; private set; }

        public OuterTestProcessor(int variables, params PostClassBooleanFunctions[] list)
        {
            this.Variables = variables;
            this.PostClasses = list.ToList();
        }

        public OuterTestProcessor(int variables, IEnumerable<PostClassBooleanFunctions> postClasses)
        {
            this.Variables = variables;
            this.PostClasses = postClasses.ToList();
        }

        protected override void ProcessDeadlockTests()
        {
            this._deadlockTests = new List<BooleanFunctionTest>();
            List<BooleanFunction> allFunctions = this.PostClasses.SelectMany(x => x.Functions).ToList();
            List<bool[]> inputs = TestTheoryCommon.ExcludeInputs(allFunctions, this.Variables);
            List<OuterTestFunctionsPair> basePairs = new List<OuterTestFunctionsPair>();
            for (int i = 0; i < this.PostClasses.Count - 1; i++)
            {
                for (int j = i + 1; j < this.PostClasses.Count; j++)
                {
                    basePairs.Add(new OuterTestFunctionsPair(this.PostClasses[i].Functions, this.PostClasses[j].Functions));
                }
            }
            List<OuterTestTreeNode> currentNodes = new List<OuterTestTreeNode>();
            int iteration = 1;
            foreach (bool[] input in inputs)
            {
                OuterTestTreeNode node = new OuterTestTreeNode(basePairs, input);
                OuterTestTreeNode resNode = node.Process();
                if (node.IsDeadlock)
                {
                    this._deadlockTests.Add(node.GetTest());
                }
                else if (resNode != null)
                {
                    currentNodes.Add(resNode);
                }
            }
            while (currentNodes.Count > 0)
            {
                Console.Write("{0} ({1}) ", iteration, currentNodes.Count);
                List<OuterTestTreeNode> newNodes = new List<OuterTestTreeNode>();
                for (int i = 0; i < currentNodes.Count; i++)
                {
                    foreach (bool[] input in inputs)
                    {
                        OuterTestTreeNode childNode = currentNodes[i].CreateChildNode(input);
                        OuterTestTreeNode resultTreeNode = childNode.Process();
                        if (childNode.IsDeadlock)
                        {
                            this._deadlockTests.Add(childNode.GetTest());
                        }
                        else if (resultTreeNode != null)
                        {
                            newNodes.Add(resultTreeNode);
                        }
                    }
                }
                currentNodes = newNodes;
                iteration++;
            }
        }
    }
}
