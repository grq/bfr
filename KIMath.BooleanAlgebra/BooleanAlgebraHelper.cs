using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    public class BooleanAlgebraHelper
    {
        static public string DecToBin(long value)
        {
            StringBuilder result = new StringBuilder();
            do
            {
                result.Insert(0, (value % 2).ToString());
                value = value / 2;
            }
            while (value > 1);
            return result.Insert(0, value).ToString();
        }

        static public int BinToDec(IEnumerable<bool> a)
        {
            int bin = 0;
            bool[] arr = a.ToArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[arr.Length - i - 1]) bin += Convert.ToInt32(Math.Pow(2, i));
            }
            return bin;
        }

        static public int BinToDec(string a)
        {
            int bin = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[a.Length - i - 1] == '1') bin += Convert.ToInt32(Math.Pow(2, i));
            }
            return bin;
        }

        static public string GetNormalForm(string function, int variables)
        {
            StringBuilder func = new StringBuilder(function);
            while (func.Length < (int)Math.Pow(2, variables))
            {
                func.Insert(0, '0');
            }
            return func.ToString();
        }

        static public string GetNormalFormByLength(string function, int length)
        {
            StringBuilder func = new StringBuilder(function);
            while (func.Length < length)
            {
                func.Insert(0, '0');
            }
            return func.ToString();
        }

        static public bool[] GetNormalBoolForm(Int64 input, Int64 length)
        {
            bool[] result = new bool[length];
            bool[] value = DecToBoolArray(input);
            Int64 offset = length - value.Length;
            for (int i = 0; i < length; i++)
            {
                result[i] = i < offset ? false : value[i - offset];
            }
            return result;
        }

        static public bool[] DecToBoolArray(Int64 value)
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

        static public string DecToBoolString(int value, int length)
        {
            bool[] result = DecToBoolArray(value);
            string resultString = string.Join("", result.Select(x => x ? "1" : "0"));
            return GetNormalFormByLength(resultString, length);
        }

        static public string BoolToString(bool[] input)
        {
            return string.Join(string.Empty, input.Select(x => x ? '1' : '0'));
        }

        static public string BoolToString(bool[] input, string separator)
        {
            return string.Join(separator, input.Select(x => x ? '1' : '0'));
        }

        static public Int64 FunctionsOfVariables(int variables)
        {
            return (Int64)Math.Pow(2, Math.Pow(2, variables));
        }

        static public List<int> GetCompactDomain(LinearOrder order, int n)
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
            return (from nabor in inputs select BoolArrayToInt(nabor)).ToList();
        }

        static public string GetBoolArrayAsString(IEnumerable<bool> value)
        {
            StringBuilder sb = new StringBuilder();
            string result = string.Empty;
            foreach(bool v in value)
            {
                sb.Append(v ? '1' : '0');
            }
            return sb.ToString();
        }
    }
}
