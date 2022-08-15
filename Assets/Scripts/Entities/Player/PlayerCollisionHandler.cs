using Zenject;
using UnityEngine;
using GameGuruCaseTwo.Entities.Platform;
using GameGuruCaseTwo.Systems.EventSystem;

namespace GameGuruCaseTwo.Entities.PLayer
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        [Inject]
        private readonly PlatformHandler _platformHandler;
        [Inject]
        private readonly EventManager _eventManager;

        [SerializeField]
        private Rigidbody _rigidbody;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 7)
            {
                if (_platformHandler.PreviousSegment.ScaleX >= 0.3f) return;

                _rigidbody.freezeRotation = false;
                GetComponent<PlayerMover>().enabled = false;

                if (Random.Range(0, 2) == 0)
                {
                    _rigidbody.AddForce(new Vector3(-50f, 0f, 5f), ForceMode.Impulse);
                }
                else
                {
                    _rigidbody.AddForce(new Vector3(50f, 0f, 5f), ForceMode.Impulse);
                }

                //_eventManager.OnLevelFailed?.Invoke();
            }
            else if (other.gameObject.layer == 8)
            {
                //_eventManager.OnLevelCompleted?.Invoke();
                //GetComponent<PlayerMover>().GoPodium();
            }
        }
    }
}