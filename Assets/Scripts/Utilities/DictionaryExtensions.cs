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
            List<TKey> keys = dictionary.Keys.Where(key => !key.Equals(CommonFields.ItemClass.Bomb)).ToList();
            int n = keys.Count;
            while (n > 0)
            {
                n--;
                int k = random.Next(n + 1);
                (keys[k], keys[n]) = (keys[n], keys[k]);
            }
        }

    }
}