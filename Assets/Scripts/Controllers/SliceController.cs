using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Controllers
{
    public class SliceController : MonoBehaviour
    {
        [SerializeField] private Item[] levelRewards;

        public void InitializeLevelRewards(Dictionary<ItemConfig, int> items)
        {
            ConfigureItems(items);
        }

        private void ConfigureItems(Dictionary<ItemConfig, int> levelItems)
        {
            var count = 0;
            foreach (KeyValuePair<ItemConfig, int> item in levelItems)
            {
                levelRewards[count].ConfigureItem(item, count);
                count++;
            }
        }
    
    }
}
