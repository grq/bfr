using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KIMath.BooleanAlgebra;
using System.Collections.Generic;

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
            List<string> result = BooleanAlgebraHelper.GetIntersection<string>(arrays);
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
    }
}
