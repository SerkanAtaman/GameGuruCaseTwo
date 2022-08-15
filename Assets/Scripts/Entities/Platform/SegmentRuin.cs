using UnityEngine;
using GameGuruCaseTwo.Datas.AssetData;

namespace GameGuruCaseTwo.Entities.Platform
{
    public class SegmentRuin
    {
        public SegmentRuin(Vector3 worldPos, Vector3 scale, Material mat, GameAssets assets)
        {
            Transform stack = Object.Instantiate(assets.PlatformSegmentRuinPref, worldPos, Quaternion.identity).transform;

            stack.localScale = scale;
            stack.GetComponent<Renderer>().material = mat;

            stack.gameObject.AddComponent<Rigidbody>();

            Vector3 forceDir = worldPos.x >= 0 ? Vector3.right : Vector3.left;
            Vector3 torgueDir = worldPos.x >= 0 ? Vector3.back : Vector3.forward;

            stack.GetComponent<Rigidbody>().AddForce(forceDir * 100);
            stack.GetComponent<Rigidbody>().AddTorque(torgueDir * 10);
        }
    }
}