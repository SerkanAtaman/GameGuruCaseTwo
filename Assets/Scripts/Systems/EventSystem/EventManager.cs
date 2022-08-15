using UnityEngine.Events;

namespace GameGuruCaseTwo.Systems.EventSystem
{
    public class EventManager
    {
        public UnityEvent OnPlayButtonPressed;

        public UnityEvent OnLevelFailed;
        public UnityEvent OnLevelCompleted;
        public UnityEvent OnNextLevelStarted;

        public UnityEvent OnInputReceived;
        public UnityEvent OnPodiumDanceEnded;

        public UnityEvent<bool> OnPlatformSegmentPlaced;

        public EventManager()
        {
            OnPlayButtonPressed = new UnityEvent();
            OnInputReceived = new UnityEvent();
            OnLevelFailed = new UnityEvent();
            OnLevelCompleted = new UnityEvent();
            OnNextLevelStarted = new UnityEvent();
            OnPodiumDanceEnded = new UnityEvent();
            OnPlatformSegmentPlaced = new UnityEvent<bool>();
        }
    }
}