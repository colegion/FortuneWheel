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
            StartCoroutine(ConfigureItems(items));
        }

        private IEnumerator ConfigureItems(Dictionary<ItemConfig, int> levelItems)
        {
            var count = 0;
            foreach (KeyValuePair<ItemConfig, int> item in levelItems)
            {
                levelRewards[count].ConfigureItem(item);
                count++;
            
                yield return new WaitForSeconds(.2f);
            }
        }
    
    }
}
