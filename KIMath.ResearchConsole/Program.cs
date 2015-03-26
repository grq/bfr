using KIMath.BooleanAlgebra;
using KIMath.BooleanAlgebra.TestTheory; //todo remove
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.ResearchConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MinimalTestsResearch.ProcessMinimalOuterTests(3, 8);
            //List<PostClassBooleanFunctions> clss = new List<PostClassBooleanFunctions>();
            //clss.Add(ProcessorClassBooleanFunctions.GetPostClass("11101", 3));
            //clss.Add(ProcessorClassBooleanFunctions.GetPostClass("11110", 3));
            //clss.Add(ProcessorClassBooleanFunctions.GetPostClass("11111", 3));
            //MinimalTestsResearch.ProcessMinimalOuterTestsForSelected(3, clss);
            Console.WriteLine("DONE");
            Console.ReadKey();
        }

        static void NewTest()
        {
            int vars = 3;
            List<PostClassBooleanFunctions> postClasses = ProcessorClassBooleanFunctions.GetPostClasses(vars).ToList();
            List<BooleanFunction> F1 = new List<BooleanFunction>() { new BooleanFunction("01111111", vars) };
            List<BooleanFunction> F2 = new List<BooleanFunction>() { new BooleanFunction("00111110", vars) };
            List<BooleanFunction> F3 = new List<BooleanFunction>() { new BooleanFunction("10111110", vars) };
            List<bool[]> allInputs = BooleanAlgebraHelper.GetAllInputs(vars);

            List<FunctionsTestPair> basePairs = new List<FunctionsTestPair>();
            basePairs.Add(new FunctionsTestPair() { FuncsA = F1, FuncsB = F2 });
            basePairs.Add(new FunctionsTestPair() { FuncsA = F1, FuncsB = F3 });
            basePairs.Add(new FunctionsTestPair() { FuncsA = F2, FuncsB = F3 });

            List<TreeNode> resultNodes = new List<TreeNode>();

            List<TreeNode> nodes = new List<TreeNode>();
            List<TreeNode> completed = new List<TreeNode>();
            foreach (bool[] input in allInputs)
            {
                TreeNode node = new TreeNode(basePairs, input);
                TreeNode resNode = node.Process();
                if (node.Completed)
                {
                    completed.Add(node);
                }
                else if (node.ResultPairs != null)
                {
                    nodes.Add(resNode);
                }
            }
            while(nodes.Count > 0)
            {
                List<TreeNode> newNodes = new List<TreeNode>();
                foreach(TreeNode node in nodes)
                {
                    foreach (bool[] input in allInputs)
                    {
                        TreeNode childNode = node.CreateChildNode(input);
                        TreeNode resultTreeNode = childNode.Process();
                        if (childNode.Completed)
                        {
                            completed.Add(childNode);
                        }
                        else if (resultTreeNode != null)
                        {
                            newNodes.Add(resultTreeNode);
                        }
                    }
                }
                nodes = newNodes;
            }
            var res = completed.Select(x => x.HistoryString).Distinct();
        }
    }
}
