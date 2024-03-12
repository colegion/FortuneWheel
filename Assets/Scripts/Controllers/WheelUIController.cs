using System;
using System.Collections.Generic;
using DataContainers;
using DG.Tweening;
using Helpers;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Controllers
{
    public class WheelUIController : MonoBehaviour
    {
        [SerializeField] private Image wheelBase;
        [SerializeField] private Image wheelIndicator;
        [SerializeField] private WheelConfigHolder configContainer;
        [SerializeField] private WheelAnimationHelper animationHelper;

        [SerializeField] private float turnDuration;
        [SerializeField] private float finalDuration;
        [SerializeField] private int angle;
        [SerializeField] private AnimationCurve ease;
        public static event Action<KeyValuePair<ItemConfig, int>> OnRewardDisplayNeeded;
        private KeyValuePair<ItemConfig, int> _outcomeItem;
        private float _finalAngle;

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void ConfigureWheelSprites(CommonFields.WheelType wheelType)
        {
            var wheel = configContainer.DecideLevelWheelType(wheelType);
            wheelBase.sprite = wheel.WheelBase;
            wheelIndicator.sprite = wheel.WheelIndicator;
        }

        private void AnimationWheel(int finalDestinationAngle, KeyValuePair<ItemConfig, int> outcome)
        {
            TestSpin();
            _outcomeItem = outcome;
            _finalAngle = finalDestinationAngle;
        }
    
        [ContextMenu("Test wheel spin")]
        public void TestSpin()
        {
            animationHelper.PlaySpinAnimation();
        }

        private void RotateTowardsTarget()
        {
            wheelBase.transform.DOLocalRotate(new Vector3(0, 0, _finalAngle), finalDuration).SetEase(ease).OnComplete(() =>
            {
                OnRewardDisplayNeeded?.Invoke(_outcomeItem);
            });
        }

        private void AddListeners()
        {
            LevelController.OnLevelReady += ConfigureWheelSprites;
            LevelController.OnAnimationNeeded += AnimationWheel;
            WheelAnimationHelper.OnAnimationCompleted += RotateTowardsTarget;
        }

        private void RemoveListeners()
        {
            LevelController.OnLevelReady -= ConfigureWheelSprites;
            LevelController.OnAnimationNeeded -= AnimationWheel;
            WheelAnimationHelper.OnAnimationCompleted -= RotateTowardsTarget;
        }
    }
}
