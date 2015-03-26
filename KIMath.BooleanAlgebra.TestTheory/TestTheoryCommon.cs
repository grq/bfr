using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra.TestTheory
{
    /// <summary>
    /// Общие методы для вычисления тестов
    /// </summary>
    internal class TestTheoryCommon
    {
        /// <summary>
        /// Получить все наборы переменных, на которых значения хотя бы для двух функций различаются 
        /// </summary>
        /// <param name="functions">Множество функций</param>
        /// <param name="variables">Число переменных</param>
        public static List<bool[]> ExcludeInputs(IEnumerable<BooleanFunction> functions, int variables)
        {

            List<bool[]> allInputs = BooleanAlgebraHelper.GetAllInputs(variables);
            List<bool[]> inputs = new List<bool[]>();
            /* Для каждого набора переменных из всех возможных */
            foreach (bool[] input in allInputs)
            {
                bool inputResult = false;
                bool? comparer = null;
                /* Просматриваем каждую функцию */
                foreach (BooleanFunction func in functions)
                {
                    /* Если сравнитель не определён */
                    if (!comparer.HasValue)
                    {
                        /* Присваиваем ему значение первой функции на этом наборе */
                        comparer = func.GetByInput(input);
                    }
                    /* Если сравнитель определён */
                    else
                    {
                        /* Сравниваем его со значением функции на этом наборе */
                        if (comparer.Value != func.GetByInput(input))
                        {
                            /* Если значения не совпадают... */
                            inputResult = true;
                            break;
                        }
                    }
                }
                /* ...добавляем этот набор в результат */
                if (inputResult)
                {
                    inputs.Add(input);
                }
            }
            return inputs;
        }
    }
}
