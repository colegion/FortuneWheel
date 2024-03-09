using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class SliceController : MonoBehaviour
{
    [SerializeField] private Item[] levelRewards;

    public void InitializeLevelRewards(Dictionary<ItemConfig, int> items)
    {
        var count = 0;
        foreach (KeyValuePair<ItemConfig, int> item in items)
        {
            levelRewards[count].ConfigureItem(item);
            count++;
        }
    }
    
}
