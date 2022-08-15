using System.Collections;
using UnityEngine;

namespace Seroley.DelayHandling
{
    public class DelayerObject : MonoBehaviour
    {
        public void StartDelay(Delay delay)
        {
            StartCoroutine(Delay(delay));
        }

        private IEnumerator Delay(Delay delay)
        {
            float timer = 0.0f;

            while(timer < delay.DelayTime)
            {
                timer += Time.deltaTime;

                yield return null;
            }

            delay.OnDelayEnded?.Invoke();
        }
    }
}