using UnityEngine;

namespace GameGuruCaseTwo.Datas.GameData
{
    [CreateAssetMenu(menuName = "Datas/GameDatas/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [field:SerializeField] public float FirstSegmentDistanceFromPlatform { get; private set; }
        [field:SerializeField] public float DistanceBtwSegments { get; private set; }
        [field:SerializeField] public float ComboTolerance { get; private set; }
        [field:SerializeField] public int SegmentPerLevel { get; private set; }
    }
}