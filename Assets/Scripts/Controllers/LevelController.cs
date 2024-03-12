using System;
using System.Collections.Generic;
using System.Linq;
using DataContainers;
using Helpers;
using UnityEngine;
using Utilities;
using static Utilities.CommonFields;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private SliceController sliceController;
        [SerializeField] private ItemContainer itemContainer;
        private int _levelIndex = 0;
        private WheelType _currentLevelType = WheelType.Silver;
        private int _levelRewardFactor;
        private Dictionary<ItemConfig, int> _levelRewards = new Dictionary<ItemConfig, int>();

        public static event Action<WheelType> OnLevelReady;
        public static event Action<int, KeyValuePair<ItemConfig, int>> OnAnimationNeeded;

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
    
        private void Start()
        {
            InitializeLevel();
        }
    
        private void InitializeLevel()
        {
            SetLevelType();
            PopulateLevelItems();
        
            OnLevelReady?.Invoke(_currentLevelType);
            sliceController.InitializeLevelRewards(_levelRewards);
            _levelIndex++;
        }

        private void PopulateLevelItems()
        {
            int count = 8;
            _levelRewards.Clear();
            itemContainer.ClearUsedClasses();

            if (_currentLevelType == WheelType.Bronze)
            {
                _levelRewards.Add(itemContainer.GetBombItem(), 1);
                count = WHEEL_SLICE_COUNT - 1;
            }
        
            else if (_currentLevelType == WheelType.Gold)
            {
                _levelRewards.Add(itemContainer.GetSpecialItem(), 1);
                count = WHEEL_SLICE_COUNT - 1;
            }
        
            GetRandomItems(count);
            _levelRewards.Shuffle();
        }

        private void GetRandomItems(int itemCount)
        {
            for (int i = 0; i < itemCount; i++)
            {
                var item = itemContainer.GetRandomItem();
                var amount = GetRandomRewardAmount();
                _levelRewards.Add(item, amount);
            }
        }

        public void DecideWheelOutcome()
        {
            var randomOutcome = Random.Range(0, _levelRewards.Count);
            var outcomeItem = _levelRewards.ElementAt(randomOutcome);
            var targetAngle = randomOutcome * SLICE_ANGLE;
            OnAnimationNeeded?.Invoke(targetAngle, outcomeItem);
        }

        private int GetRandomRewardAmount()
        {
            return Random.Range(1, 5) * _levelRewardFactor;
        }

        private void SetLevelType()
        {
            if (_levelIndex == 0)
            {
                _currentLevelType = WheelType.Silver;
                _levelRewardFactor = RewardAmountFactors[WheelType.Silver];
                return;
            }
            if (_levelIndex % GOLDEN_WHEEL_COEFFICIENT == 0)
            {
                _currentLevelType = WheelType.Gold;
                _levelRewardFactor = RewardAmountFactors[WheelType.Gold];
                return;
            }
            if (_levelIndex % SILVER_WHEEL_COEFFICIENT == 0)
            {
                _currentLevelType = WheelType.Silver;
                _levelRewardFactor = RewardAmountFactors[WheelType.Silver];
                return;
            }
            _currentLevelType = WheelType.Bronze;
            _levelRewardFactor = RewardAmountFactors[WheelType.Bronze];
        }

        private void AddListeners()
        {
            ItemCardUIHelper.OnNextLevelNeeded += InitializeLevel;
        }

        private void RemoveListeners()
        {
            ItemCardUIHelper.OnNextLevelNeeded -= InitializeLevel;
        }
    }
}
