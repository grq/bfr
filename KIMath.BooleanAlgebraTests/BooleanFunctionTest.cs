using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KIMath.BooleanAlgebra;

namespace KIMath.BooleanAlgebraTests
{
    [TestClass]
    public class BooleanFunctionTest
    {
        #region Constructor Tests

        /// <summary>
        /// Функция алгебры логики может быть задана в виде массива Bool, и значения функции не будут потеряны
        /// </summary>
        [TestMethod]
        public void ConstructFunctionByBoolArray()
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
        public void ConstructFunctionByString()
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
        public void ConstructFunctionByInteger()
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

        #endregion

        #region Post Properties Detection Tests

        /// <summary>
        /// Свойство "Самодвойственность" для функции может быть корректно определена
        /// </summary>
        [TestMethod]
        public void PropertySelfdualCalculate()
        {
            // Множество удовлетворяющих функций, проверенных "на бумаге"
            string[] positiveInputs = new string[] { 
                "10010110", "11110000", "00001111", "10110010", "11101000",
                "1000110101001110", "0010011100011011", "0110001010111001", "0001100101100111",
                "0111001010110001", "1000011010011110", "0000011100011111", "1010001100111010",
            };
            foreach (string input in positiveInputs)
            {
                BooleanFunction function = new BooleanFunction(input, (int)Math.Log(input.Length, 2));
                Assert.AreEqual(true, function.IsSelfDual);
                Assert.AreEqual(true, function.GetPostPropertyValue(PostProperty.SelfDual));
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
                Assert.AreEqual(false, function.GetPostPropertyValue(PostProperty.SelfDual));
            }
        }

