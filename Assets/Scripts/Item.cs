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

    public void ConfigureItem(ItemConfig config, int amount)
    {
        _itemConfig = config;
        _itemAmount = amount;

        itemImage.sprite = GetRandomClassSprite();
        itemAmount.text = $"{amount}";
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
        return _itemConfig.ClassSprites[Random.Range(0, _itemConfig.ClassSprites.Length)];
    }
    
    
}
