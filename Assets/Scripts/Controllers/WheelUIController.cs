using System;
using System.Collections;
using System.Collections.Generic;
using DataContainers;
using DG.Tweening;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Utilities;
using Image = UnityEngine.UI.Image;

namespace Controllers
{
    public class WheelUIController : MonoBehaviour
    {
        [SerializeField] private Image wheelBase;
        [SerializeField] private Image wheelIndicator;
        [SerializeField] private WheelConfigHolder configContainer;
        [SerializeField] private float finalDuration;
        [SerializeField] private AnimationCurve animationCurve;
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
            ResetWheelRotation();
        }

        private void ResetWheelRotation()
        {
            wheelBase.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        private void PlayWheelAnimation(int finalDestinationAngle, KeyValuePair<ItemConfig, int> outcome)
        {
            _finalAngle = finalDestinationAngle;
            _outcomeItem = outcome;
            PlayAnimation();
        }

        private void PlayAnimation()
        {
            var targetAngle = 3 * 360f + _finalAngle;
            wheelBase.transform.DORotate(new Vector3(0f, 0f, targetAngle), finalDuration, RotateMode.FastBeyond360)
                .SetEase(animationCurve).OnComplete(() =>
                {
                    OnRewardDisplayNeeded?.Invoke(_outcomeItem);
                });
        }

        private void AddListeners()
        {
            LevelController.OnLevelReady += ConfigureWheelSprites;
            LevelController.OnAnimationNeeded += PlayWheelAnimation;
        }

        private void RemoveListeners()
        {
            LevelController.OnLevelReady -= ConfigureWheelSprites;
            LevelController.OnAnimationNeeded -= PlayWheelAnimation;
        }
    }
}
