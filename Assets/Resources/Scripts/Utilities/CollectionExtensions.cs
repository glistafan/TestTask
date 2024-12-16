using System;
using System.Collections.Generic;

namespace Assets.Resources.Scripts.Utilities
{
    public static class CollectionExtensions
    {
        public static T GetRandomValue<T>(this IList<T> collection)
        {
            if (collection.Count == 0) return default;
            return collection[UnityEngine.Random.Range(0, collection.Count)];
        }

        public static List<T> GetRandomValuesList<T>(this IList<T> list, int count)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be negative.");
            if (count > list.Count) throw new ArgumentException("Count cannot be greater than the number of elements in the collection.");

            List<T> result = new List<T>();
            List<int> indices = new List<int>(list.Count - 1);

            for (int i = 0; i < list.Count; i++)
                indices.Add(i);

            for (int i = 0; i < count; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, indices.Count);
                result.Add(list[indices[randomIndex]]);
                indices.RemoveAt(randomIndex);
            }

            return result;
        }
    }
}