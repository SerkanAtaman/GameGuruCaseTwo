using UnityEngine;
using GameGuruCaseTwo.Entities.PLayer;

namespace GameGuruCaseTwo
{
    public class PlayReferences : MonoBehaviour
    {
        [field: SerializeField] public Transform DummyPlatform { get; private set; }
        [field: SerializeField] public Transform Finisher { get; private set; }
        [field: SerializeField] public Player Player { get; private set; }

        public void SetFinisher(Transform finisher)
        {
            Finisher = finisher;
        }
    }
}