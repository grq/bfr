using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KIMath.BooleanAlgebra;

namespace KIMath.BooleanAlgebraTests
{
    [TestClass]
    public class BooleanFunctionTest
    {
        /// <summary>
        /// Функция алгебры логики может быть задана в виде массива Bool, и значения функции не будут потеряны
        /// </summary>
        [TestMethod]
        public void CanBeConstructedByBoolArray()
        {
            bool[] input = new bool[8] { false, true, true, false, false, true, true, true };
            BooleanFunction function = new BooleanFunction(input, 3);
            // Сравниваем значение с исходными
            Assert.AreEqual(false, function.Value[0]);
            Assert.AreEqual(true, function.Value[1]);
            Assert.AreEqual(true, function.Value[2]);
            Assert.AreEqual(false, function.Value[3]);
            Assert.AreEqual(false, function.Value[4]);
            Assert.AreEqual(true, function.Value[5]);
            Assert.AreEqual(true, function.Value[6]);
            Assert.AreEqual(true, function.Value[7]);
            // Сравниваем значение с противоположными исходному
            Assert.AreNotEqual(true, function.Value[0]);
            Assert.AreNotEqual(false, function.Value[1]);
            Assert.AreNotEqual(false, function.Value[2]);
            Assert.AreNotEqual(true, function.Value[3]);
            Assert.AreNotEqual(true, function.Value[4]);
            Assert.AreNotEqual(false, function.Value[5]);
            Assert.AreNotEqual(false, function.Value[6]);
            Assert.AreNotEqual(false, function.Value[7]);
        }

        /// <summary>
        /// Функция алгебры логики может быть задана в виде строки, и значения функции не будут потеряны
        /// </summary>
        [TestMethod]
        public void CanBeConstructedByString()
        {
            string input = "01001110";
            BooleanFunction function = new BooleanFunction(input, 3);
            // Сравниваем значение с исходными
            Assert.AreEqual(false, function.Value[0]);
            Assert.AreEqual(true, function.Value[1]);
            Assert.AreEqual(false, function.Value[2]);
            Assert.AreEqual(false, function.Value[3]);
            Assert.AreEqual(true, function.Value[4]);
            Assert.AreEqual(true, function.Value[5]);
            Assert.AreEqual(true, function.Value[6]);
            Assert.AreEqual(false, function.Value[7]);
            // Сравниваем значение с противоположными исходному
            Assert.AreNotEqual(true, function.Value[0]);
            Assert.AreNotEqual(false, function.Value[1]);
            Assert.AreNotEqual(true, function.Value[2]);
            Assert.AreNotEqual(true, function.Value[3]);
            Assert.AreNotEqual(false, function.Value[4]);
            Assert.AreNotEqual(false, function.Value[5]);
            Assert.AreNotEqual(false, function.Value[6]);
            Assert.AreNotEqual(true, function.Value[7]);
        }

        /// <summary>
        /// Функция алгебры логики может быть задана в виде числа, и значения функции не будут потеряны
        /// </summary>
        [TestMethod]
        public void CanBeConstructedByInteger()
        {
            int input = 45; // В двоичной системе 00101101
            BooleanFunction function = new BooleanFunction(input, 3);
            // Сравниваем значение с исходными
            Assert.AreEqual(false, function.Value[0]);
            Assert.AreEqual(false, function.Value[1]);
            Assert.AreEqual(true, function.Value[2]);
            Assert.AreEqual(false, function.Value[3]);
            Assert.AreEqual(true, function.Value[4]);
            Assert.AreEqual(true, function.Value[5]);
            Assert.AreEqual(false, function.Value[6]);
            Assert.AreEqual(true, function.Value[7]);
            // Сравниваем значение с противоположными исходному
            Assert.AreNotEqual(true, function.Value[0]);
            Assert.AreNotEqual(true, function.Value[1]);
            Assert.AreNotEqual(false, function.Value[2]);
            Assert.AreNotEqual(true, function.Value[3]);
            Assert.AreNotEqual(false, function.Value[4]);
            Assert.AreNotEqual(false, function.Value[5]);
            Assert.AreNotEqual(true, function.Value[6]);
            Assert.AreNotEqual(false, function.Value[7]);
        }

        /// <summary>
        /// Свойство самодвойственности для функции может быть корректно определено
        /// </summary>
        [TestMethod]
        public void SelfdualPropertyCalculatedCorrectly()
        {
            // Множество самодвойственных функций, проверенных "на бумаге"
            string[] selfDualInputs = new string[] { 
                "10010110", "11110000", "00001111", "10110010", "11101000",
                "1000110101001110", "0010011100011011", "0110001010111001", "0001100101100111",
                "0111001010110001", "1000011010011110", "0000011100011111", "1010001100111010",
            };
            foreach(string input in selfDualInputs)
            {
                BooleanFunction function = new BooleanFunction(input, (int)Math.Log(input.Length, 2));
                Assert.AreEqual(true, function.IsSelfDual);
            }

            // Множество НЕ самодвойственных функций, проверенных "на бумаге"
            string[] notSelfDualInputs = new string[] { 
                "01110011", "11110011", "11111111", "01101111", "00000100", "01110000", "11100010", "01011010",
                "1000010000011001", "0100100000001101", "0000100110100110", "1011110100000000", "1010111101001011",
                "1001011100111111", "1001011000001101", "0000100101000010", "1001110011000101", "0010110100000100",
            };
            foreach (string input in notSelfDualInputs)
            {
                BooleanFunction function = new BooleanFunction(input, (int)Math.Log(input.Length, 2));
                Assert.AreEqual(false, function.IsSelfDual);
            }
        }
    }
}
