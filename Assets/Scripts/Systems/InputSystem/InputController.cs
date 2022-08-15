using UnityEngine;
using GameGuruCaseTwo.Systems.EventSystem;
using Zenject;

namespace GameGuruCaseTwo.Systems.InputSystem
{
    public class InputController : MonoBehaviour
    {
        [Inject]
        private readonly EventManager _eventManager;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _eventManager.OnInputReceived?.Invoke();
            }
        }
    }
}