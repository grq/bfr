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

        public string PostPropertiesString { get; private set; }

        public PostClassBooleanFunctions(params PostPropertyValue[] list)
        {
            this.PostProperties = list;
            this.CreatePostPropertiesString();
        }

        private void CreatePostPropertiesString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetPostPropertyChar(PostPropertyValue.SelfDual, PostPropertyValue.NotSelfDual));
            sb.Append(this.GetPostPropertyChar(PostPropertyValue.PreservingNil, PostPropertyValue.NotPreservingNil));
            sb.Append(this.GetPostPropertyChar(PostPropertyValue.PreservingOne, PostPropertyValue.NotPreservingOne));
            sb.Append(this.GetPostPropertyChar(PostPropertyValue.Linear, PostPropertyValue.NotLinear));
            sb.Append(this.GetPostPropertyChar(PostPropertyValue.Monotone, PostPropertyValue.NotMonotone));
            this.PostPropertiesString = sb.ToString();
        }

        private string GetPostPropertyChar(PostPropertyValue yes, PostPropertyValue no)
        {
            if (this.PostProperties.Contains(yes))
            {
                return "1";
            }
            else if (this.PostProperties.Contains(no))
            {
                return "0";
            }
            else
            {
                return "~";
            }
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
