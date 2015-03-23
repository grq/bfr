using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace KIMath.ResearchConsole
{
    static public class ResearchUtilities
    {
        static public void WriteTitle(string text)
        {
            Console.WriteLine("------- {0} -------", text);
            Console.WriteLine();
        }

        static public void ReplaceWordText(Word.Application application, string original, string replacement)
        {
            application.Selection.Find.ClearFormatting();
            application.Selection.Find.Replacement.Text = replacement;
            application.Selection.Find.Execute(original, Replace: Word.WdReplace.wdReplaceAll);
        }
    }
}
