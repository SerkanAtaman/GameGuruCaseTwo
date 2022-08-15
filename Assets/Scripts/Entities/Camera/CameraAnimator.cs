using System.Collections;
using UnityEngine;
using Zenject;
using Cinemachine;
using GameGuruCaseTwo.Systems.EventSystem;

namespace GameGuruCaseTwo.Entities.Camera
{
    public class CameraAnimator : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _defaultVirtualCam = null;
        [SerializeField] private CinemachineVirtualCamera _trackedVirtualCam = null;
        [SerializeField] private Transform _track = null;

        [Inject]
        private readonly PlayReferences _playReferences;

        [Inject]
        private readonly EventManager _eventManager;

        private void Start()
        {
            _eventManager.OnLevelCompleted.AddListener(AnimateCamera);
            _eventManager.OnLevelFailed.AddListener(DisableCameras);

            _defaultVirtualCam.enabled = true;
            _trackedVirtualCam.enabled = false;
        }

        private void AnimateCamera()
        {
            _trackedVirtualCam.transform.SetPositionAndRotation(_defaultVirtualCam.transform.position, _defaultVirtualCam.transform.rotation);

            _track.position = _playReferences.Player.transform.position;

            _defaultVirtualCam.enabled = false;
            _trackedVirtualCam.enabled = true;

            StartCoroutine(TrackAnimation());
        }
        private void DisableCameras()
        {
            _defaultVirtualCam.enabled = false;
            _trackedVirtualCam.enabled = false;
        }

        private void OnAnimationEnded()
        {
            _defaultVirtualCam.enabled = true;
            _trackedVirtualCam.enabled = false;
        }

        private IEnumerator TrackAnimation()
        {
            var dolly = _trackedVirtualCam.GetCinemachineComponent<CinemachineTrackedDolly>();
            dolly.m_PathPosition = 0f;

            float delta = 0.0f;

            while (delta <= 1.0f)
            {
                delta += Time.deltaTime * 0.3f;

                dolly.m_PathPosition = delta;

                yield return null;
            }

            OnAnimationEnded();
        }
    }
}