using System;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using static Utilities.CommonFields;

namespace Controllers
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Button exitButton;
        [SerializeField] private Image exitButtonImage;
        [SerializeField] private Sprite goldExitSprite;
        [SerializeField] private RectTransform itemContent;
        [SerializeField] private InventoryItemUIHelper inventoryItem;

        private Dictionary<ItemClass, InventoryItemUIHelper> _spawnedInventoryItems = new Dictionary<ItemClass, InventoryItemUIHelper>();
        public static Dictionary<ItemConfig, int> PlayerInventory = new Dictionary<ItemConfig, int>();

        public static event Action<Dictionary<ItemConfig, int>> OnRewardsCollected;

        private void OnEnable()
        {
            AddListeners();
            PlayerInventory = new Dictionary<ItemConfig, int>();
        }

        private void OnValidate()
        {
            exitButton.onClick.AddListener(RaiseCollectEvent);
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void TrySpawnInventorySlot(KeyValuePair<ItemConfig, int> item)
        {
            if (item.Key.ItemClass == ItemClass.Bomb) return;
            if (AlreadySpawned(item.Key.ItemClass))
            {
                var tempItem = GetSpawnedItemIndex(item.Key.ItemClass);
                tempItem.IncreaseItemAmount(item.Value);
            }
            else
            {
                var tempItem = Instantiate(inventoryItem.gameObject, itemContent.transform);
                tempItem.GetComponent<InventoryItemUIHelper>().ConfigureItemUI(item.Key.ClassPointSprite, item.Value);
                _spawnedInventoryItems.Add(item.Key.ItemClass, tempItem.GetComponent<InventoryItemUIHelper>());
            }
        }

        public static void PopulateInventory(KeyValuePair<ItemConfig, int> rewardItem)
        {
            if (PlayerInventory.ContainsKey(rewardItem.Key))
            {
                PlayerInventory[rewardItem.Key] += rewardItem.Value;
            }
            else
            {
                PlayerInventory.Add(rewardItem.Key, rewardItem.Value);
            }
        }

        private InventoryItemUIHelper GetSpawnedItemIndex(ItemClass itemClass)
        {
            return _spawnedInventoryItems[itemClass];
        }

        private void DisableExitButton(int dummyNumber, KeyValuePair<ItemConfig, int> dummyPair)
        {
            exitButton.interactable = false;
        }

        private void EnableExitButton(WheelType type)
        {
            if (type != WheelType.Bronze)
                exitButtonImage.sprite = goldExitSprite;
            exitButton.interactable = true;
        }

        private void RaiseCollectEvent()
        {
            if (PlayerInventory.Count == 0) return;
            OnRewardsCollected?.Invoke(PlayerInventory);
        }

        private void AddListeners()
        {
            ItemCardUIHelper.OnInventoryUpdateNeeded += TrySpawnInventorySlot;
            LevelController.OnLevelReady += EnableExitButton;
            LevelController.OnAnimationNeeded += DisableExitButton;
        }

        private void RemoveListeners()
        {
            ItemCardUIHelper.OnInventoryUpdateNeeded -= TrySpawnInventorySlot;
            LevelController.OnLevelReady -= EnableExitButton;
            LevelController.OnAnimationNeeded -= DisableExitButton;
            exitButton.onClick.RemoveListener(RaiseCollectEvent);
        }
        private bool AlreadySpawned(ItemClass itemClass) => _spawnedInventoryItems.ContainsKey(itemClass);
    }
}
