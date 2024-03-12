using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Controllers
{
    public class ChestController : MonoBehaviour
    {
        [SerializeField] private Sprite[] chestSprites;
        [SerializeField] private GameObject chestObject;
        [SerializeField] private GridLayoutGroup rewardLayoutParent;
        [SerializeField] private Item rewardCard;

        private Dictionary<ItemConfig, int> _rewards = new Dictionary<ItemConfig, int>();
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void SpawnRewardChest(Dictionary<ItemConfig, int> rewards)
        {
            _rewards = rewards;
            var chest = Instantiate(chestObject, transform);
            chest.GetComponent<Image>().sprite = GetStageChestSprite(CalculateTotalRewardAmount());
            StartCoroutine(SpawnRewardCards());
        }
        
        
        private IEnumerator SpawnRewardCards()
        {
            foreach (KeyValuePair<ItemConfig, int> reward in _rewards)
            {
                var tempCard = Instantiate(rewardCard, rewardLayoutParent.transform);
                tempCard.ConfigureItem(reward);

                yield return new WaitForSeconds(0.3f);
            }
        }

        private Sprite GetStageChestSprite(int rewardCount)
        {
            if (rewardCount >= 300) return chestSprites[4];
            if (rewardCount >= 200) return chestSprites[3];
            if (rewardCount >= 100) return chestSprites[2];
            if (rewardCount >= 50) return chestSprites[1];
            return chestSprites[0];
        }

        private int CalculateTotalRewardAmount()
        {
            var count = 0;
            foreach (KeyValuePair<ItemConfig, int> item in _rewards)
            {
                count += item.Value;
            }
            return count;
        }

        private void AddListeners()
        {
            Inventory.OnRewardsCollected += SpawnRewardChest;
        }

        private void RemoveListeners()
        {
            Inventory.OnRewardsCollected -= SpawnRewardChest;
        }
    }
}
