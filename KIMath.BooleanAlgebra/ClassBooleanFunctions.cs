using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    /// <summary>
    /// Класс функций алгебры логики
    /// </summary>
    public partial class ClassBooleanFunctions
    {
        /// <summary>
        /// Функции класса
        /// </summary>
        public ObservableCollection<BooleanFunction> Functions { get; private set; }

        public ClassBooleanFunctions()
        {
            this.Functions = new ObservableCollection<BooleanFunction>();
        }

        /// <summary>
        /// Добавить функцию(-ии) в класс
        /// </summary>
        /// <param name="list">Функция алгебры логики</param>
        public void AddFunction(params BooleanFunction[] list)
        {
            foreach(BooleanFunction function in list)
            {
                this.Functions.Add(function);
            }
        }
    }
}
