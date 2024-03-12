using System;
using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using static Utilities.CommonFields;
using Random = UnityEngine.Random;

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

        public static event Action<KeyValuePair<ItemConfig, int>> OnInventoryUpdateNeeded;
        public static event Action OnNextLevelNeeded;
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
                pointAmountField.text = $"x{outcome.Value}";
            }
            
            cardImage.sprite = outcome.Key.GetSelectedClassSprite();
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
                ItemAnimationHelper.InstantiateItemObjects(_currentItem.Value / 5, _currentItem.Key.ClassPointSprite);
                OnInventoryUpdateNeeded?.Invoke(_currentItem);
            });

            DOVirtual.DelayedCall(2.3f, () =>
            {
                ResetItemCard();
                OnNextLevelNeeded?.Invoke();
            });
        }

        private void DisableFieldsForBomb()
        {
            classPointsImage.gameObject.SetActive(false);
            pointAmountField.gameObject.SetActive(false);
        }

        private void ResetItemCard()
        {
            classPointsImage.gameObject.SetActive(true);
            pointAmountField.gameObject.SetActive(true);
            backSideImage.gameObject.SetActive(true);
            cardPanelImage.gameObject.SetActive(false);
            cardImage.sprite = null;
            pointAmountField.text = "";
            backSideImage.transform.rotation =  Quaternion.Euler(0, 0, 0);
            rewardPanel.gameObject.SetActive(false);
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
