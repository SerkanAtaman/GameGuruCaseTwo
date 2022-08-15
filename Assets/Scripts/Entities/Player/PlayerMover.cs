using UnityEngine;
using Zenject;
using GameGuruCaseTwo.Datas.PlayerData;
using GameGuruCaseTwo.Systems.EventSystem;
using GameGuruCaseTwo.Entities.Platform;

namespace GameGuruCaseTwo.Entities.PLayer
{
    
    public class PlayerMover : MonoBehaviour
    {
        [Inject]
        private readonly PlayerSettings _playerSettings;

        [Inject]
        private readonly EventManager _eventManager;

        [Inject]
        private readonly PlatformHandler _platformHandler;

        [SerializeField]
        private Rigidbody _rigidbody;
        

        private float _targetOffsetX;
        private float _horizontalDelta;

        private float _horizontalMoveStartX;

        private void Start()
        {
            _eventManager.OnPlatformSegmentPlaced.AddListener(SetPlayerOffset);
            _eventManager.OnPlayButtonPressed.AddListener(() => enabled = true);

            enabled = false;
        }

        private void FixedUpdate()
        {
            SetOffset();

            Vector3 vel = _rigidbody.velocity;
            vel.x = 0.0f;
            vel.z = _playerSettings.VelocityZ;

            _rigidbody.velocity = vel;
        }

        private void SetPlayerOffset(bool segmentPlacementSuccessed)
        {
            if (!segmentPlacementSuccessed) return;

            _targetOffsetX = _platformHandler.PreviousSegment.WorldPosition.x;
            _horizontalMoveStartX = _rigidbody.position.x;
            _horizontalDelta = 0.0f;
        }

        private void SetOffset()
        {
            if (_horizontalDelta > 1.0f) return;

            _horizontalDelta += Time.fixedDeltaTime * _playerSettings.HorizontalSpeed;

            Vector3 pos = _rigidbody.position;
            pos.x = Mathf.Lerp(_horizontalMoveStartX, _targetOffsetX, _horizontalDelta);
            _rigidbody.MovePosition(pos);
        }
    }
}