using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KIMath.BooleanAlgebra;
using System.Collections.Generic;
using System.Linq;

namespace KIMath.BooleanAlgebraTests
{
    [TestClass]
    public class HelperTest
    {
        [TestMethod]
        public void HelperIntersection()
        {
            /// "A", "B", "C" - перечение множеств
            string[] intersection = new string[] { "A", "B", "C" };
            string[] array1 = new string[] { "A", "B", "C", "Q", "W", "E", "R", "T", "Y", "U" };
            string[] array2 = new string[] { "A", "B", "C", "I", "O", "P", "S", "D", "F", "G" };
            string[] array3 = new string[] { "A", "B", "C", "H", "J", "K", "L", "Z", "X", "V" };
            List<string[]> arrays = new List<string[]>() { array1, array2, array3 };
            List<string> result = SetTheoryHelper.GetIntersection<string>(arrays).ToList();
            Assert.AreEqual(true, BooleanAlgebraHelper.CollectionsAreEqualOrdered(result, intersection));
        }

        [TestMethod]
        public void HelperCollectionsAreEqualOrdered()
        {
            string[] array1 = new string[] { "A", "B", "C", "Q", "W", "E", "R", "T", "Y", "U" };
            string[] array2 = new string[] { "A", "B", "C", "Q", "W", "E", "R", "T", "Y", "U" };
            Assert.AreEqual(true, BooleanAlgebraHelper.CollectionsAreEqualOrdered(array1, array2));
        }

        [TestMethod]
        public void HelperCollectionsAreEqualNotOrdered()
        {
            string[] arrayEqual1 = new string[] { "A", "B", "C", "Q" };
            string[] arrayEqual2 = new string[] { "Q", "C", "B", "A" };
            Assert.AreEqual(true, BooleanAlgebraHelper.CollectionsAreEqualNotOrdered(arrayEqual1, arrayEqual2));

            string[] arrayEqual3 = new string[] { "A", "A", "B", "B" };
            string[] arrayEqual4 = new string[] { "B", "A", "B", "A" };
            Assert.AreEqual(true, BooleanAlgebraHelper.CollectionsAreEqualNotOrdered(arrayEqual3, arrayEqual4));

            string[] arrayNotEqual1 = new string[] { "A", "A", "A", "B" };
            string[] arrayNotEqual2 = new string[] { "B", "B", "B", "A" };
            Assert.AreEqual(false, BooleanAlgebraHelper.CollectionsAreEqualNotOrdered(arrayNotEqual1, arrayNotEqual2));
        }

        [TestMethod]
        public void HelperInclusion()
        {
            string[] array_1_1 = new string[] { "A", "B", "C", "Q", "W", "E", "R", "T", "Y", "U" };
            string[] array_1_2 = new string[] { "A", "B", "C" };
            bool result1 = SetTheoryHelper.IsInclusion(array_1_1, array_1_2);
            Assert.AreEqual(true, result1);

            string[] array_2_1 = new string[] { "A", "B", "C", "Q", "W", "E", "R", "T", "Y", "U" };
            string[] array_2_2 = new string[] { "A", "B", "M" };
            bool result2 = SetTheoryHelper.IsInclusion(array_2_1, array_2_2);
            Assert.AreEqual(false, result2);

            string[] array_3_1 = new string[] { "A", "A", "A", "B" };
            string[] array_3_2 = new string[] { "A", "A", "B" };
            bool result3 = SetTheoryHelper.IsInclusion(array_3_1, array_3_2);
            Assert.AreEqual(true, result3);o

            string[] array_4_1 = new string[] { "A", "A", "A", "B" };
            string[] array_4_2 = new string[] { "A", "B", "B" };
            bool result4 = SetTheoryHelper.IsInclusion(array_4_1, array_4_2);
            Assert.AreEqual(false, result4);
        }

        [TestMethod]
        public void HelperUnion()
        {
            string[] array_1_1 = new string[] { "A", "B", "C" };
            string[] array_1_2 = new string[] { "D", "F", "E" };
            List<string[]> arrays_1 = new List<string[]>() { array_1_1, array_1_2 };
            string[] expected_result_1 = new string[] { "D", "F", "E", "A", "B", "C" };
            IEnumerable<string> actual_result_1 = SetTheoryHelper.GetUnion<string>(arrays_1);
            Assert.AreEqual(true, BooleanAlgebraHelper.CollectionsAreEqualNotOrdered(expected_result_1, actual_result_1));
        }
    }
}
