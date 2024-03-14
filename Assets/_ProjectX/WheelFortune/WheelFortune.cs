using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class WheelFortune : MonoBehaviour
{
    public Transform wheel;
    public AnimationCurve animationCurve;
    public float speed;
    public float rotateTime;
    public int tourCount;
    public bool isPositive;

    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        DOVirtual.DelayedCall(1, () =>
        {
            var targetAngle = tourCount * 360f + 240f; // Random.Range(0f, 6f) * 60f;
            // wheel.DORotateQuaternion(Quaternion.Euler(0f, 0f, targetAngle), rotateTime).setr.SetEase(animationCurve);
            wheel.DORotate(new Vector3(0f, 0f, targetAngle), rotateTime, RotateMode.FastBeyond360)
                .SetEase(animationCurve);
        });

    }
}
