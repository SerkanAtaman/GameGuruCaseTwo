using UnityEngine;
using GameGuruCaseTwo.Datas.GameData;
using GameGuruCaseTwo.Datas.AssetData;
using GameGuruCaseTwo.Systems.EventSystem;
using GameGuruCaseTwo.Entities.SliceSystem;
using GameGuruCaseTwo.Systems.ComboSystem;
using Zenject;

namespace GameGuruCaseTwo.Entities.Platform
{
    public class PlatformHandler
    {
        public PlatformSegment CurrentSegment { get; private set; }
        public PlatformSegment PreviousSegment { get; private set; }

        [Inject]
        private readonly PlayReferences _playReferences;
        [Inject]
        private readonly GameSettings _gameSettings;
        [Inject]
        private readonly GameAssets _gameAssets;
        [Inject]
        private readonly EventManager _eventManager;
        [Inject]
        private readonly PlatformSegmentSlicer _segmentSlicer;
        [Inject]
        private readonly SegmentComboManager _comboManager;

        private Vector3 _firstSegmentPos;

        public void Init()
        {
            PreviousSegment = new PlatformSegment(_playReferences.DummyPlatform, 3.0f, _gameAssets);

            _eventManager.OnInputReceived.AddListener(ReceiveInput);
        }

        public void StartPlatforming()
        {
            CurrentSegment = new PlatformSegment(_playReferences.DummyPlatform.position + new Vector3(10, 0, _gameSettings.FirstSegmentDistanceFromPlatform), 3.0f, _gameAssets);
            _firstSegmentPos = CurrentSegment.WorldPosition;
            //_currentLevelData.RemainingSegmentToSpawn--;
        }

        private void ReceiveInput()
        {
            if (CurrentSegment == null) return;

            CurrentSegment.StopMoving();

            _comboManager.CheckPlacementSuccess(PreviousSegment, CurrentSegment);

            bool successed = _segmentSlicer.SliceSegment(PreviousSegment.SegmentRenderer, CurrentSegment.SegmentRenderer);

            CurrentSegment.ResetLayer();

            PreviousSegment = CurrentSegment;

            //if (_currentLevelData.RemainingSegmentToSpawn <= 0) return;

            Vector3 newSegmentPos = Vector3.zero;
            newSegmentPos.y = _firstSegmentPos.y;
            newSegmentPos.z = CurrentSegment.WorldPosition.z + _gameSettings.DistanceBtwSegments;
            newSegmentPos.x = CurrentSegment.LeftSided ? _firstSegmentPos.x : -_firstSegmentPos.x;

            CurrentSegment = new PlatformSegment(newSegmentPos, PreviousSegment.ScaleX, _gameAssets);
            //_currentLevelData.RemainingSegmentToSpawn--;
            _eventManager.OnPlatformSegmentPlaced?.Invoke(successed);
        }
    }
}