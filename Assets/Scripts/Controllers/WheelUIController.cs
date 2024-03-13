using System;
using System.Collections;
using System.Collections.Generic;
using DataContainers;
using DG.Tweening;
using Helpers;
using UnityEngine;
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
            ResetWheelRotation();
        }

        private void ResetWheelRotation()
        {
            wheelBase.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        private void PlayWheelAnimation(int finalDestinationAngle, KeyValuePair<ItemConfig, int> outcome)
        {
            PlaySpin();
            _outcomeItem = outcome;
            _finalAngle = finalDestinationAngle;
        }
        
        private void PlaySpin()
        {
            animationHelper.PlaySpinAnimation();
        }

        private void RotateTowardsTarget()
        {
            if (_finalAngle > 180)
            {
                RotateWheel(180, () =>
                {
                    _finalAngle -= 180;
                    RotateWheel(_finalAngle, () =>
                    {
                        OnRewardDisplayNeeded?.Invoke(_outcomeItem);
                    });
                });
            }
            else
            {
                RotateWheel(_finalAngle, () => 
                {
                    OnRewardDisplayNeeded?.Invoke(_outcomeItem); 
                });
            }
        }

        private void RotateWheel(float rotationAmount, Action onComplete)
        {
            wheelBase.transform.DOLocalRotate(new Vector3(0, 0, rotationAmount), finalDuration, RotateMode.LocalAxisAdd).SetEase(ease).OnComplete(
                () =>
                {
                    onComplete?.Invoke();
                });
        }


        private void AddListeners()
        {
            LevelController.OnLevelReady += ConfigureWheelSprites;
            LevelController.OnAnimationNeeded += PlayWheelAnimation;
            WheelAnimationHelper.OnAnimationCompleted += RotateTowardsTarget;
        }

        private void RemoveListeners()
        {
            LevelController.OnLevelReady -= ConfigureWheelSprites;
            LevelController.OnAnimationNeeded -= PlayWheelAnimation;
            WheelAnimationHelper.OnAnimationCompleted -= RotateTowardsTarget;
        }
    }
}
