using UnityEngine;
using GameGuruCaseTwo.Systems.EventSystem;
using GameGuruCaseTwo.Systems.GameSystem;
using Zenject;

namespace GameGuruCaseTwo.Systems.InputSystem
{
    public class InputController : MonoBehaviour
    {
        [Inject]
        private readonly EventManager _eventManager;

        [SerializeField]
        private GameStateController _gameStateController;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_gameStateController.GameState != GameState.Run) return;

                _eventManager.OnInputReceived?.Invoke();
            }
        }
    }
}