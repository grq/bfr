using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    /// <summary>
    /// Значения свойств Поста
    /// </summary>
    public enum PostPropertyValue
    {
        /// <summary>
        /// Является самодвойственной
        /// </summary>
        SelfDual,

        /// <summary>
        /// Не является самодвойственной
        /// </summary>
        NotSelfDual,
        
        /// <summary>
        /// Сохраняет ноль
        /// </summary>
        PreservingNil,

        /// <summary>
        /// Не сохраняет ноль
        /// </summary>
        NotPreservingNil,

        /// <summary>
        /// Сохраняет единицу
        /// </summary>
        PreservingOne,

        /// <summary>
        /// Не сохраняет единицу
        /// </summary>
        NotPreservingOne,

        /// <summary>
        /// Линейная
        /// </summary>
        Linear,

        /// <summary>
        /// Не линейная
        /// </summary>
        NotLinear,

        /// <summary>
        /// Монотонная
        /// </summary>
        Monotone,

        /// <summary>
        /// Не монотонная
        /// </summary>
        NotMonotone
    }
}
