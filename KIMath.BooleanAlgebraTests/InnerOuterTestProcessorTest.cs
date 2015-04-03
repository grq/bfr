using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using KIMath.BooleanAlgebra;
using KIMath.BooleanAlgebra.TestTheory;

namespace KIMath.BooleanAlgebraTests
{
    [TestClass]
    public class InnerOuterTestProcessorTest
    {
        private List<int> GetOuterTestsHash(int variables, int capacity)
        {
            List<int> hash = new List<int>();
            List<PostClassBooleanFunctions> classes = ProcessorClassBooleanFunctions.GetPostClasses(3).ToList();
            List<List<PostClassBooleanFunctions>> combinations = BooleanAlgebraHelper.GetAllCombinations<PostClassBooleanFunctions>(classes, capacity);
            foreach (List<PostClassBooleanFunctions> combination in combinations)
            {
                OuterTestProcessor otp = new OuterTestProcessor(variables, combination);
                hash.Add(otp.MinimalTestLength);
                hash.Add(otp.MinimalTests.Count);
            }
            return hash;
        }

        [TestMethod]
        public void TestOuterHashIsTheSameAsBefore()
        {
            int[] oldHashForThreeVariables = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 2, 6, 1, 6, 1, 1, 2, 1, 2, 1, 2, 1, 2, 
                6, 1, 6, 1, 1, 2, 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 12, 1, 2, 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 2, 1, 8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 4, 1, 1, 6, 1, 6, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 1, 2, 12, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 4, 1, 1, 
                6, 1, 1, 2, 1, 2, 6, 1, 3, 2, 6, 1, 6, 1, 1, 2, 1, 2, 4, 9, 6, 1, 2, 9, 6, 1, 3, 8, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 
                1, 2, 2, 12, 3, 8, 3, 8, 1, 6, 3, 8, 2, 12 };
            List<int> newHashForThreeVariables = this.GetOuterTestsHash(3, 2);

            bool resultForThreeVariables = BooleanAlgebraHelper.CollectionsAreEqualOrdered<int>(newHashForThreeVariables, oldHashForThreeVariables);
            Assert.AreEqual(true, resultForThreeVariables);

            int[] oldHashForFourVariables = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 2, 14, 1, 14, 1, 1, 2, 1, 2, 1, 2, 1, 2, 14, 
                1, 14, 1, 1, 2, 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 224, 1, 2, 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 2, 1, 16, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 14, 1, 14, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 3, 224, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 14, 
                1, 1, 2, 1, 2, 14, 1, 8, 1, 14, 1, 14, 1, 1, 2, 1, 2, 8, 1, 14, 1, 3, 24, 8, 1, 7, 128, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 
                1, 2, 4, 16, 7, 128, 7, 128, 2, 24, 4, 16, 3, 224 };

            int[] newHashForFourVariables = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 2, 14, 1, 14, 1, 1, 2, 1, 2, 1, 2, 1, 2, 14, 
                1, 14, 1, 1, 2, 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 224, 1, 2, 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 2, 1, 16, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 14, 1, 14, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 3, 224, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 14, 
                1, 1, 2, 1, 2, 14, 1, 8, 1, 14, 1, 14, 1, 1, 2, 1, 2, 8, 1, 14, 1, 3, 24, 8, 1, 7, 128, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 
                1, 2, 4, 16, 7, 128, 7, 128, 2, 24, 4, 16, 3, 224 };

            bool resultForFourVariables = BooleanAlgebraHelper.CollectionsAreEqualOrdered<int>(newHashForFourVariables, oldHashForFourVariables);
            Assert.AreEqual(true, resultForThreeVariables);
        }

        [TestMethod]
        public void TestInnerEqualToOuter()
        {
            int variables = 3;
            PostClassBooleanFunctions[] postClasses = ProcessorClassBooleanFunctions.GetPostClasses(variables).ToArray();
            foreach (PostClassBooleanFunctions postClass in postClasses)
            {
                InnerTestProcessor innerProcessor = new InnerTestProcessor(postClass, variables);
                List<ClassBooleanFunctions> functionsAsManyClasses = new List<ClassBooleanFunctions>();
                foreach(BooleanFunction function in postClass.Functions)
                {
                    functionsAsManyClasses.Add(new ClassBooleanFunctions(function));
                }
                OuterTestProcessor outerProcessor = new OuterTestProcessor(variables, functionsAsManyClasses);
                Assert.AreEqual(innerProcessor.MinimalTestLength, outerProcessor.MinimalTestLength);
                Assert.AreEqual(innerProcessor.MinimalTests.Count, outerProcessor.MinimalTests.Count);
            }
        }
    }
}
