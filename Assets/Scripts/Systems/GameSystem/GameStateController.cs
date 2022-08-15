using GameGuruCaseTwo.Systems.EventSystem;
using GameGuruCaseTwo.Entities.Platform;
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

        private void Start()
        {
            GameState = GameState.Home;
            _eventManager.OnPlayButtonPressed.AddListener(StartGame);
        }

        private void StartGame()
        {
            GameState = GameState.Run;
            _platformHandler.Init();
            _platformHandler.StartPlatforming();
        }
    }
}