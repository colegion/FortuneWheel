using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Utilities;

namespace Helpers
{
    public class IndicatorRotationHelper : MonoBehaviour
    {
        [SerializeField] private float rotationDuration;
        private int _directionFlag = 1;
        private Coroutine _rotate;
        private Tween _rotation;

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void RotateIndicator(int dummy, KeyValuePair<ItemConfig, int> dummyPair)
        {
            _rotate = StartCoroutine(Rotate());
        }

        private IEnumerator Rotate()
        {
            var targetAngle = _directionFlag == 1 ? 15 : -15;
            while (true)
            {
                _rotation = transform.DORotateQuaternion(Quaternion.Euler(0, 0, targetAngle), rotationDuration).SetEase(Ease.Linear).OnComplete(() =>
                {
                    _directionFlag *= -1;
                    targetAngle = _directionFlag == 1 ? 15 : -15;
                });

                yield return new WaitForSeconds(rotationDuration + 0.01f);
            }
        }

        private void StopRotating(KeyValuePair<ItemConfig, int> dummyPair)
        {
            if (_rotate != null)
            {
                StopCoroutine(_rotate);
                _rotate = null;
                _rotation?.Kill();
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        private void AddListeners()
        {
            LevelController.OnAnimationNeeded += RotateIndicator;
            WheelUIController.OnRewardDisplayNeeded += StopRotating;
        }

        private void RemoveListeners()
        {

            LevelController.OnAnimationNeeded -= RotateIndicator;
            WheelUIController.OnRewardDisplayNeeded -= StopRotating;
        }
    }
}
