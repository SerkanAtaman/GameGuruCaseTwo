using UnityEngine;
using Zenject;
using GameGuruCaseTwo.Datas.AssetData;
using GameGuruCaseTwo.Datas.GameData;
using GameGuruCaseTwo.Entities.Platform;

namespace GameGuruCaseTwo.Systems.LevelSystem
{
    public class LevelFinisherSpawner
    {
        [Inject]
        private readonly GameAssets _gameAssets;

        [Inject]
        private readonly GameSettings _gameSettings;

        [Inject]
        private readonly PlatformHandler _platformHandler;

        [Inject]
        private readonly PlayReferences _playReferences;

        public void SpawnLevelFinisher()
        {
            Vector3 pos = _platformHandler.FirstSegmentPos;
            pos.x = 0;
            pos.y = 0.0f;
            pos.z += 3f * _gameSettings.SegmentPerLevel - 0.6f;

            _playReferences.SetFinisher(Object.Instantiate(_gameAssets.LevelFinisherPref, pos, Quaternion.identity).transform);
        }
    }
}