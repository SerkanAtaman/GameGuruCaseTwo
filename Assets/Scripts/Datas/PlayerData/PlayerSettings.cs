using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGuruCaseTwo.Datas.PlayerData
{
    [CreateAssetMenu(menuName = "Datas/PlayerDatas/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField] public float VelocityZ { get; private set; }
        [field: SerializeField] public float HorizontalSpeed { get; private set; }
    }
}