using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    public class ProcessorClassBooleanFunctions
    {
        static public IEnumerable<PostClassBooleanFunctions> GetPostClasses(int variables, bool order = true)
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
            if (!order)
            {
                return result.Values;
            }
            return result.Values.OrderBy(x => BooleanAlgebraHelper.BinaryToDec(x.PostPropertiesString));
        }

        static public PostClassBooleanFunctions GetPostClass(PostPropertyValue[] props, int variables)
        {
            PostClassBooleanFunctions result = new PostClassBooleanFunctions(props);
            for (int startFunc = 0; startFunc < BooleanAlgebraHelper.FunctionsOfVariables(variables); startFunc++)
            {
                BooleanFunction function = new BooleanFunction(startFunc, variables);
                if (function.HasPostPropertyValues(props))
                {
                    if (!result.AddFunction(function))
                    {
                        throw new SystemException("Inner problems has been occured. Function can't be added to class.");
                    }
                }
            }
            return result;
        }

        static public PostClassBooleanFunctions GetPostClass(string props, int variables)
        {
            PostPropertyValue[] properties = new PostPropertyValue[5];
            properties[0] = props[0] == '1' ? PostPropertyValue.SelfDual : PostPropertyValue.NotSelfDual;
            properties[1] = props[1] == '1' ? PostPropertyValue.PreservingNil : PostPropertyValue.NotPreservingNil;
            properties[2] = props[2] == '1' ? PostPropertyValue.PreservingOne : PostPropertyValue.NotPreservingOne;
            properties[3] = props[3] == '1' ? PostPropertyValue.Linear : PostPropertyValue.NotLinear;
            properties[4] = props[4] == '1' ? PostPropertyValue.Monotone : PostPropertyValue.NotMonotone;
            return GetPostClass(properties, variables);
        }
    }
}
