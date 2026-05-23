using System.Collections.Generic;
using UnityEngine;
using System;

namespace KH
{
    public static class LoopExtensions
    {
        #region LIST ████████████████████████████████████████████████████████████████████████████████████

        /// <summary>
        /// Iterates through all elements in the list and executes the given action.
        /// </summary>
        /// <typeparam name="T">The element type of the list.</typeparam>
        /// <param name="list">The list to iterate.</param>
        /// <param name="action">The action to execute for each element.</param>
        public static void KHForEachElement<T>(this IList<T> list, Action<T> action)
        {
            for (int i = 0; i < list.Count; i++)
                action(list[i]);
        }

        /// <summary>
        /// Iterates through all elements in the list and executes the given action,
        /// providing both the element and its index.
        /// </summary>
        /// <typeparam name="T">The element type of the list.</typeparam>
        /// <param name="list">The list to iterate.</param>
        /// <param name="action">The action to execute for each element, with index.</param>
        public static void KHForEachElement<T>(this IList<T> list, Action<T, int> action)
        {
            for (int i = 0; i < list.Count; i++)
                action(list[i], i);
        }

        /// <summary>
        /// Iterates through elements in the list and executes the given function.
        /// The loop continues while the function returns <c>true</c>
        /// and stops when it returns <c>false</c>.
        /// </summary>
        /// <typeparam name="T">The element type of the list.</typeparam>
        /// <param name="list">The list to iterate.</param>
        /// <param name="action">
        /// A function executed for each element.  
        /// Return <c>true</c> to continue, <c>false</c> to break the loop.
        /// </param>
        public static void KHForEachElement<T>(this IList<T> list, Func<T, bool> action)
        {
            for (int i = 0; i < list.Count; i++)
                if (!action(list[i]))
                    break;
        }

        /// <summary>
        /// Iterates through elements in the list and executes the given function,
        /// providing both the element and its index.  
        /// The loop continues while the function returns <c>true</c>
        /// and stops when it returns <c>false</c>.
        /// </summary>
        /// <typeparam name="T">The element type of the list.</typeparam>
        /// <param name="list">The list to iterate.</param>
        /// <param name="action">
        /// A function executed for each element and index.  
        /// Return <c>true</c> to continue, <c>false</c> to break the loop.
        /// </param>
        public static void KHForEachElement<T>(this IList<T> list, Func<T, int, bool> action)
        {
            for (int i = 0; i < list.Count; i++)
                if (!action(list[i], i))
                    break;
        }

        /// <summary>
        /// Searches for the first element in the list that matches the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to search through.</param>
        /// <param name="predicate">
        /// A function that takes an element and returns <c>true</c> if it matches the search criteria.
        /// </param>
        /// <returns>
        /// The first element that satisfies the predicate, or <c>default(T)</c> if no element matches.
        /// </returns>
        public static T KHFindElement<T>(this IList<T> list, Func<T, bool> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                bool result = action(list[i]);
                if (result)
                    return list[i];
            }

            return default;
        }

        /// <summary>
        /// Searches for the first element in the list that matches the specified predicate,
        /// providing both the element and its index.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to search through.</param>
        /// <param name="predicate">
        /// A function that takes an element and its index, and returns <c>true</c> if it matches the search criteria.
        /// </param>
        /// <returns>
        /// The first element that satisfies the predicate, or <c>default(T)</c> if no element matches.
        /// </returns>
        public static T KHFindElement<T>(this IList<T> list, Func<T, int, bool> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                bool result = action(list[i], i);
                if (result)
                    return list[i];
            }

            return default;
        }

        /// <summary>
        /// Searches for all elements in the list that match the specified predicate and returns them as a new list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static List<T> KHFindAllElements<T>(this IList<T> list, Func<T, bool> action)
        {
            List<T> resultList = new();

            for (int i = 0; i < list.Count; i++)
            {
                bool result = action(list[i]);
                if (result)
                    resultList.Add(list[i]);
            }

            return resultList;
        }

        #endregion

        #region ForEachChild ████████████████████████████████████████████████████████████████████████████████████
        public static void KHForEachChild(this Transform transform, Action<Transform> action)
        {
            for (int i = 0; i < transform.childCount; i++)
                action(transform.GetChild(i));
        }

        public static void KHForEachChild(this Transform transform, Action<Transform, int> action)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                action(transform.GetChild(i), i);
            }
        }
        #endregion
    }
}
