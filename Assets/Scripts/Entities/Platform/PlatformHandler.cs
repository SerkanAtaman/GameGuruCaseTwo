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

        public Vector3 FirstSegmentPos { get; private set; }

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

        private int _segmentPerLevel;
        private int _remainingSegments;

        public void Init()
        {
            PreviousSegment = new PlatformSegment(_playReferences.DummyPlatform, 3.0f, _gameAssets);
            _segmentPerLevel = _gameSettings.SegmentPerLevel;
            _remainingSegments = _segmentPerLevel;

            _eventManager.OnInputReceived.AddListener(ReceiveInput);
        }

        public void StartPlatforming()
        {
            CurrentSegment = new PlatformSegment(_playReferences.DummyPlatform.position + new Vector3(10, 0, _gameSettings.FirstSegmentDistanceFromPlatform), 3.0f, _gameAssets);
            FirstSegmentPos = CurrentSegment.WorldPosition;
            _remainingSegments--;
        }

        public void StartNextLevel()
        {
            _comboManager.ResetCombo();
            _remainingSegments = _segmentPerLevel;
            CurrentSegment = new PlatformSegment(_playReferences.Finisher.position + new Vector3(10, -0.5f, 2.4f), 3.0f, _gameAssets);
            _remainingSegments--;
            FirstSegmentPos = CurrentSegment.WorldPosition;
        }

        private void ReceiveInput()
        {
            if (CurrentSegment == null) return;

            CurrentSegment.StopMoving();

            _comboManager.CheckPlacementSuccess(PreviousSegment, CurrentSegment);

            bool successed = _segmentSlicer.SliceSegment(PreviousSegment.SegmentRenderer, CurrentSegment.SegmentRenderer);

            CurrentSegment.ResetLayer();

            PreviousSegment = CurrentSegment;

            if(_remainingSegments <= 0) return;

            Vector3 newSegmentPos = Vector3.zero;
            newSegmentPos.y = FirstSegmentPos.y;
            newSegmentPos.z = CurrentSegment.WorldPosition.z + _gameSettings.DistanceBtwSegments;
            newSegmentPos.x = CurrentSegment.LeftSided ? FirstSegmentPos.x : -FirstSegmentPos.x;

            CurrentSegment = new PlatformSegment(newSegmentPos, PreviousSegment.ScaleX, _gameAssets);
            _remainingSegments--;
            _eventManager.OnPlatformSegmentPlaced?.Invoke(successed);
        }
    }
}