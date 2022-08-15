using System.Collections;
using UnityEngine;

namespace GameGuruCaseTwo.Entities.Platform
{
    public class PlatformSegmentMono : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;

        private bool _stop;

        public void Move(bool leftSided)
        {
            Vector3 moveDir = leftSided ? Vector3.right : Vector3.left;

            StartCoroutine(MoveCoroutine(moveDir));
        }

        public void StopMoving()
        {
            _stop = true;
        }

        private IEnumerator MoveCoroutine(Vector3 dir)
        {
            _stop = false;
            while (!_stop)
            {
                transform.position += Time.deltaTime * _moveSpeed * dir;

                yield return null;
            }
        }
    }
}