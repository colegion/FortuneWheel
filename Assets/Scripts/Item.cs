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

    public void ConfigureItem(KeyValuePair<ItemConfig, int> item, int delay)
    {
        _itemConfig = item.Key;
        _itemAmount = item.Value;
        ResetItemUI();
        itemImage.sprite = GetRandomClassSprite();
        transform.DOScale(1, .3f).SetEase(Ease.OutBack).SetDelay(0.1f * delay).SetLoops(1, LoopType.Yoyo);
        if(_itemConfig.ItemClass != CommonFields.ItemClass.Bomb)
            itemAmount.text = $"{_itemAmount}";
    }

    private void ResetItemUI()
    {
        transform.localScale = new Vector3(0, 0, 0);
        itemAmount.text = "";
    }

    private Sprite GetRandomClassSprite()
    {
        return _itemConfig.GetRandomItemSprite();
    }
    
    
}
