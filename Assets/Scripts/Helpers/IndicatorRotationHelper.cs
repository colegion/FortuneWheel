using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static Utilities.CommonFields;

public class IndicatorRotationHelper : MonoBehaviour
{
    [SerializeField] private float rotationDuration;
    private int _directionFlag;
    private bool _isRotating;

    private void OnTriggerEnter2D(Collider2D other)
    {
        RotateIndicator();
    }

    private void RotateIndicator()
    {
        if (_isRotating) return;
        _isRotating = true;
        transform.DORotate(new Vector3(0, 0, _directionFlag * SLICE_ANGLE), rotationDuration).SetEase(Ease.Flash).OnComplete(
            () =>
            {
                _isRotating = false;
                _directionFlag *= -1;
            });
    }
}
