using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    public class ProcessorClassBooleanFunctions
    {
        static public IEnumerable<PostClassBooleanFunctions> GetPostClasses(int variables)
        {
            Dictionary<int, PostClassBooleanFunctions> result = new Dictionary<int, PostClassBooleanFunctions>();
            for (int startFunc = 0; startFunc < BooleanAlgebraHelper.FunctionsOfVariables(variables); startFunc++)
            {
                BooleanFunction function = new BooleanFunction(startFunc, variables);
                if (!result.Keys.Contains(function.PostClassHash))
                {
                    result.Add(function.PostClassHash, new PostClassBooleanFunctions(function.PostClassValues));
                }
                if (!result[function.PostClassHash].AddFunction(function))
                {
                    throw new SystemException("Inner problems has been occured. Function can't be added to class.");
                }
            }
            return result.Values;
        }

        static public PostClassBooleanFunctions GetPostClass(PostPropertyValue[] props, int variables)
        {
            PostClassBooleanFunctions result = new PostClassBooleanFunctions(props);
            for (int startFunc = 0; startFunc < BooleanAlgebraHelper.FunctionsOfVariables(variables); startFunc++)
            {
                BooleanFunction function = new BooleanFunction(startFunc, variables);
                if (function.HasPostPropertyValues(props))
                {
                    if (result.AddFunction(function))
                    {
                        throw new SystemException("Inner problems has been occured. Function can't be added to class.");
                    }
                }
            }
            return result;
        }
    }
}
