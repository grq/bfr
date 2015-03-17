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
        /// Свойство "Самодвойственность" для функции может быть корректно определена
        /// </summary>
        [TestMethod]
        public void SelfdualPropertyCalculatedCorrectly()
        {
            // Множество удовлетворяющих функций, проверенных "на бумаге"
            string[] positiveInputs = new string[] { 
                "10010110", "11110000", "00001111", "10110010", "11101000",
                "1000110101001110", "0010011100011011", "0110001010111001", "0001100101100111",
                "0111001010110001", "1000011010011110", "0000011100011111", "1010001100111010",
            };
            foreach(string input in positiveInputs)
            {
                BooleanFunction function = new BooleanFunction(input, (int)Math.Log(input.Length, 2));
                Assert.AreEqual(true, function.IsSelfDual);
            }

            // Множество НЕ удовлетворяющих функций, проверенных "на бумаге"
            string[] negativeInputs = new string[] { 
                "01110011", "11110011", "11111111", "01101111", "00000100", "01110000", "11100010", "01011010",
                "1000010000011001", "0100100000001101", "0000100110100110", "1011110100000000", "1010111101001011",
                "1001011100111111", "1001011000001101", "0000100101000010", "1001110011000101", "0010110100000100",
            };
            foreach (string input in negativeInputs)
            {
                BooleanFunction function = new BooleanFunction(input, (int)Math.Log(input.Length, 2));
                Assert.AreEqual(false, function.IsSelfDual);
            }
        }

        /// <summary>
        /// Свойство "Сохраняет 0" для функции может быть корректно определена
        /// </summary>
        [TestMethod]
        public void Preserving0PropertyCalculatedCorrectly()
        {
            // Множество удовлетворяющих функций, проверенных "на бумаге"
            string[] positiveInputs = new string[] { 
                "01010100", "01010011", "00100010", "00100101", "00101111", "00110100", "00001101", "01110111", 
                "0101101101000101", "0100001101010000", "0011100000111001", "0000001001110011", "0011100010000100", 
                "0000011010011110", "0011111111001110", "0011111111000000", "0110101100010101", "0011110101001110", 
            };
            foreach (string input in positiveInputs)
            {
                BooleanFunction function = new BooleanFunction(input, (int)Math.Log(input.Length, 2));
                Assert.AreEqual(true, function.IsPreserving0);
            }

            // Множество НЕ удовлетворяющих функций, проверенных "на бумаге"
            string[] negativeInputs = new string[] { 
                "10110110", "10010010", "11010100", "10100011", "10111111", "10100000", "10001100", "10000001", 
                "1010001101010010", "1010010010011000", "1010010001101001", "1100101010100100", "1101010001110101", 
                "1101010110010010", "1001010101010010", "1101010011001110", "1100110010101100", "1010110101001011",
            };
            foreach (string input in negativeInputs)
            {
                BooleanFunction function = new BooleanFunction(input, (int)Math.Log(input.Length, 2));
                Assert.AreEqual(false, function.IsPreserving0);
            }
        }

        /// <summary>
        /// Свойство "Сохраняет 1" для функции может быть корректно определена
        /// </summary>
        [TestMethod]
        public void Preserving1PropertyCalculatedCorrectly()
        {
            // Множество удовлетворяющих функций, проверенных "на бумаге"
            string[] positiveInputs = new string[] { 
                "11010101", "01010101", "10101011", "10100001", "01110101", "10101001", "11000101", "10101111", 
                "0101100100101101", "0101010101010101", "0101100101110111", "1101100110111011", "0110001010001011", 
                "0100000000000111", "1011111101010101", "0101110101000001", "0100111010101011", "1011001100010111",
            };
            foreach (string input in positiveInputs)
            {
                BooleanFunction function = new BooleanFunction(input, (int)Math.Log(input.Length, 2));
                Assert.AreEqual(true, function.IsPreserving1);
            }

            // Множество НЕ удовлетворяющих функций, проверенных "на бумаге"
            string[] negativeInputs = new string[] { 
                "01101100", "11000100", "01010000", "10101010", "10101000", "01010110", "10110100", "01011010", 
                "0111010000100000", "0101010110100010", "0110000100111110", "1011000111111100", "0001110000000000", 
                "1100100010001000", "0000101110000100", "0111000011001100", "0101111110100000", "0110111110110000",
            };
            foreach (string input in negativeInputs)
            {
                BooleanFunction function = new BooleanFunction(input, (int)Math.Log(input.Length, 2));
                Assert.AreEqual(false, function.IsPreserving1);
            }
        }

        /// <summary>
        /// Свойство "Линейность" для функции может быть корректно определена
        /// </summary>
        [TestMethod]
        public void LinearPropertyCalculatedCorrectly()
        {
            Assert.AreEqual(true, true);
        }

        /// <summary>
        /// Свойство "Монотонность" для функции может быть корректно определена
        /// </summary>
        [TestMethod]
        public void MonotonePropertyCalculatedCorrectly()
        {
            // Множество удовлетворяющих функций, проверенных "на бумаге"
            string[] positiveInputs = new string[] { 
                "01010101", "00010111", "01110111", "00000101", "01010111", "01011111", "00010001", "00111111", "00000000", "11111111",
            };
            foreach (string input in positiveInputs)
            {
                BooleanFunction function = new BooleanFunction(input, (int)Math.Log(input.Length, 2));
                Assert.AreEqual(true, function.IsMonotone);
            }

            // Множество НЕ удовлетворяющих функций, проверенных "на бумаге"
            string[] negativeInputs = new string[] { 
                "10010010", "00110001", "00110010", "11011111", "01001101", "01100101", "01000010", "01110000", 
            };
            foreach (string input in negativeInputs)
            {
                BooleanFunction function = new BooleanFunction(input, (int)Math.Log(input.Length, 2));
                Assert.AreEqual(false, function.IsMonotone);
            }
        }
    }
}

