using System;

namespace Seroley.DelayHandling
{
    public class Delay
    {
        public float DelayTime { get; private set; }

        public Action OnDelayEnded { get; private set; }

        public Delay(float delayTime, Action onDelayEnded)
        {
            DelayTime = delayTime;
            OnDelayEnded = onDelayEnded;

            DelayHandler.OnDelayCreated?.Invoke(this);
        }
    }
}