using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using static Utilities.CommonFields;
namespace Helpers
{
    public class ItemCardUIHelper : MonoBehaviour
    {
        [SerializeField] private Image rewardPanel;
        [SerializeField] private Image backSideImage;
        [SerializeField] private Image cardPanelImage;
        [SerializeField] private Image cardImage;
        [SerializeField] private Image classPointsImage;
        [SerializeField] private TextMeshProUGUI pointAmountField;

        [SerializeField] private ItemAnimationHelper ItemAnimationHelper;

        private KeyValuePair<ItemConfig, int> _currentItem;
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }


        private void ConfigureItemCard(KeyValuePair<ItemConfig, int> outcome)
        {
            _currentItem = outcome;
            if (IsBomb(outcome.Key.ItemClass))
            {
                DisableFieldsForBomb();
                cardPanelImage.color = Color.red;
            }
            else
            {
                cardImage.sprite = outcome.Key.GetSelectedClassSprite();
                pointAmountField.text = $"x{outcome.Value}";
            }
        
            classPointsImage.sprite = outcome.Key.ClassPointSprite;
            rewardPanel.gameObject.SetActive(true);
            FakeCardTurn();
        }
    
        [ContextMenu("Test fake turn")]
        public void FakeCardTurn()
        {
            backSideImage.transform.DORotate(new Vector3(0, 135, 0), 0.7f).SetEase(Ease.InBack).OnComplete(() =>
            {
                backSideImage.gameObject.SetActive(false);
                cardPanelImage.gameObject.SetActive(true);
                ItemAnimationHelper.InstantiateItemObjects(Random.Range(1, 5), _currentItem.Key.ClassPointSprite);
            });
        }

        private void DisableFieldsForBomb()
        {
            classPointsImage.gameObject.SetActive(false);
            pointAmountField.gameObject.SetActive(false);
        }

        private void AddListeners()
        {
            WheelUIController.OnRewardDisplayNeeded += ConfigureItemCard;
        }

        private void RemoveListeners()
        {
            WheelUIController.OnRewardDisplayNeeded -= ConfigureItemCard;
        }

        private bool IsBomb(ItemClass itemClass) => itemClass == ItemClass.Bomb;
    }
}
