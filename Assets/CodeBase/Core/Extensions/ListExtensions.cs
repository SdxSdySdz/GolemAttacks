using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Core.Extensions
{
    public static class ListExtensions
    {
        public static T GetRandomElement<T>(this IList<T> collection)
        {
            return collection[Random.Range(0, collection.Count)];
        }
    }
}