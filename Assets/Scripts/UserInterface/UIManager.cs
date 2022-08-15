using UnityEngine;
using GameGuruCaseTwo.Systems.EventSystem;
using GameGuruCaseTwo.Entities.Platform;
using Zenject;

namespace GameGuruCaseTwo.UserInterface
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _homeCanvas;

        [Inject]
        private readonly EventManager _eventManager;
        [Inject]
        private readonly PlatformHandler _platformHandler;

        private void Start()
        {
            _homeCanvas.SetActive(true);

            _platformHandler.Init();
            _platformHandler.StartPlatforming();
        }

        public void PlayButton()
        {
            _homeCanvas.SetActive(false);

            _eventManager.OnPlayButtonPressed?.Invoke();
        }
    }
}