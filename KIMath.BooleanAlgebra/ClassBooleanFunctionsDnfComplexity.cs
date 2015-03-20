using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    public partial class ClassBooleanFunctions
    {
        /// <summary>
        /// Наименьшее значение повторности функции среди всех функций в классе
        /// </summary>
        public long RepetivityMin
        {
            get
            {
                return this.Functions.Select(x => x.Repetivity).Min();
            }
        }

        /// <summary>
        /// Наибольшее значение повторности среди всех функций в классе
        /// </summary>
        public long RepetivityMax
        {
            get
            {
                return this.Functions.Select(x => x.Repetivity).Max();
            }
        }

        /// <summary>
        /// Среднее значение повторности среди всех функций в классе
        /// </summary>
        public double RepetivityAverage
        {
            get
            {
                return this.Functions.Select(x => x.Repetivity).Average();
            }
        }
    }
}
