using UnityEngine;

namespace GameGuruCaseTwo.Entities.PLayer
{
    [RequireComponent(typeof(Player))]
    public class PlayerCollisionHandler : MonoBehaviour
    {
        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 7)
            {
                _player.EnterNewSegment();
            }
            else if (other.gameObject.layer == 8)
            {
                _player.GoPodium();
            }
        }
    }
}