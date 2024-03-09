using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class Item : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemAmount;

    private ItemConfig _itemConfig;
    private int _itemAmount;

    public void ConfigureItem(KeyValuePair<ItemConfig, int> item)
    {
        _itemConfig = item.Key;
        _itemAmount = item.Value;

        itemImage.sprite = GetRandomClassSprite();
        if(_itemConfig.ItemClass != CommonFields.ItemClass.Bomb)
            itemAmount.text = $"{_itemAmount}";
    }

    public Sprite GetItemClassSprite()
    {
        return _itemConfig.ClassPointSprite;
    }

    public int GetItemAmount()
    {
        return _itemAmount;
    }

    private Sprite GetRandomClassSprite()
    {
        return _itemConfig.GetRandomItemSprite();
    }
    
    
}
