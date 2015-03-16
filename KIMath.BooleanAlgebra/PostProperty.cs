using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    /// <summary>
    /// Свойства Поста
    /// </summary>
    public enum PostProperty
    {
        /// <summary>
        /// Самодвойственность
        /// </summary>
        SelfDual,
        
        /// <summary>
        /// Сохраняет ноль
        /// </summary>
        PreservingNil,

        /// <summary>
        /// Сохраняет единицу
        /// </summary>
        PreservingOne,

        /// <summary>
        /// Линейная
        /// </summary>
        Linear,

        /// <summary>
        /// Монотонная
        /// </summary>
        Monotone
    }
}
