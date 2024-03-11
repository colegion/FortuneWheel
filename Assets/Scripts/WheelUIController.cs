using System;
using System.Collections;
using System.Collections.Generic;
using DataContainers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities;

public class WheelUIController : MonoBehaviour
{
    [SerializeField] private Image wheelBase;
    [SerializeField] private Image wheelIndicator;
    [SerializeField] private WheelConfigHolder configContainer;

    [SerializeField] private float turnDuration;
    [SerializeField] private float finalDuration;
    [SerializeField] private int angle;
    [SerializeField] private AnimationCurve ease;

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

    private void SpinWheel(int finalDestinationAngle, Action onComplete)
    {
        wheelBase.transform.DORotate(new Vector3(0, 0, -360f), turnDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).SetLoops(3, LoopType.Incremental).OnComplete(() =>
            {
                wheelBase.transform.DORotate(new Vector3(0, 0, angle), finalDuration).SetEase(ease).OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
            });
    }
    
    [ContextMenu("Test wheel spin")]
    public void TestSpin()
    {
        wheelBase.transform.DORotate(new Vector3(0, 0, -360f), turnDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear).SetLoops(3, LoopType.Incremental).OnComplete(() =>
            {
                wheelBase.transform.DORotate(new Vector3(0, 0, angle), finalDuration).SetEase(ease);
            });
    }

    private void AddListeners()
    {
        LevelController.OnLevelReady += ConfigureWheelSprites;
        LevelController.OnSpinNeeded += SpinWheel;
    }

    private void RemoveListeners()
    {
        LevelController.OnLevelReady -= ConfigureWheelSprites;
        LevelController.OnSpinNeeded -= SpinWheel;
    }
}
