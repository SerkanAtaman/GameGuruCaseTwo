using UnityEngine;
using Zenject;
using GameGuruCaseTwo.Entities.Platform;
using GameGuruCaseTwo.Systems.EventSystem;

namespace GameGuruCaseTwo.Entities.PLayer
{
    public class Player : MonoBehaviour
    {
        public PlayerMover PlayerMover { get; private set; }
        public PlayerAnimator PlayerAnimator { get; private set; }

        [Inject]
        private readonly PlatformHandler _platformHandler;
        [Inject]
        private readonly EventManager _eventManager;

        private void Awake()
        {
            PlayerMover = GetComponent<PlayerMover>();
            PlayerAnimator = transform.GetChild(0).GetComponent<PlayerAnimator>();
        }

        public void EnterNewSegment()
        {
            if (_platformHandler.PreviousSegment.ScaleX >= 0.3f) return;

            PlayerMover.Rigid.freezeRotation = false;
            GetComponent<PlayerMover>().enabled = false;

            if (Random.Range(0, 2) == 0)
            {
                PlayerMover.Rigid.AddForce(new Vector3(-50f, 0f, 5f), ForceMode.Impulse);
            }
            else
            {
                PlayerMover.Rigid.AddForce(new Vector3(50f, 0f, 5f), ForceMode.Impulse);
            }

            _eventManager.OnLevelFailed?.Invoke();
        }

        public void GoPodium()
        {
            _eventManager.OnLevelCompleted?.Invoke();
            PlayerMover.GoPodium();
        }

        public void StartDancing()
        {
            PlayerAnimator.SetAnimator(PlayerAnimatorTransition.RunToDance, EndDancing);
            PlayerAnimator.StartFacingForward();
        }

        private void EndDancing()
        {
            PlayerAnimator.StopFacingForward();
            _eventManager.OnPodiumDanceEnded?.Invoke();
        }
    }
}