using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        ResetItemUI();
        itemImage.sprite = GetRandomClassSprite();
        itemImage.transform.DOScale(1, 1.2f).SetEase(Ease.OutBounce).SetLoops(1, LoopType.Yoyo);
        if(_itemConfig.ItemClass != CommonFields.ItemClass.Bomb)
            itemAmount.text = $"{_itemAmount}";
    }

    private void ResetItemUI()
    {
        itemImage.transform.localScale = new Vector3(0, 0, 0);
        itemAmount.text = "";
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
