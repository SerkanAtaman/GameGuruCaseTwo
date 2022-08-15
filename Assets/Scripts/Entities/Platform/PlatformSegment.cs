using UnityEngine;
using GameGuruCaseTwo.Datas.AssetData;

namespace GameGuruCaseTwo.Entities.Platform
{
    public class PlatformSegment
    {
        private readonly Transform _segmentObject;

        public MeshRenderer SegmentRenderer
        {
            get
            {
                return _segmentObject.GetComponent<MeshRenderer>();
            }
        }

        public Vector3 WorldPosition
        {
            get
            {
                return _segmentObject.position;
            }
        }

        public bool LeftSided
        {
            get
            {
                return _leftSided;
            }
            private set
            {
                _leftSided = value;
            }
        }
        private bool _leftSided;

        public float ScaleX
        {
            get
            {
                return _segmentObject.localScale.x;
            }
        }

        public PlatformSegment(Transform segmentObject, float scaleX, GameAssets assets)
        {
            _segmentObject = segmentObject;
            _segmentObject.localScale = new Vector3(scaleX, _segmentObject.localScale.y, _segmentObject.localScale.z);

            SegmentRenderer.material = assets.GetRandomSegmentMat();
        }

        public PlatformSegment(Vector3 position, float scaleX, GameAssets assets)
        {

            _segmentObject = Object.Instantiate(assets.PlatformSegmentPref, position, Quaternion.identity).transform;
            _segmentObject.localScale = new Vector3(scaleX, _segmentObject.localScale.y, _segmentObject.localScale.z);

            SegmentRenderer.material = assets.GetRandomSegmentMat();

            LeftSided = position.x < 0;

            _segmentObject.GetComponent<PlatformSegmentMono>().Move(LeftSided);
        }

        public void StopMoving()
        {
            _segmentObject.GetComponent<PlatformSegmentMono>().StopMoving();
        }

        public void ResetLayer()
        {
            _segmentObject.gameObject.layer = 0;
        }
    }
}