using UnityEngine.Events;

namespace GameGuruCaseTwo.Systems.EventSystem
{
    public class EventManager
    {
        public UnityEvent OnPlayButtonPressed;

        public UnityEvent OnInputReceived;

        public EventManager()
        {
            OnPlayButtonPressed = new UnityEvent();
            OnInputReceived = new UnityEvent();
        }
    }
}