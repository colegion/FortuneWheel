using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using static Utilities.CommonFields;
public class ItemCardUIHelper : MonoBehaviour
{
    [SerializeField] private Image panelImage;

    [SerializeField] private Image cardImage;

    [SerializeField] private Image classPointsImage;

    [SerializeField] private TextMeshProUGUI pointAmountField;


    public void ConfigureItemCard(KeyValuePair<ItemConfig, int> outcome)
    {
        if (IsBomb(outcome.Key.ItemClass))
        {
            DisableFieldsForBomb();
            panelImage.color = Color.red;
        }
        else
        {
            cardImage.sprite = outcome.Key.GetSelectedClassSprite();
            pointAmountField.text = $"x{outcome.Value}";
        }
        
        classPointsImage.sprite = outcome.Key.ClassPointSprite;
    }

    private void DisableFieldsForBomb()
    {
        classPointsImage.gameObject.SetActive(false);
        pointAmountField.gameObject.SetActive(false);
    }

    private bool IsBomb(CommonFields.ItemClass itemClass) => itemClass == ItemClass.Bomb;
}
