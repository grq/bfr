using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    public partial class ClassBooleanFunctions
    {
        #region Omega0 G0

        /// <summary>
        /// Наименьшее значение Omega0 среди всех функций в классе при линейном порядке G0 (1 > 0)
        /// </summary>
        public long Omega0G0Min
        {
            get
            {
                return this.Functions.Select(x => x.GetOmega0(LinearOrder.TrueHigherThanFalse)).Min();
            }
        }

        /// <summary>
        /// Наибольшее значение Omega0 среди всех функций в классе при линейном порядке G0 (1 > 0)
        /// </summary>
        public long Omega0G0Max
        {
            get
            {
                return this.Functions.Select(x => x.GetOmega0(LinearOrder.TrueHigherThanFalse)).Max();
            }
        }

        /// <summary>
        /// Среднее значение Omega0 среди всех функций в классе при линейном порядке G0 (1 > 0)
        /// </summary>
        public double Omega0G0Average
        {
            get
            {
                return this.Functions.Select(x => x.GetOmega0(LinearOrder.TrueHigherThanFalse)).Average();
            }
        }

        #endregion

        #region Omega0 G1

        /// <summary>
        /// Наименьшее значение Omega0 среди всех функций в классе при линейном порядке G1 (0 > 1)
        /// </summary>
        public long Omega0G1Min
        {
            get
            {
                return this.Functions.Select(x => x.GetOmega0(LinearOrder.FalseHigherTheTrue)).Min();
            }
        }

        /// <summary>
        /// Наибольшее значение Omega0 среди всех функций в классе при линейном порядке G1 (0 > 1)
        /// </summary>
        public long Omega0G1Max
        {
            get
            {
                return this.Functions.Select(x => x.GetOmega0(LinearOrder.FalseHigherTheTrue)).Max();
            }
        }

        /// <summary>
        /// Среднее значение Omega0 среди всех функций в классе при линейном порядке G1 (0 > 1)
        /// </summary>
        public double Omega0G1Average
        {
            get
            {
                return this.Functions.Select(x => x.GetOmega0(LinearOrder.FalseHigherTheTrue)).Average();
            }
        }

        #endregion

        #region Omega1 G0

        /// <summary>
        /// Наименьшее значение Omega1 среди всех функций в классе при линейном порядке G0 (1 > 0)
        /// </summary>
        public long Omega1G0Min
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega1(LinearOrder.TrueHigherThanFalse)).Min();
            }
        }

        /// <summary>
        /// Наибольшее значение Omega1 среди всех функций в классе при линейном порядке G0 (1 > 0)
        /// </summary>
        public long Omega1G0Max
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega1(LinearOrder.TrueHigherThanFalse)).Max();
            }
        }

        /// <summary>
        /// Среднее значение Omega1 среди всех функций в классе при линейном порядке G0 (1 > 0)
        /// </summary>
        public double Omega1G0Average
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega1(LinearOrder.TrueHigherThanFalse)).Average();
            }
        }

        #endregion

        #region Omega1 G1

        /// <summary>
        /// Наименьшее значение Omega1 среди всех функций в классе при линейном порядке G1 (0 > 1)
        /// </summary>
        public long Omega1G1Min
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega1(LinearOrder.FalseHigherTheTrue)).Min();
            }
        }

        /// <summary>
        /// Наибольшее значение Omega1 среди всех функций в классе при линейном порядке G1 (0 > 1)
        /// </summary>
        public long Omega1G1Max
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega1(LinearOrder.FalseHigherTheTrue)).Max();
            }
        }

        /// <summary>
        /// Среднее значение Omega1 среди всех функций в классе при линейном порядке G1 (0 > 1)
        /// </summary>
        public double Omega1G1Average
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega1(LinearOrder.FalseHigherTheTrue)).Average();
            }
        }

        #endregion

        #region Omega2 G0

        /// <summary>
        /// Наименьшее значение Omega2 среди всех функций в классе при линейном порядке G0 (1 > 0)
        /// </summary>
        public long Omega2G0Min
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega2(LinearOrder.TrueHigherThanFalse)).Min();
            }
        }

        /// <summary>
        /// Наибольшее значение Omega2 среди всех функций в классе при линейном порядке G0 (1 > 0)
        /// </summary>
        public long Omega2G0Max
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega2(LinearOrder.TrueHigherThanFalse)).Max();
            }
        }

        /// <summary>
        /// Среднее значение Omega2 среди всех функций в классе при линейном порядке G0 (1 > 0)
        /// </summary>
        public double Omega2G0Average
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega2(LinearOrder.TrueHigherThanFalse)).Average();
            }
        }

        #endregion

        #region Omega2 G1

        /// <summary>
        /// Наименьшее значение Omega2 среди всех функций в классе при линейном порядке G1 (0 > 1)
        /// </summary>
        public long Omega2G1Min
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega2(LinearOrder.FalseHigherTheTrue)).Min();
            }
        }

        /// <summary>
        /// Наибольшее значение Omega2 среди всех функций в классе при линейном порядке G1 (0 > 1)
        /// </summary>
        public long Omega2G1Max
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega2(LinearOrder.FalseHigherTheTrue)).Max();
            }
        }

        /// <summary>
        /// Среднее значение Omega2 среди всех функций в классе при линейном порядке G1 (0 > 1)
        /// </summary>
        public double Omega2G1Average
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega2(LinearOrder.FalseHigherTheTrue)).Average();
            }
        }

        #endregion

        #region Omega3 G0

        /// <summary>
        /// Наименьшее значение Omega3 среди всех функций в классе при линейном порядке G0 (1 > 0)
        /// </summary>
        public long Omega3G0Min
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega3(LinearOrder.TrueHigherThanFalse)).Min();
            }
        }

        /// <summary>
        /// Наибольшее значение Omega3 среди всех функций в классе при линейном порядке G0 (1 > 0)
        /// </summary>
        public long Omega3G0Max
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega3(LinearOrder.TrueHigherThanFalse)).Max();
            }
        }

        /// <summary>
        /// Среднее значение Omega3 среди всех функций в классе при линейном порядке G0 (1 > 0)
        /// </summary>
        public double Omega3G0Average
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega3(LinearOrder.TrueHigherThanFalse)).Average();
            }
        }

        #endregion

        #region Omega3 G1

        /// <summary>
        /// Наименьшее значение Omega3 среди всех функций в классе при линейном порядке G1 (0 > 1)
        /// </summary>
        public long Omega3G1Min
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega3(LinearOrder.FalseHigherTheTrue)).Min();
            }
        }

        /// <summary>
        /// Наибольшее значение Omega3 среди всех функций в классе при линейном порядке G1 (0 > 1)
        /// </summary>
        public long Omega3G1Max
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega3(LinearOrder.FalseHigherTheTrue)).Max();
            }
        }

        /// <summary>
        /// Среднее значение Omega3 среди всех функций в классе при линейном порядке G1 (0 > 1)
        /// </summary>
        public double Omega3G1Average
        {
            get
            {
                return this.Functions.Select(x => x.GetAbsoluteOmega3(LinearOrder.FalseHigherTheTrue)).Average();
            }
        }

        #endregion
    }
}
