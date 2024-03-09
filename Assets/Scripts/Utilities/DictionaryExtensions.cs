using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    public static class DictionaryExtensions
    {
        public static void Shuffle<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            Random random = new Random();
            List<TKey> keys = new List<TKey>(dictionary.Keys);
            int n = keys.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (keys[k], keys[n]) = (keys[n], keys[k]);
            }

            // Construct a new dictionary with shuffled keys
            Dictionary<TKey, TValue> shuffledDictionary = new Dictionary<TKey, TValue>();
            foreach (var key in keys)
            {
                shuffledDictionary[key] = dictionary[key];
            }

            // Replace original dictionary with shuffled dictionary
            dictionary.Clear();
            foreach (var kvp in shuffledDictionary)
            {
                dictionary[kvp.Key] = kvp.Value;
            }
        }

    }
}