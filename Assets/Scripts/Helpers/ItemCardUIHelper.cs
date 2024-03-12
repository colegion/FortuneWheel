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

        [SerializeField] private TextMeshProUGUI gameOverText;

        [SerializeField] private ItemAnimationHelper ItemAnimationHelper;

        private KeyValuePair<ItemConfig, int> _currentItem;
        private bool _isGameOver;

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


        public void ConfigureItemCard(KeyValuePair<ItemConfig, int> outcome)
        {
            _currentItem = outcome;
            if (IsItemClassesSame(outcome.Key.ItemClass, ItemClass.Bomb))
            {
                DisableFieldsForBomb();
                cardPanelImage.color = Color.red;
                cardImage.sprite = outcome.Key.ClassPointSprite;
                _isGameOver = true;
                FadeGameOverTextIn();
            }
            else
            {
                pointAmountField.text = $"x{outcome.Value}";
                cardImage.sprite = IsItemClassesSame(outcome.Key.ItemClass, ItemClass.Special)
                    ? outcome.Key.ClassPointSprite: outcome.Key.GetSelectedClassSprite();
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
                if (_isGameOver) return;
                ItemAnimationHelper.InstantiateItemObjects(_currentItem.Value / 5, _currentItem.Key.ClassPointSprite);
                OnInventoryUpdateNeeded?.Invoke(_currentItem);
                DOVirtual.DelayedCall(1.6f, () =>
                {
                    ResetItemCard();
                    OnNextLevelNeeded?.Invoke();
                });
            });
        }
        
        private void DisableFieldsForBomb()
        {
            classPointsImage.gameObject.SetActive(false);
            pointAmountField.gameObject.SetActive(false);
        }

        private void FadeGameOverTextIn()
        {
            gameOverText.DOFade(1, 1.4f).SetEase(Ease.OutCirc);
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
        private bool IsItemClassesSame(ItemClass givenClass, ItemClass targetClass) => givenClass == targetClass;
    }
}
