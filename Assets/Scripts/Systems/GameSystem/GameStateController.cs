using GameGuruCaseTwo.Systems.EventSystem;
using GameGuruCaseTwo.Entities.Platform;
using GameGuruCaseTwo.Systems.LevelSystem;
using Zenject;
using UnityEngine;

namespace GameGuruCaseTwo.Systems.GameSystem
{
    public class GameStateController : MonoBehaviour
    {
        public GameState GameState { get; private set; }

        [Inject]
        private readonly EventManager _eventManager;

        [Inject]
        private readonly PlatformHandler _platformHandler;

        [Inject]
        private readonly LevelFinisherSpawner _finisherSpawner;

        private void Start()
        {
            GameState = GameState.Home;
            _eventManager.OnPlayButtonPressed.AddListener(StartGame);
            _eventManager.OnLevelCompleted.AddListener(LevelCompleted);
            _eventManager.OnNextLevelStarted.AddListener(NextLevelStarted);
        }

        private void StartGame()
        {
            GameState = GameState.Run;
            _platformHandler.Init();
            _platformHandler.StartPlatforming();
            _finisherSpawner.SpawnLevelFinisher();
        }

        private void LevelCompleted()
        {
            GameState = GameState.Podium;
        }

        private void NextLevelStarted()
        {
            _platformHandler.StartNextLevel();
            _finisherSpawner.SpawnLevelFinisher();

            GameState = GameState.Run;
        }
    }
}