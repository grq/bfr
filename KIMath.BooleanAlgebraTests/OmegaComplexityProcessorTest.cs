using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KIMath.BooleanAlgebra;
using System.Collections.Generic;

namespace KIMath.BooleanAlgebraTests
{
    [TestClass]
    public class OmegaComplexityProcessorTest
    {
        OmegaComplexityProcessor ocp;

        /// <summary>
        /// Пример взят из монографии Твердохлебова В. А. "Геометрические образы законов функционирования автоматов", издательство "Научная книга", Саратов, 2008, страница 40
        /// </summary>
        [TestInitialize]
        public void InitOcp()
        {
            string sequence = "31415926535897932384626433832795028841971693993751";
            this.ocp = new OmegaComplexityProcessor(sequence);
        }

        /// <summary>
        /// Определение параметра Omega0 для последовательности элементов
        /// </summary>
        [TestMethod]
        public void Omega0Calculation()
        {
            Assert.AreEqual(3, this.ocp.Omega0);
        }

        /// <summary>
        /// Определение параметров Omega1 для последовательности элементов
        /// </summary>
        [TestMethod]
        public void Omega1Calculation()
        {
            int[] omega1 = this.ocp.Omega1.ToArray();
            Assert.AreEqual(3, omega1.Length);
            CollectionAssert.AreEqual(new int[] { 4, 23, 50 }, omega1);
        }

        /// <summary>
        /// Определение абсолютного параметра Omega1 для последовательности элементов
        /// </summary>
        [TestMethod]
        public void Omega1CalculationTotal()
        {
            Assert.AreEqual(200, this.ocp.AbsoluteOmega1);
        }

        /// <summary>
        /// Определение параметров Omega2 для последовательности элементов
        /// </summary>
        [TestMethod]
        public void Omega2Calculation()
        {
            int[] omega2 = this.ocp.Omega2.ToArray();
            Assert.AreEqual(3, omega2.Length);
            CollectionAssert.AreEqual(new int[] { 14, 3, 1 }, omega2);
        }

        /// <summary>
        /// Определение абсолютного параметра Omega2 для последовательности элементов
        /// </summary>
        [TestMethod]
        public void Omega2CalculationTotal()
        {
            Assert.AreEqual(23, this.ocp.AbsoluteOmega2);
        }

        /// <summary>
        /// Определение параметров Omega3 для последовательности элементов
        /// </summary>
        [TestMethod]
        public void Omega3Calculation()
        {
            IEnumerable<int>[] omega3 = this.ocp.Omega3.ToArray();
            Assert.AreEqual(3, omega3.Length);
            CollectionAssert.AreEqual(new int[] { 3, 5, 2, 4, 3, 5, 3, 2, 6, 2, 5, 4, 1, 4 }, omega3[0].ToArray());
            CollectionAssert.AreEqual(new int[] { 21, 24, 3 }, omega3[1].ToArray());
            CollectionAssert.AreEqual(new int[] { 47 }, omega3[2].ToArray());
        }

        /// <summary>
        /// Определение абсолютного параметра Omega3 для последовательности элементов
        /// </summary>
        [TestMethod]
        public void Omega3CalculationTotal()
        {
            Assert.AreEqual(660, this.ocp.AbsoluteOmega3);
        }
    }
}
