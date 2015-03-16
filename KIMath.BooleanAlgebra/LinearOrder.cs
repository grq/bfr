using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    /// <summary>
    /// Линейный порядок над множеством {0, 1}
    /// </summary>
    public enum LinearOrder
    {
        /// <summary>
        /// В работе указывается как g0
        /// </summary>
        TrueHigherThanFalse,

        /// <summary>
        /// В работе указывается как g1
        /// </summary>
        FalseHigherTheTrue
    }
}
