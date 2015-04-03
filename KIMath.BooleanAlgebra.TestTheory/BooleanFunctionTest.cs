using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra.TestTheory
{
    /// <summary>
    /// Тест для функций алгебры логики
    /// </summary>
    public class BooleanFunctionTest
    {
        /// <summary>
        /// Наборы переменных
        /// </summary>
        public List<bool[]> Inputs { get; private set; }

        /// <summary>
        /// Длина теста
        /// </summary>
        public int Length { get; private set; }

        private string _stringValue;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="inputs">Наборы значений переменных</param>
        public BooleanFunctionTest(List<bool[]> inputs)
        {
            this.Inputs = inputs;
            this.Length = inputs.Count;
            this._stringValue = string.Format("({0})", string.Join(string.Empty, string.Join("; ", 
                this.Inputs.Select(x => "{" + BooleanAlgebraHelper.BinaryToString(x, ", ") + "}"))));
        }

        #region Base Overrides

        public override string ToString()
        {
            return this._stringValue;
        }

        public override bool Equals(object obj)
        {
            return this.ToString() == ((BooleanFunctionTest)obj).ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        #endregion
    }
}
