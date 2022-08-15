using UnityEngine;
using System;
using System.Collections;

namespace GameGuruCaseTwo.Entities.PLayer
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;

        private Action _animEvent;

        private bool _break;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animEvent = null;
        }

        public void SetAnimator(PlayerAnimatorTransition animatorTransition, Action animEvent = null)
        {
            switch (animatorTransition)
            {
                case PlayerAnimatorTransition.IdleToRun:
                {
                        _animator.SetTrigger("Run");
                        break;
                }
                case PlayerAnimatorTransition.RunToDance:
                {
                    _animator.SetTrigger("Dance");
                    break;
                }
            }

            _animEvent = animEvent;
        }

        public void InvokeAnimEvent()
        {
            _animEvent?.Invoke();
        }

        public void StartFacingForward()
        {
            StartCoroutine(FaceForward());
        }

        public void StopFacingForward()
        {
            _break = true;
        }

        private IEnumerator FaceForward()
        {
            _break = false;
            while (!_break)
            {
                transform.localRotation = Quaternion.identity;

                yield return null;
            }
        }
    }
}