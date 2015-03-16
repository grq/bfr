using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    /// <summary>
    /// Функция алгебры логики
    /// </summary>
    [Serializable]
    public class BooleanFunction
    {

        #region PrivateFields

        private bool[] _postClass = null;

        private bool? _isSelfDual = null;

        private bool? _isPreserving0 = null;

        private bool? _isPreserving1 = null;

        private bool? _isLinear = null;

        private bool? _isMonotone = null;

        private bool[] _zhegalkinCoefficents = null;

        private string _minimalDNF = null;

        private int? _repetivity = null;

        private List<int> _omega1 = null;

        private OmegaComplexityProcessor _g0ComplexityProcessor;

        private OmegaComplexityProcessor _g1ComplexityProcessor;

        #endregion

        #region PublicFields

        /// <summary>
        /// Значения функции на всех наборах переменных упорядоченных лексикографически
        /// </summary>
        public bool[] Value { get; private set; }

        /// <summary>
        /// Число переменных, которые принимает функция
        /// </summary>
        public int Variables { get; private set; }

        /// <summary>
        /// Является ли функция самодвойственной
        /// </summary>
        public bool IsSelfDual
        {
            get
            {
                return this._isSelfDual.HasValue ? this._isSelfDual.Value : this.IsSelfDualFunction();
            }
        }

        /// <summary>
        /// Является ли функция сохраняющей ноль
        /// </summary>
        public bool IsPreserving0
        {
            get
            {
                return this._isPreserving0.HasValue ? this._isPreserving0.Value : this.IsPreserving0Function();
            }
        }

        /// <summary>
        /// Является ли функция сохраняющей единицу
        /// </summary>
        public bool IsPreserving1
        {
            get
            {
                return this._isPreserving1.HasValue ? this._isPreserving1.Value : this.IsPreserving1Function();
            }
        }

        /// <summary>
        /// Является ли функция монотонной
        /// </summary>
        public bool IsMonotone
        {
            get
            {
                return this._isMonotone.HasValue ? this._isMonotone.Value : this.IsMonotoneFunction();
            }
        }

        /// <summary>
        /// Является ли функция линейной
        /// </summary>
        public bool IsLinear
        {
            get
            {
                return this._isLinear.HasValue ? this._isLinear.Value : this.IsLinearFunction();
            }
        }

        /// <summary>
        /// Возвращает все значения свойств Поста, в порядке S, T0, T1, L, M
        /// </summary>
        public bool[] PostClass
        {
            get
            {
                return this._postClass != null ? this._postClass : this.GetPostClass();
            }
        }

        /// <summary>
        /// Минимальная ДНФ функции (минимизация методом Квайна-МакКласки)
        /// </summary>
        public string MinimalDNF
        {
            get
            {
                return this._minimalDNF != null ? this._minimalDNF : this.GetMinimalDNF();
            }
        }

        /// <summary>
        /// Повторность функции (наибольшее число вхождений одной из переменных в минимальную ДНФ функции)
        /// </summary>
        public int Repetivity
        {
            get
            {
                return this._repetivity != null ? this._repetivity.Value : this.GetRepetivityValue();
            }
        }

        /// <summary>
        /// Пометка функции (удобно при проведении сложных экспериментов)
        /// </summary>
        public string Mark { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по бинарным значениям. Если число значений функции меньше 2^n, то в начало последовательности дописывается значение 0
        /// </summary>
        /// <param name="value">Значения функции на всех наборах, упорядоченных лексикографически. Передаётся массив bool</param>
        /// <param name="variables">Число переменных</param>
        public BooleanFunction(bool[] value, int variables)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Value");
            }
            if (value.Length > Math.Pow(2, variables))
            {
                throw new ArgumentException(string.Format("Function's value length can't be much then {0} for functions of {1} variables", Math.Pow(2, variables), variables));
            }
            if (value.Length < Math.Pow(2, variables))
            {
                value = this.SetCorrectLength(value, (int)Math.Pow(2, variables));
            }
            this.Value = value;
            this.Variables = variables;
        }

        /// <summary>
        /// Конструктор по строке значений. Если число значений функции меньше 2^n, то в начало последовательности дописывается значение 0
        /// </summary>
        /// <param name="value">Значения функции на всех наборах, упорядоченных лексикографически. Передаётся строка, содержащая значения "0" и "1"</param>
        /// <param name="variables">Число переменных</param>
        public BooleanFunction(string binaryValue, int variables)
        {
            if (binaryValue == null)
            {
                throw new ArgumentNullException("Value");
            }
            if (binaryValue.Length > Math.Pow(2, variables))
            {
                throw new ArgumentException(string.Format("Function's value length can't be much then {0} for functions of {1} variables", Math.Pow(2, variables), variables));
            }
            bool[] value = this.StringToBoolArray(binaryValue);
            if (value.Length < Math.Pow(2, variables))
            {
                value = this.SetCorrectLength(value, (int)Math.Pow(2, variables));
            }
            this.Value = value;
            this.Variables = variables;
        }

        /// <summary>
        /// Конструктор по десятичному значению. Число переводится в двоичную систему счисления, где каждая цифра интерпретируется как значение функции на наборе. 
        /// Наборы упорядочены лексикографически. Если число значений функции меньше 2^n, то в начало последовательности дописывается значение 0
        /// </summary>
        /// <param name="value">Значения функции на всех наборах, упорядоченных лексикографически. Передаётся строка, содержащая значения "0" и "1"</param>
        /// <param name="variables">Число переменных</param>
        public BooleanFunction(Int64 decimalValue, int variables)
        {
            bool[] value = BooleanAlgebraHelper.DecToBoolArray(decimalValue);
            if (value.Length > Math.Pow(2, variables))
            {
                throw new ArgumentException(string.Format("Function's value length can't be much then {0} for functions of {1} variables", Math.Pow(2, variables), variables));
            }
            if (value.Length < Math.Pow(2, variables))
            {
                value = this.SetCorrectLength(value, (int)Math.Pow(2, variables));
            }
            this.Value = value;
            this.Variables = variables;
        }

        #endregion

        #region PrivateMethods

        private void ResetAllTriggers()
        {
            this._postClass = null;
            this._isSelfDual = null;
            this._isPreserving0 = null;
            this._isPreserving1 = null;
            this._isLinear = null;
            this._isMonotone = null;
            this._zhegalkinCoefficents = null;
            this._minimalDNF = null;
            this._repetivity = null;
            this._omega1 = null;
        }

        private int GetRepetivityValue()
        {
            List<string> minVars = new List<string>();
            for (int i = 0; i < this.MinimalDNF.Length; i++)
            {
                if (this.MinimalDNF[i] == 'x')
                {

                    minVars.Add(this.MinimalDNF[i + 1] == '\'' ? this.MinimalDNF.Substring(i, 3).Remove(1, 1) : this.MinimalDNF.Substring(i, 2));
                }
            }
            int repNumber = 1;
            for (int i = 0; i < minVars.Count - 1; i++)
            {
                int curRep = 1;
                for (int j = i + 1; j < minVars.Count; j++)
                {
                    if (minVars[i] == minVars[j])
                    {
                        curRep++;
                    }
                }
                repNumber = curRep > repNumber ? curRep : repNumber;
            }
            return repNumber;
        }

        private string GetMinimalDNF()
        {
            string minim = "";
            string value = this.ToString();
            string[] trues = new string[Convert.ToInt64(Math.Pow(2, Variables)) * 2];
            string[] rtrues = new string[Convert.ToInt64(Math.Pow(2, Variables)) * 2];
            int c = 0;
            for (int i = 0; i < Convert.ToInt64(Math.Pow(2, Variables)); i++)
            {
                if (value[i].ToString() == "1")
                {
                    trues[c] = BooleanAlgebraHelper.GetBoolArrayAsString(this.SetCorrectLength(BooleanAlgebraHelper.DecToBoolArray(i), this.Variables));
                    c++;
                }
            }
            bool check = false;
            rtrues = trues;
            do
            {
                int co = 0;
                string[] trues1 = new string[Convert.ToInt64(Math.Pow(2, Variables)) * 2];
                for (int i = 0; i < c; i++)
                {
                    check = false;
                    for (int j = 0; j < c; j++)
                    {
                        int sed = 0;
                        for (int a = 0; a < Variables; a++)
                        {
                            if (trues[i][a] == trues[j][a]) sed++;
                        }
                        if (sed == Variables - 1)
                        {
                            check = true;
                            string nf = "";
                            for (int a = 0; a < Variables; a++)
                            {
                                if (trues[i][a] != trues[j][a]) nf = string.Concat(nf, "*");
                                else nf = string.Concat(nf, trues[i][a].ToString());
                            }
                            bool check2 = true;
                            for (int z = 0; z < co; z++)
                            {
                                if (nf == trues1[z]) check2 = false;
                            }
                            if (check2 == true)
                            {
                                trues1[co] = nf;
                                co++;
                            }
                        }
                    }
                    bool check3 = true;
                    for (int z = 0; z < co; z++)
                    {
                        if (trues[i] == trues1[z]) check3 = false;
                    }
                    if ((check == false) && (check3 == true))
                    {
                        trues1[co] = trues[i];
                        co++;
                    }
                }
                trues = trues1;
                c = co;
            }
            while (check == true);
            //Таблица покрытия
            string[,] picktable = new string[trues.Length, rtrues.Length];
            int cou = 0;
            c = 0;
            for (int i = 0; i < trues.Length - 1; i++)
            {
                for (int j = 0; j < rtrues.Length - 1; j++)
                {
                    if ((trues[i] != null) && (rtrues[j]) != null)
                    {
                        bool check4 = true;
                        for (int a = 0; a < Variables; a++)
                        {
                            if ((trues[i][a] != rtrues[j][a]) && (trues[i][a].ToString() != "*")) check4 = false;
                        }
                        if (check4 == true) picktable[i, j] = "+";
                        else picktable[i, j] = "_";
                    }
                }
                if (trues[i] != null) cou++;
                if (rtrues[i] != null) c++;
            }
            //Удаление лишних импликантов
            for (int i = cou - 1; i >= 0; i--)
            {
                int ntr = 0, ntrd = 0;
                for (int j = 0; j < c; j++)
                {
                    if (picktable[i, j] == "+")
                    {
                        ntr++;
                        for (int z = cou - 1; z >= 0; z--)
                        {
                            if ((z != i) && (picktable[z, j] != null) && (picktable[i, j] == picktable[z, j]))
                            {
                                ntrd++;
                                break;
                            }
                        }
                    }
                }
                if (ntr == ntrd) for (int j = 0; j < c; j++) picktable[i, j] = null;
                else
                {
                    for (int a = 0; a < Variables; a++)
                    {
                        if (trues[i][a].ToString() != "*")
                        {
                            if (trues[i][a].ToString() == "1") minim = string.Concat(minim, "x");
                            else minim = string.Concat(minim, "x'");
                            minim = string.Concat(minim, a + 1);
                        }
                    }
                    minim = string.Concat(minim, "v");
                }
            }
            if (minim.Length > 3) minim = minim.Substring(0, minim.Length - 1);
            this._minimalDNF = minim;
            return minim;
        }

        public bool[] GetZhegalkinCoefficents()
        {
            bool[] zhegalkinCoefficients = new bool[(int)Math.Pow(2, this.Variables)];
            zhegalkinCoefficients[0] = this.Value[0];
            for (int i = 1; i < zhegalkinCoefficients.Length; i++)
            {
                List<bool> values = new List<bool>() { this.Value[i] };
                bool[] input = BooleanAlgebraHelper.DecToBoolArray(i);
                // Ищем номера необходимых наборов
                List<int> naborCoefficents = new List<int>();
                for (int a = 0; a < input.Length; a++)
                {
                    if (input[a] && (int)Math.Pow(2, input.Length - a - 1) != i)
                    {
                        naborCoefficents.Add((int)Math.Pow(2, input.Length - a - 1));
                    }
                }
                // Ищем комбинации
                Dictionary<int, List<int[]>> combinations = new Dictionary<int, List<int[]>>();
                combinations.Add(1, naborCoefficents.Select(p => new int[1] { p }).ToList());
                int curLength = 2;
                while (curLength <= naborCoefficents.Count)
                {
                    combinations.Add(curLength, new List<int[]>());
                    foreach (var ad in combinations[curLength - 1])
                    {
                        foreach (var bd in combinations[1])
                        {
                            if (!ad.Contains(bd[0]))
                            {
                                var res = ad.ToList().Concat(bd).OrderBy(p => p).ToArray();
                                if (!combinations[curLength].Select(p => BooleanAlgebraHelper.CollectionAreEquals(p, res)).Contains(true))
                                {
                                    combinations[curLength].Add(res);
                                }
                            }
                        }
                    }
                    curLength++;
                }
                foreach (var ar in combinations.SelectMany(p => p.Value))
                {
                    if (ar.Sum() != i)
                    {
                        values.Add(zhegalkinCoefficients[ar.Sum()]);
                    }
                }
                bool result = zhegalkinCoefficients[0];
                foreach (var val in values)
                {
                    result = BooleanAlgebraHelper.Xor(result, val);
                }
                zhegalkinCoefficients[i] = result;
            }
            this._zhegalkinCoefficents = zhegalkinCoefficients;
            return zhegalkinCoefficients;
        }

        private OmegaComplexityProcessor GetComplexityProcessor(LinearOrder linearOrder)
        {
            switch (linearOrder)
            {
                case LinearOrder.TrueHigherThanFalse:
                    if (this._g0ComplexityProcessor == null)
                    {
                        IEnumerable<bool> boolSequence = this.GetCompactValue(linearOrder);
                        string sequence = BooleanAlgebraHelper.GetBoolArrayAsString(boolSequence);
                        this._g0ComplexityProcessor = new OmegaComplexityProcessor(sequence);
                    }
                    return this._g0ComplexityProcessor;
                case LinearOrder.FalseHigherTheTrue:
                    if (this._g1ComplexityProcessor == null)
                    {
                        IEnumerable<bool> boolSequence = this.GetCompactValue(linearOrder);
                        string sequence = BooleanAlgebraHelper.GetBoolArrayAsString(boolSequence);
                        this._g1ComplexityProcessor = new OmegaComplexityProcessor(sequence);
                    }
                    return this._g1ComplexityProcessor;
                default:
                    throw new Exception("Complexity proccessor couldn't be defined.");
            }
        }

        public int GetOmega0(LinearOrder linearOrder)
        {
            return this.GetComplexityProcessor(linearOrder).Omega0;
        }

        public int GetAbsoluteOmega1(LinearOrder linearOrder)
        {
            return this.GetComplexityProcessor(linearOrder).AbsoluteOmega1;
        }

        private bool IsSelfDualFunction()
        {
            this._isSelfDual = true;
            for (int i = 0; i < this.Value.Length / 2; i++)
            {
                if (this.Value[i] == this.Value[this.Value.Length - i - 1])
                {
                    this._isSelfDual = false;
                    return false;
                }
            }
            this._isSelfDual = true;
            return true;
        }

        private bool IsPreserving0Function()
        {
            this._isPreserving0 = (this.Value[0] == false);
            return this._isPreserving0.Value;
        }

        private bool IsPreserving1Function()
        {
            this._isPreserving1 = (this.Value.Last() == true);
            return this._isPreserving1.Value;
        }

        private bool IsMonotoneFunction()
        {
            List<bool[]> inputs = this.GetAllInputs();
            for (int i = 0; i < this.Value.Length - 1; i++)
            {
                for (int j = i; j < this.Value.Length; j++)
                {
                    if (this.IsComparableInputs(inputs[i], inputs[j]))
                    {
                        if (!BooleanAlgebraHelper.Implication(this.Value[i], this.Value[j]))
                        {
                            this._isMonotone = false;
                            return false;
                        }
                    }
                }
            }
            this._isMonotone = true;
            return true;
        }

        private bool IsLinearFunction()
        {
            /* Метод треугольника */
            bool result = true;
            bool[] set = this.Value;
            int setNumber = (int)Math.Pow(2, this.Variables);
            for (int i = 0; i < setNumber; i++)
            {
                bool[] newSet = new bool[setNumber - i - 1];
                for (int y = 0; y < newSet.Length; y++)
                {
                    newSet[y] = BooleanAlgebraHelper.Xor(set[y], set[y + 1]);
                    if (y == 0 && newSet[y] && BooleanAlgebraHelper.DecToBoolArray(i + 1).Where(x => x).ToList().Count > 1)
                        return false;
                }
                set = newSet;
                if (set.Where(x => x).ToList().Count == set.Length)
                {
                    break;
                }
            }
            return result;
        }

        private bool IsComparableInputs(bool[] inputA, bool[] inputB)
        {
            int unComparable = 0;
            for (int i = 0; i < inputA.Length; i++)
            {
                if (inputA[i] != inputB[i])
                {
                    unComparable++;
                    if (unComparable > 1)
                    {
                        return false;
                    }
                }
            }
            return unComparable == 1;
        }

        private List<bool[]> GetAllInputs()
        {
            List<bool[]> result = new List<bool[]>();
            for (int i = 0; i < Math.Pow(2, this.Variables); i++)
            {
                result.Add(this.SetCorrectLength(BooleanAlgebraHelper.DecToBoolArray(i), this.Variables));
            }
            return result;
        }

        private bool[] StringToBoolArray(string value)
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

        private bool[] SetCorrectLength(bool[] value, int length)
        {
            List<bool> result = value.ToList();
            while (result.Count < length)
            {
                result.Insert(0, false);
            }
            return result.ToArray();
        }

        private bool[] GetPostClass()
        {
            List<bool> result = new List<bool>();
            result.Add(this.IsSelfDual);
            result.Add(this.IsPreserving0);
            result.Add(this.IsPreserving1);
            result.Add(this.IsLinear);
            result.Add(this.IsMonotone);
            this._postClass = result.ToArray();
            return this._postClass;
        }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Возвращает значение свойства Поста для функции
        /// </summary>
        /// <param name="prop">Свойство Поста</param>
        /// <returns>False - функций не удовлетворяет свойству. True - функция удовлетворяет свойству.</returns>
        public bool GetPostPropertyValue(PostProperty prop)
        {
            switch(prop)
            {
                case PostProperty.SelfDual:
                    return this.IsSelfDual;
                case PostProperty.PreservingNil:
                    return this.IsPreserving0;
                case PostProperty.PreservingOne:
                    return this.IsPreserving1;
                case PostProperty.Linear:
                    return this.IsLinear;
                case PostProperty.Monotone:
                    return this.IsMonotone;
                default:
                    throw new Exception("Post Property is not defined!");
            }
        }

        /// <summary>
        /// Возвращает значения необходимых свойств Поста
        /// </summary>
        /// <param name="props">Множество свойств Поста</param>
        /// <returns>Упорядоченный множество, где False - функций не удовлетворяет свойству. True - функция удовлетворяет свойству.</returns>
        public IEnumerable<bool> GetPostProperties(PostProperty[] props)
        {
            List<bool> result = new List<bool>();
            foreach(PostProperty prop in props)
            {
                result.Add(this.GetPostPropertyValue(prop));
            }
            return result;
        }

        /// <summary>
        /// Возвращает значение функции на наобходимом наборе переменных. Набор переменных передаётся в виде строки.
        /// </summary>
        /// <param name="input">Набор переменных в виде строки, где 0 - False, 1 - True</param>
        /// <returns>Значение функции на наборе</returns>
        public bool GetByInput(string input)
        {
            return this.Value[BooleanAlgebraHelper.BinToDec(input)];
        }

        /// <summary>
        /// Возвращает значение функции на наобходимом наборе переменных. Набор переменных передаётся в виде десятичного числа, которое в дальнейшем переводится в двоичное,
        /// и интерпретируется как набор переменных.
        /// </summary>
        /// <param name="input">Набор переменных в виде десятичного числа</param>
        /// <returns>Значение функции на наборе</returns>
        public bool GetByInput(int input)
        {
            return this.Value[input];
        }

        /// <summary>
        /// Возвращает значение функции на наобходимом наборе переменных. Набор переменных передаётся в виде массива Bool.
        /// </summary>
        /// <param name="input">Набор переменных в виде массива Bool</param>
        /// <returns>Значение функции на наборе</returns>
        public bool GetByInput(IEnumerable<bool> input)
        {
            return this.Value[BooleanAlgebraHelper.BinToDec(input)];
        }

        /// <summary>
        /// Возвращает значения функции на наобходимых наборах переменных. Наборы переменных передаются в виде массива массивов Bool.
        /// </summary>
        /// <param name="inputs">Множество наборов переменных</param>
        /// <returns>Значения функции на переданных наборах, упорядоченные по переданным наборам</returns>
        public IEnumerable<bool> GetByInputs(IEnumerable<IEnumerable<bool>> inputs)
        {
            List<bool> result = new List<bool>();
            foreach (IEnumerable<bool> input in inputs)
            {
                result.Add(this.GetByInput(input));
            }
            return result;
        }

        /// <summary>
        /// Возвращает значения функции на наобходимых наборах переменных в виде строки. Наборы переменных передаются в виде массива массивов Bool.
        /// </summary>
        /// <param name="inputs">Множество наборов переменных</param>
        /// <returns>Значения функции на переданных наборах, упорядоченные по переданным наборам. Строка, где 0 - False, 1 - True</returns>
        public string GetStringByInputs(IEnumerable<IEnumerable<bool>> inputs)
        {
            return string.Join(string.Empty, this.GetByInputs(inputs).Select(x => x ? "1" : "0"));
        }

        /// <summary>
        /// Возвращает значения функции на всех наборах, упорядоченных в виде компактной последовательности
        /// </summary>
        /// <param name="order">Линейный порядок над множеством {0, 1}, требуется для построения компактной последовательности области определения</param>
        /// <returns>Все значения функции, в порядке построенной компактной последовательности области определения</returns>
        public IEnumerable<bool> GetCompactValue(LinearOrder order)
        {
            List<bool> result = new List<bool>();
            foreach (int i in BooleanAlgebraHelper.GetCompactDomain(order, this.Variables))
            {
                result.Add(this.Value[i]);
            }
            return result.ToArray();
        }

        /// <summary>
        /// Возвращает строку, определяющую сочетания всех свойств Поста функции, в порядке S, T0, T1, L, M
        /// </summary>
        /// <returns>Строка, где 1 - удовлетворяет свойству, 0 - не удовлетворяет свойству</returns>
        public string GetPostClassString()
        {
            StringBuilder result = new StringBuilder();
            foreach (bool i in this.PostClass)
            {
                result.Append(i ? '1' : '0');
            }
            return result.ToString();
        }

        #endregion

        #region BaseOverrides

        public override string ToString()
        {
            return BooleanAlgebraHelper.GetBoolArrayAsString(this.Value);
        }

        public override bool Equals(object obj)
        {
            return BooleanAlgebraHelper.CollectionAreEquals(this.Value, ((BooleanFunction)obj).Value);
        }

        #endregion
    }
}
