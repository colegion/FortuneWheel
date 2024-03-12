using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities;
using static Utilities.CommonFields;

namespace DataContainers
{
    [CreateAssetMenu(fileName = "ItemContainer", menuName = "Item/ItemContainer")]
    public class ItemContainer : SerializedScriptableObject
    {
        public Dictionary<ItemClass, ItemConfig> RewardItems;

        private List<ItemClass> _usedClasses = new List<ItemClass>();

        public ItemConfig GetRandomItem()
        {
            var randomClass = (ItemClass)Random.Range(0, (int)ItemClass.Bomb - 1);
            while (_usedClasses.Contains(randomClass))
            {
                randomClass = (ItemClass)Random.Range(0, (int)ItemClass.Bomb - 1);
            }
            _usedClasses.Add(randomClass);
            return RewardItems[randomClass];
        }

        public ItemConfig GetBombItem()
        {
            return RewardItems[ItemClass.Bomb];
        }

        public ItemConfig GetSpecialItem()
        {
            return RewardItems[ItemClass.Special];
        }

        public void ClearUsedClasses()
        {
            _usedClasses.Clear();
        }
    }
}
