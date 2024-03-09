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
            var randomClass = (ItemClass)Random.Range(0, (int)ItemClass.Bomb);
            while (_usedClasses.Contains(randomClass))
            {
                randomClass = (ItemClass)Random.Range(0, (int)ItemClass.Bomb);
            }
            _usedClasses.Add(randomClass);
            return RewardItems[randomClass];
        }

        public ItemConfig GetBombItem()
        {
            return RewardItems[ItemClass.Bomb];
        }

        public void ClearUsedClasses()
        {
            _usedClasses.Clear();
        }
    }
}
