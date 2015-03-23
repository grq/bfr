using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra.TestTheory
{
    /// <summary>
    /// Базовый класс. Содержит логику для логирования процесса вычислений.
    /// </summary>
    public abstract class CommonProcessor
    {
        protected bool _useConsole;

        protected void ConsoleWrite(string text = "")
        {
            if (this._useConsole)
            {
                Console.Write(text);
            }
        }

        protected void ConsoleWriteLine(string text = "")
        {
            if (this._useConsole)
            {
                Console.WriteLine(text);
            }
        }
    }
}
