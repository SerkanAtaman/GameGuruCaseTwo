using UnityEngine.Events;

namespace GameGuruCaseTwo.Systems.EventSystem
{
    public class EventManager
    {
        public UnityEvent OnPlayButtonPressed;

        public UnityEvent OnInputReceived;

        public UnityEvent<bool> OnPlatformSegmentPlaced;

        public EventManager()
        {
            OnPlayButtonPressed = new UnityEvent();
            OnInputReceived = new UnityEvent();
            OnPlatformSegmentPlaced = new UnityEvent<bool>();
        }
    }
}