        /// <summary>
        /// Свойство "Сохраняет 0" для функции может быть корректно определена
        /// </summary>
        [TestMethod]
        public void PropertyPreserving0Calculate()
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
                Assert.AreEqual(true, function.GetPostPropertyValue(PostProperty.PreservingNil));
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
                Assert.AreEqual(false, function.GetPostPropertyValue(PostProperty.PreservingNil));
            }
        }

        /// <summary>
        /// Свойство "Сохраняет 1" для функции может быть корректно определена
        /// </summary>
        [TestMethod]
        public void PropertyPreserving1Calculate()
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
                Assert.AreEqual(true, function.GetPostPropertyValue(PostProperty.PreservingOne));
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
                Assert.AreEqual(false, function.GetPostPropertyValue(PostProperty.PreservingOne));
            }
        }

        /// <summary>
        /// Свойство "Линейность" для функции может быть корректно определена
        /// </summary>
        [TestMethod]
        public void PropertyLinearCalculate()
        {
            /* Множество удовлетворяющих свойству функций, проверенных "на бумаге"  */
            /* Проверка произведена в файле "Documents/Функции для проверки определения свойства линейности.docx" */
            BooleanFunction[] positiveFunctions = new BooleanFunction[] { 
                new BooleanFunction("10100101", 3) { Mark = "1" },
                new BooleanFunction("00110011", 3) { Mark = "2" },
                new BooleanFunction("10010110", 3) { Mark = "3" },
                new BooleanFunction("11000011", 3) { Mark = "4" },
                new BooleanFunction("01100110", 3) { Mark = "5" },
                new BooleanFunction("10011001", 3) { Mark = "6" },
                new BooleanFunction("00111100", 3) { Mark = "7" },
                new BooleanFunction("01011010", 3) { Mark = "8" },
                new BooleanFunction("01100110", 3) { Mark = "9" },
                new BooleanFunction("01101001", 3) { Mark = "10" },
                new BooleanFunction("0000111111110000", 4) { Mark = "11" },
                new BooleanFunction("0011001111001100", 4) { Mark = "12" },
                new BooleanFunction("0011110000111100", 4) { Mark = "13" },
                new BooleanFunction("0011110011000011", 4) { Mark = "14" },
                new BooleanFunction("0101101001011010", 4) { Mark = "15" },
                new BooleanFunction("0101101010100101", 4) { Mark = "16" },
                new BooleanFunction("0110100101101001", 4) { Mark = "17" },
                new BooleanFunction("0110100110010110", 4) { Mark = "18" },
                new BooleanFunction("1010010110100101", 4) { Mark = "19" },
                new BooleanFunction("1100110000110011", 4) { Mark = "20" },
            };
            foreach (BooleanFunction function in positiveFunctions)
            {
                Assert.AreEqual(true, function.IsLinear);
                Assert.AreEqual(true, function.GetPostPropertyValue(PostProperty.Linear));
            }
            /* Множество НЕ удовлетворяющих свойству функций, проверенных "на бумаге"  */
            /* Проверка произведена в файле "Documents/Функции для проверки определения свойства линейности.docx" */
            BooleanFunction[] negativeFunctions = new BooleanFunction[] { 
                new BooleanFunction("10100100", 3) { Mark = "31" },
                new BooleanFunction("10111001", 3) { Mark = "32" },
                new BooleanFunction("00011010", 3) { Mark = "33" },
                new BooleanFunction("00000101", 3) { Mark = "34" },
                new BooleanFunction("11010001", 3) { Mark = "35" },
                new BooleanFunction("10101100", 3) { Mark = "36" },
                new BooleanFunction("11111100", 3) { Mark = "37" },
                new BooleanFunction("11001011", 3) { Mark = "38" },
                new BooleanFunction("00100010", 3) { Mark = "39" },
                new BooleanFunction("11011000", 3) { Mark = "40" },
                new BooleanFunction("1001101111001101", 4) { Mark = "41" },
                new BooleanFunction("0110111010000100", 4) { Mark = "42" },
                new BooleanFunction("0011111100100000", 4) { Mark = "43" },
                new BooleanFunction("0000000010000100", 4) { Mark = "44" },
                new BooleanFunction("0001101001011011", 4) { Mark = "45" },
                new BooleanFunction("0010110100101010", 4) { Mark = "46" },
                new BooleanFunction("1010110100000001", 4) { Mark = "47" },
                new BooleanFunction("0101010101000001", 4) { Mark = "48" },
                new BooleanFunction("0011001110001111", 4) { Mark = "49" },
                new BooleanFunction("1110001111111110", 4) { Mark = "50" },
            };
            foreach (BooleanFunction function in negativeFunctions)
            {
                Assert.AreEqual(false, function.IsLinear);
                Assert.AreEqual(false, function.GetPostPropertyValue(PostProperty.Linear));
            }
        }

        /// <summary>
        /// Свойство "Монотонность" для функции может быть корректно определена
        /// </summary>
        [TestMethod]
        public void PropertyMonotoneCalculate()
        {
            /* Множество удовлетворяющих свойству функций, проверенных "на бумаге"  */
            /* Проверка произведена в файле "Documents/Функции для проверки определения свойства монотонности.docx" */
            BooleanFunction[] positiveFunctions = new BooleanFunction[] { 
                new BooleanFunction("01010101", 3) { Mark = "1" },
                new BooleanFunction("00010111", 3) { Mark = "2" },
                new BooleanFunction("01110111", 3) { Mark = "3" },
                new BooleanFunction("00000101", 3) { Mark = "4" },
                new BooleanFunction("01010111", 3) { Mark = "5" },
                new BooleanFunction("01011111", 3) { Mark = "6" },
                new BooleanFunction("00010001", 3) { Mark = "7" },
                new BooleanFunction("00111111", 3) { Mark = "8" },
                new BooleanFunction("00000000", 3) { Mark = "9" },
                new BooleanFunction("11111111", 3) { Mark = "10" },
                new BooleanFunction("0101111101011111", 4) { Mark = "11" },
                new BooleanFunction("0001010101010101", 4) { Mark = "12" },
                new BooleanFunction("0000001100000011", 4) { Mark = "13" },
                new BooleanFunction("0001011111111111", 4) { Mark = "14" },
                new BooleanFunction("0000111111111111", 4) { Mark = "15" },
                new BooleanFunction("0000001101010111", 4) { Mark = "16" },
                new BooleanFunction("0000011100001111", 4) { Mark = "17" },
                new BooleanFunction("0000000100000011", 4) { Mark = "18" },
                new BooleanFunction("0111011101110111", 4) { Mark = "19" },
                new BooleanFunction("0101011101010111", 4) { Mark = "20" },
            };
            foreach (BooleanFunction function in positiveFunctions)
            {
                Assert.AreEqual(true, function.IsMonotone);
                Assert.AreEqual(true, function.GetPostPropertyValue(PostProperty.Monotone));
            }

            /* Множество НЕ удовлетворяющих свойству функций, проверенных "на бумаге" */
            /* Проверка произведена в файле "Documents/Функции для проверки определения свойства монотонности.docx" */
            BooleanFunction[] negativeFunctions = new BooleanFunction[] { 
                new BooleanFunction("10010010", 3) { Mark = "31" },
                new BooleanFunction("00110001", 3) { Mark = "32" },
                new BooleanFunction("00110010", 3) { Mark = "33" },
                new BooleanFunction("11011111", 3) { Mark = "34" },
                new BooleanFunction("01001101", 3) { Mark = "35" },
                new BooleanFunction("01100101", 3) { Mark = "36" },
                new BooleanFunction("01000010", 3) { Mark = "37" },
                new BooleanFunction("01110000", 3) { Mark = "38" },
                new BooleanFunction("00100010", 3) { Mark = "39" },
                new BooleanFunction("10111101", 3) { Mark = "40" },
                new BooleanFunction("0001011101010101", 4) { Mark = "41" },
                new BooleanFunction("1111011111111111", 4) { Mark = "42" },
                new BooleanFunction("0000000000000100", 4) { Mark = "43" },
                new BooleanFunction("0011001000010011", 4) { Mark = "44" },
                new BooleanFunction("0000001001001111", 4) { Mark = "45" },
                new BooleanFunction("0001101011011011", 4) { Mark = "46" },
                new BooleanFunction("0000001000000000", 4) { Mark = "47" },
                new BooleanFunction("1110010010011010", 4) { Mark = "48" },
                new BooleanFunction("1001011001101000", 4) { Mark = "49" },
                new BooleanFunction("1110110011011010", 4) { Mark = "50" },
            };
            foreach (BooleanFunction function in negativeFunctions)
            {
                Assert.AreEqual(false, function.IsMonotone);
                Assert.AreEqual(false, function.GetPostPropertyValue(PostProperty.Monotone));
            }
        }

        #endregion
    }
}

