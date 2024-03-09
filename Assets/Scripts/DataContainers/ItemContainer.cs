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
    }
}
