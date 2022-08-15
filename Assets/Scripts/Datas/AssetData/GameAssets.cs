using UnityEngine;

namespace GameGuruCaseTwo.Datas.AssetData
{
    [CreateAssetMenu(menuName = "Datas/AssetDatas/GameAssets")]
    public class GameAssets : ScriptableObject
    {
        [field: SerializeField] public GameObject PlatformSegmentRuinPref { get; set; }
        [field: SerializeField] public GameObject PlatformSegmentPref { get; set; }
        [field: SerializeField] public GameObject LevelFinisherPref { get; set; }
        [field: SerializeField] public AudioClip PianoSoundClip { get; set; }
        [field: SerializeField] public Material[] PlatformSegmentMaterials { get; set; }

        public Material GetRandomSegmentMat()
        {
            return PlatformSegmentMaterials[Random.Range(0, PlatformSegmentMaterials.Length)];
        }
    }
}