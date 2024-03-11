using System;
using DG.Tweening;
using UnityEngine;

namespace Helpers
{
    public class WheelAnimationHelper : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private string wheelAnimationName = "SpinAnimation";
        private int _hashedAnimId;

        private int _repetitions = 0;
        private int _desiredRepetitions = 3;

        private bool _animRequired;

        public static event Action OnAnimationCompleted;

        private void OnEnable()
        {
            _repetitions = 0;
            _hashedAnimId = Animator.StringToHash(wheelAnimationName);
        }

        public void PlaySpinAnimation()
        {
            ToggleAnimator(true);
            _animRequired = true;
            animator.Play(_hashedAnimId);
        }

        private void ToggleAnimator(bool toggle)
        {
            animator.enabled = toggle;
        }

        private void Update()
        {
            if (!_animRequired) return;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(wheelAnimationName))
            {
                if (_repetitions < _desiredRepetitions)
                {
                    animator.Play(_hashedAnimId);
                    _repetitions++;
                }
                else
                {
                    ToggleAnimator(false);
                    _animRequired = false;
                    _repetitions = 0;
                    OnAnimationCompleted?.Invoke();
                }
            }
        }
    }
}
