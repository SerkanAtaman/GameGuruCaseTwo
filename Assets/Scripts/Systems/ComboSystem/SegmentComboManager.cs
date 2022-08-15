using UnityEngine;
using Zenject;
using GameGuruCaseTwo.Systems.AudioSystem;
using GameGuruCaseTwo.Datas.GameData;
using GameGuruCaseTwo.Entities.Platform;

namespace GameGuruCaseTwo.Systems.ComboSystem
{
    public class SegmentComboManager
    {
        [Inject]
        private readonly AudioManager _audioManager;

        public int CurrentCombo { get; private set; }

        private readonly float _tolerance;

        public SegmentComboManager(GameSettings gameSettings, AudioManager audioManager)
        {
            _audioManager = audioManager;

            CurrentCombo = 0;

            _tolerance = gameSettings.ComboTolerance;
        }

        public void CheckPlacementSuccess(PlatformSegment previousSegment, PlatformSegment currentSegment)
        {
            float failAmount = Mathf.Abs(currentSegment.WorldPosition.x - previousSegment.WorldPosition.x);

            if (failAmount <= _tolerance)
            {
                // success
                CurrentCombo++;
            }
            else
            {
                CurrentCombo = 0;
            }

            _audioManager.PlayPianoSource(CurrentCombo);
        }

        public void ResetCombo()
        {
            CurrentCombo = 0;
        }
    }
}