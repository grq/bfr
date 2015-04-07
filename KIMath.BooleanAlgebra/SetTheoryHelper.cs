using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    /// <summary>
    /// Функции для выполнения операций из теории множеств
    /// </summary>
    public class SetTheoryHelper
    {
        /// <summary>
        /// Вычисление пересечения множеств
        /// </summary>
        /// <typeparam name="T">Тип элемента в множестве</typeparam>
        /// <param name="sets">Множество элементов</param>
        /// <returns>Пересечение множеств</returns>
        static public IEnumerable<T> GetIntersection<T>(IEnumerable<IEnumerable<T>> sets)
        {
            List<T> result = new List<T>();
            foreach (IEnumerable<T> set in sets)
            {
                List<IEnumerable<T>> otherSets = sets.Except(new List<IEnumerable<T>>() { set }).ToList();
                foreach (T element in set)
                {
                    if (!result.Contains(element))
                    {
                        bool add = true;
                        otherSets.ForEach(x => add = add && x.Contains(element));
                        if (add)
                        {
                            result.Add(element);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Получить объединение
        /// </summary>
        /// <typeparam name="T">Тип элементов в множестве</typeparam>
        /// <param name="sets">Множество элементов</param>
        /// <returns>Объединение множеств</returns>
        static public IEnumerable<T> GetUnion<T>(IEnumerable<IEnumerable<T>> sets)
        {
            List<T> result = new List<T>();
            foreach (IEnumerable<T> set in sets)
            {
                foreach (T el in set)
                {
                    if(!result.Contains(el))
                    {
                        result.Add(el);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Вычислить вхождение
        /// </summary>
        /// <typeparam name="T">Тип эдлементов в множестве</typeparam>
        /// <param name="basicSet">Оригинальное множество</param>
        /// <param name="inclusiveSet">Множество, которое входит (или не входит) в оригинальное</param>
        /// <returns>Отношение вхождения множества inclusiveSet в множество basicSet</returns>
        static public bool IsInclusion<T>(IEnumerable<T> basicSet, IEnumerable<T> inclusiveSet)
        {
            List<T> cuttedSet = basicSet.ToList();
            foreach(T el in inclusiveSet)
            {
                T elInBasic = cuttedSet.FirstOrDefault(x => x.Equals(el));
                if(elInBasic != null)
                {
                    cuttedSet.Remove(elInBasic);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
