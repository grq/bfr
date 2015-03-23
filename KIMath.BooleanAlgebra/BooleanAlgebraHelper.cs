using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    public class BooleanAlgebraHelper
    {
        #region Boolean Operations

        static public bool Implication(params bool[] list)
        {
            if(list.Length < 2)
            {
                throw new ArgumentOutOfRangeException("Params", "At least two parameters should be provided.");
            }
            bool result = list[0];
            for (int i = 1; i < list.Length; i++)
            {
                result = !result || list[i];
            }
            return result;
        }

        static public bool Xor(params bool[] list)
        {
            if (list.Length < 2)
            {
                throw new ArgumentOutOfRangeException("Params", "At least two parameters should be provided.");
            }
            bool result = list[0];
            for (int i = 1; i < list.Length; i++)
            {
                result = result != list[i];
            }
            return result;
        }

        #endregion

        #region Numerical System Conversions

        #region Dec To Binary

        /// <summary>
        /// Перевод десятичного числа в двоичную систему счисления
        /// </summary>
        /// <param name="value">Десятичное число</param>
        /// <returns>Двоичное число</returns>
        static public IEnumerable<bool> DecToBinaryArray(long value)
        {
            List<bool> result = new List<bool>();
            while (value > 1)
            {
                result.Insert(0, ((value % 2) == 1));
                value = value / 2;
            }
            result.Insert(0, (value == 1));
            return result.ToArray();
        }

        /// <summary>
        /// Перевод из десятичной в двоичную систему счисления
        /// </summary>
        /// <param name="value">Число в десятичной системе счисления</param>
        /// <returns>Число в двоичной системе счисления, в формате строки, где 1 - True, 0 -False</returns>
        static public string DecToBinaryString(long value)
        {
            return BinaryToString(DecToBinaryArray(value));
        }

        /// <summary>
        /// Перевод из десятичной в двоичную систему счисления, с заданной длиной массива
        /// </summary>
        /// <param name="value">Число в десятичной системе счисления</param>
        /// <param name="length">Длина массива</param>
        /// <returns>Число в двоичной системе счисления, в формате строки, где 1 - True, 0 -False</returns>
        static public string DecToBinaryString(long value, int length)
        {
            return GetCompletedStringFormByLength(DecToBinaryString(value), length);
        }

        #endregion

        #region Binary To Dec

        /// <summary>
        /// Перевод из двоичной в десятичную систему счисления
        /// </summary>
        /// <param name="value">Число в двоичной системе счисления в формате массива bool</param>
        /// <returns>Число в десятичной системе счисления</returns>
        static public long BinaryToDec(IEnumerable<bool> value)
        {
            long bin = 0;
            bool[] arr = value.ToArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[arr.Length - i - 1]) bin += Convert.ToInt32(Math.Pow(2, i));
            }
            return bin;
        }

        /// <summary>
        /// Перевод из двоичной в десятичную систему счисления
        /// </summary>
        /// <param name="value">Число в двоичной системе счисления в формате строки</param>
        /// <returns>Число в десятичной системе счисления</returns>
        static public long BinaryToDec(string value)
        {
            return BinaryToDec(StringToBoolArray(value));
        }

        #endregion

        #endregion

        #region Utility Methods for Binaries

        static public string BinaryToString(IEnumerable<bool> input, string separator)
        {
            return string.Join(separator, input.Select(x => x ? '1' : '0'));
        }

        static public string BinaryToString(IEnumerable<bool> input)
        {
            return BinaryToString(input, string.Empty);;
        }

        static public IEnumerable<bool> StringToBoolArray(string value)
        {
            bool[] result = new bool[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] != '1' && value[i] != '0')
                {
                    throw new ArgumentException("Value can't contains any symbols, except 0 or 1");
                }
                result[i] = (value[i] == '1');
            }
            return result;
        }

        #endregion

        #region Utility Methods for Boolean Functions

        /// <summary>
        /// Получить число всех возмождных функций алгебры логики от заданного числа переменных
        /// </summary>
        /// <param name="variables">Число переменных</param>
        /// <returns>Колличество функций алгебры логики</returns>
        static public Int64 FunctionsOfVariables(int variables)
        {
            return (Int64)Math.Pow(2, Math.Pow(2, variables));
        }

        /// <summary>
        /// Привести строку значений функции в нормальный вид, т.е. если не все значения определены, в начало дописывается "0"
        /// </summary>
        /// <param name="value">Заданная строка</param>
        /// <param name="variables">Заданное число переменных</param>
        /// <returns>Результирующая строка</returns>
        static public string GetCompletedStringFormByVariables(string value, int variables)
        {
            return GetCompletedStringFormByLength(value, (int)Math.Pow(2, variables));
        }

        /// <summary>
        /// Добавить в начало строки столько символов "0", что бы длина массива была равна заданной в параметрах длине
        /// </summary>
        /// <param name="value">Заданная строка</param>
        /// <param name="length">Заданная длина строки</param>
        /// <returns>Результирующая строка</returns>
        static public string GetCompletedStringFormByLength(string value, int length)
        {
            StringBuilder func = new StringBuilder(value);
            while (func.Length < length)
            {
                func.Insert(0, '0');
            }
            return func.ToString();
        }

        /// <summary>
        /// Перевод десятичного очисла в массив bool в формате с заданной длиной массива
        /// </summary>
        /// <param name="value">Десятичное число</param>
        /// <param name="length">Необходимая длина массива</param>
        /// <returns>Двоичное число</returns>
        static public bool[] GetCompletedBinaryFormByLength(long value, long length)
        {
            bool[] result = new bool[length];
            bool[] arr = DecToBinaryArray(value).ToArray();
            Int64 offset = length - arr.Length;
            for (int i = 0; i < length; i++)
            {
                result[i] = i < offset ? false : arr[i - offset];
            }
            return result;
        }

        /// <summary>
        /// Перевод десятичного очисла в массив bool в формате с заданной длиной массива
        /// </summary>
        /// <param name="value">Десятичное число</param>
        /// <param name="length">Необходимая длина массива</param>
        /// <returns>Двоичное число</returns>
        static public bool[] GetCompletedBinaryFormByVariables(long value, long variables)
        {
            return GetCompletedBinaryFormByLength(value, (int)Math.Pow(2, variables));
        }

        /// <summary>
        /// Перевод десятичного очисла в массив bool в формате с заданной длиной массива
        /// </summary>
        /// <param name="value">Массив bool</param>
        /// <param name="length">Необходимая длина массива</param>
        /// <returns>Двоичное число</returns>
        static public bool[] GetCompletedBinaryFormByVariables(bool[] value, long variables)
        {
            List<bool> result = value.ToList();
            while (result.Count < (int)Math.Pow(2, variables))
            {
                result.Insert(0, false);
            }
            return result.ToArray();
        }

        #endregion

        #region Extra Methods

        /// <summary>
        /// Получить компактную последовательность для задания области опредлеления функций от N переменных
        /// с заданным линейным порядком над множеством {0, 1}
        /// </summary>
        /// <param name="order">Линейный порядок</param>
        /// <param name="n">Число переменных</param>
        /// <returns>Множество наборов переменных в необходимом порядке</returns>
        static public List<long> GetCompactDomain(LinearOrder order, int n)
        {
            bool hi;
            bool low;
            if (order == 0)
            {
                hi = true;
                low = false;
            }
            else
            {
                hi = false;
                low = true;
            }
            List<bool[]> inputs = new List<bool[]>();
            bool[] firstInput = new bool[n];
            for (int i = 0; i < n; i++)
            {
                firstInput[i] = low;
            }
            inputs.Add(firstInput);
            for (int i = 1; i < (int)Math.Pow(2, n); i++)
            {
                bool flag = true;
                List<bool> comparer = inputs[i - 1].ToList().GetRange(1, n - 1).ToList();
                for (int j = 0; j < i - 1; j++)
                {
                    List<bool> prephix = inputs[j].ToList().GetRange(1, n - 1);
                    if (CollectionAreEquals(prephix, comparer))
                    {
                        prephix.Add(low);
                        inputs.Add(prephix.ToArray());
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    comparer.Add(hi);
                    inputs.Add(comparer.ToArray());
                }
            }
            return (from nabor in inputs select BinaryToDec(nabor)).ToList();
        }

        /// <summary>
        /// Сравнение двух коллекций объектов
        /// </summary>
        /// <typeparam name="T">Любой тип</typeparam>
        /// <param name="valueA">Коллекция объектов</param>
        /// <param name="valueB">Коллекция объектов</param>
        /// <returns>Результат сравнения</returns>
        static public bool CollectionAreEquals<T>(IEnumerable<T> valueA, IEnumerable<T> valueB)
        {
            var a = valueA.ToArray();
            var b = valueB.ToArray();
            if (a.Length != a.Length)
            {
                return false;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (!a[i].Equals(b[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Получить все входные наборы переменных, упорядоченные лексикографически
        /// </summary>
        /// <param name="variables">Число переменных</param>
        /// <returns>Множество наборов переменных</returns>
        static public List<bool[]> GetAllInputs(int variables)
        {
            List<bool[]> result = new List<bool[]>();
            for (int i = 0; i < Math.Pow(2, variables); i++)
            {
                result.Add(GetCompletedBinaryFormByLength(i, variables));
            }
            return result;
        }

        #endregion
    }
}
