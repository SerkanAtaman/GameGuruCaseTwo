using UnityEngine;
using GameGuruCaseTwo.Systems.EventSystem;
using Zenject;
using UnityEngine.SceneManagement;

namespace GameGuruCaseTwo.UserInterface
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _homeCanvas;
        [SerializeField] private GameObject _winCanvas;
        [SerializeField] private GameObject _loseCanvas;

        [Inject]
        private readonly EventManager _eventManager;

        private void Start()
        {
            _homeCanvas.SetActive(true);

            _eventManager.OnPodiumDanceEnded.AddListener(OnDanceEnded);
            _eventManager.OnLevelFailed.AddListener(LevelFailed);
        }

        public void PlayButton()
        {
            _homeCanvas.SetActive(false);

            _eventManager.OnPlayButtonPressed?.Invoke();
        }

        public void NextLevelButton()
        {
            _winCanvas.SetActive(false);

            _eventManager.OnNextLevelStarted?.Invoke();
        }

        public void TryAgainButton()
        {
            _loseCanvas.SetActive(false);

            SceneManager.LoadScene(0);
        }

        private void OnDanceEnded()
        {
            _winCanvas.SetActive(true);
        }

        private void LevelFailed()
        {
            _loseCanvas?.SetActive(true);
        }
    }
}