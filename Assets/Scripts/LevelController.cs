using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataContainers;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;
using static Utilities.CommonFields;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
    [SerializeField] private SliceController sliceController;
    [SerializeField] private ItemContainer itemContainer;
    private int _levelIndex = 1;
    private WheelType _currentLevelType = WheelType.Silver;
    private int _levelRewardFactor;
    private Dictionary<ItemConfig, int> _levelRewards = new Dictionary<ItemConfig, int>();

    public static event Action<WheelType> OnLevelReady;
    public static event Action<int, Action> OnSpinNeeded;
    public static event Action<KeyValuePair<ItemConfig, int>> OnRewardDisplayNeeded;

    [ContextMenu("Test population")]
    public void InitializeLevel()
    {
        _levelIndex++;
        SetLevelType();
        PopulateLevelItems();
        
        sliceController.InitializeLevelRewards(_levelRewards);
        
        OnLevelReady?.Invoke(_currentLevelType);
    }

    private void Start()
    {
        InitializeLevel();
    }

    private void PopulateLevelItems()
    {
        _levelRewards.Clear();
        itemContainer.ClearUsedClasses();
        
        _levelRewards.Add(itemContainer.GetBombItem(), 1);
        for (int i = 0; i < WHEEL_SLICE_COUNT - 1; i++)
        {
            var item = itemContainer.GetRandomItem();
            var amount = GetRandomRewardAmount();
            _levelRewards.Add(item, amount);
        }
        
        _levelRewards.Shuffle();
    }

    public void DecideWheelOutcome()
    {
        var randomOutcome = Random.Range(0, _levelRewards.Count);
        var outcomeItem = _levelRewards.ElementAt(randomOutcome);
        var targetAngle = randomOutcome * SLICE_ANGLE;
        OnSpinNeeded?.Invoke(targetAngle, () =>
        {
            OnRewardDisplayNeeded?.Invoke(outcomeItem);
        });
    }

    private int GetRandomRewardAmount()
    {
        return Random.Range(1, 10) * _levelRewardFactor;
    }

    private void SetLevelType()
    {
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
}
