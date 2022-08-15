using UnityEngine;
using System.Collections;
using Zenject;
using GameGuruCaseTwo.Datas.PlayerData;
using GameGuruCaseTwo.Systems.EventSystem;
using GameGuruCaseTwo.Entities.Platform;

namespace GameGuruCaseTwo.Entities.PLayer
{
    
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMover : MonoBehaviour
    {
        public Rigidbody Rigid { get; private set; }

        [Inject]
        private readonly PlayerSettings _playerSettings;

        [Inject]
        private readonly EventManager _eventManager;

        [Inject]
        private readonly PlatformHandler _platformHandler;

        [Inject]
        private readonly PlayReferences _playReferences;

        private Player _player;

        private float _targetOffsetX;
        private float _horizontalDelta;

        private float _horizontalMoveStartX;

        private void Start()
        {
            Rigid = GetComponent<Rigidbody>();
            _player = GetComponent<Player>();

            _eventManager.OnPlatformSegmentPlaced.AddListener(SetPlayerOffset);
            _eventManager.OnPlayButtonPressed.AddListener(StartMoving);

            enabled = false;
        }

        private void FixedUpdate()
        {
            CheckFail();
            SetOffset();

            Vector3 vel = Rigid.velocity;
            vel.x = 0.0f;
            vel.z = _playerSettings.VelocityZ;

            Rigid.velocity = vel;
        }

        private void StartMoving()
        {
            enabled = true;
            _player.PlayerAnimator.SetAnimator(PlayerAnimatorTransition.IdleToRun);
        }

        private void StartMovingAgain(bool segmentPlacementSuccessed)
        {
            _player.PlayerAnimator.SetAnimator(PlayerAnimatorTransition.IdleToRun);
            _eventManager.OnPlatformSegmentPlaced.RemoveListener(StartMovingAgain);
            SetPlayerOffset(segmentPlacementSuccessed);
            enabled = true;
        }

        private void SetPlayerOffset(bool segmentPlacementSuccessed)
        {
            if (!segmentPlacementSuccessed) return;

            _targetOffsetX = _platformHandler.PreviousSegment.WorldPosition.x;
            _horizontalMoveStartX = Rigid.position.x;
            _horizontalDelta = 0.0f;
        }

        private void CheckFail()
        {
            if(Rigid.velocity.y <= -1.0f)
            {
                enabled = false;
                _eventManager.OnLevelFailed?.Invoke();
            }
        }

        private void SetOffset()
        {
            if (_horizontalDelta > 1.0f) return;

            _horizontalDelta += Time.fixedDeltaTime * _playerSettings.HorizontalSpeed;

            Vector3 pos = Rigid.position;
            pos.x = Mathf.Lerp(_horizontalMoveStartX, _targetOffsetX, _horizontalDelta);
            Rigid.MovePosition(pos);
        }

        private IEnumerator PodiumPlacement()
        {
            float delta = 0.0f;
            Vector3 startPos = transform.position;
            Vector3 targetPos = _playReferences.Finisher.position + new Vector3(0, 0, 0);

            while (delta <= 1.0f)
            {
                delta += Time.deltaTime * 2f;

                transform.position = Vector3.Lerp(startPos, targetPos, delta);

                yield return null;
            }

            _player.StartDancing();
        }

        public void GoPodium()
        {
            enabled = false;
            StartCoroutine(PodiumPlacement());
            _eventManager.OnPlatformSegmentPlaced.AddListener(StartMovingAgain);
        }
    }
}