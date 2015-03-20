using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    public class PostClassBooleanFunctions: ClassBooleanFunctions
    {
        /// <summary>
        /// Свойства Поста, определяющие класс
        /// </summary>
        public PostPropertyValue[] PostProperties { get; private set; }

        public PostClassBooleanFunctions(params PostPropertyValue[] list)
        {
            this.PostProperties = list;
        }

        /// <summary>
        /// Добавить функцию в класс. Если функция не удовлетворяет заданным свойствам Поста, функция добавлена не будет.
        /// </summary>
        /// <param name="list">Фнкция алгебры логики</param>
        /// <returns>Добавлена функция или не добавлена</returns>
        public override bool AddFunction(BooleanFunction function)
        {
            if (function.HasPostPropertyValues(this.PostProperties))
            {
                this.Functions.Add(function);
                return true;
            }
            return false;
        }
    }
}
