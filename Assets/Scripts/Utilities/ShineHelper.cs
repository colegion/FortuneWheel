using System;
using DG.Tweening;
using UnityEngine;

namespace Utilities
{
    public class ShineHelper : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.DORotate(new Vector3(0, 0, 360f), 14f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        }

        private void OnDisable()
        {
            DOTween.Kill(transform);
        }
    }
}
