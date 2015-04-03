using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra.TestTheory
{
    /// <summary>
    /// Процессор для вычисления тестов для распознавания (отделения) функций между множествами функций
    /// </summary>
    public class OuterTestProcessor: CommonProcessor
    {
        /// <summary>
        /// Число переменных
        /// </summary>
        public int Variables { get; private set; }

        /// <summary>
        /// Классы Поста, между которыми происходит распознавание (отделение) функций
        /// </summary>
        public List<ClassBooleanFunctions> PostClasses { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="variables">Число переменных</param>
        /// <param name="list">Класс функций алгебры логики</param>
        public OuterTestProcessor(int variables, params ClassBooleanFunctions[] list)
        {
            this.Variables = variables;
            this.PostClasses = list.ToList();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="variables">Число переменных</param>
        /// <param name="postClasses">Классы функций алгебры логики</param>
        ///<param name="useConsole">Использовать консоль для вычисления</param>
        public OuterTestProcessor(int variables, IEnumerable<ClassBooleanFunctions> postClasses, bool useConsole = false)
        {
            this.Variables = variables;
            this.PostClasses = postClasses.ToList();
            this._useConsole = useConsole;
        }

        /// <summary>
        /// Вычислить тупиковые тесты
        /// </summary>
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
                if (node.IsCompleted)
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
                this.ConsoleWrite(string.Format("{0} ({1}) ", iteration, currentNodes.Count));
                List<OuterTestTreeNode> newNodes = new List<OuterTestTreeNode>();
                for (int i = 0; i < currentNodes.Count; i++)
                {
                    foreach (bool[] input in inputs)
                    {
                        OuterTestTreeNode childNode = currentNodes[i].CreateChildNode(input);
                        OuterTestTreeNode resultTreeNode = childNode.Process();
                        if (childNode.IsCompleted)
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
            this.ConsoleWriteLine();
        }
    }
}
