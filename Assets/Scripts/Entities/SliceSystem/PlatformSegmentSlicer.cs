using UnityEngine;
using GameGuruCaseTwo.Datas.AssetData;
using GameGuruCaseTwo.Utilities.Physic;
using GameGuruCaseTwo.Entities.Platform;

namespace GameGuruCaseTwo.Entities.SliceSystem
{
    public class PlatformSegmentSlicer
    {
        private readonly LayerMask _platformableLayer;
        private readonly GameAssets _gameAssets;

        public PlatformSegmentSlicer(GameAssets assets)
        {
            _gameAssets = assets;
            _platformableLayer = LayerMask.GetMask(new string[] { "Platformable" });
        }

        public bool SliceSegment(MeshRenderer previousSegment, MeshRenderer segmentToCut)
        {
            Vector3[] hitPoints = PhysicUtility.GetHitPoints(CalculateCastPoints(previousSegment), _platformableLayer);

            if (hitPoints.Length == 0)
            {
                return false;
            }
            else if (hitPoints.Length == 1)
            {
                if (hitPoints[0].x > 0)
                {
                    CutSegmentFromRightSide(segmentToCut, hitPoints);
                }
                else if (hitPoints[0].x < 0)
                {
                    CutSegmentFromLeftSide(segmentToCut, hitPoints);
                }
            }
            else if (hitPoints.Length == 2)
            {
                CutSegmentIntoThreeParts(segmentToCut, hitPoints);
            }

            return true;
        }

        private Vector3[] CalculateCastPoints(MeshRenderer previousSegment)
        {
            Vector3 castPointRight = previousSegment.bounds.max;
            castPointRight.y -= previousSegment.bounds.extents.y;
            castPointRight.z -= 0.5f;

            Vector3 castPointLeft = previousSegment.bounds.min;
            castPointLeft.y += previousSegment.bounds.extents.y;
            castPointLeft.z += previousSegment.bounds.extents.z * 2.0f;
            castPointLeft.z -= 0.5f;

            return new Vector3[] { castPointLeft, castPointRight };
        }

        private void CutSegmentIntoThreeParts(MeshRenderer segmentToCut, Vector3[] hitPoints)
        {
            Transform segmentTransform = segmentToCut.transform;

            float newScaleX = Mathf.Abs(hitPoints[0].x - hitPoints[1].x);
            Vector3 newScale = new Vector3(newScaleX, segmentTransform.localScale.y, segmentTransform.localScale.z);
            Vector3 newPos = hitPoints[0] + (hitPoints[1] - hitPoints[0]) * 0.5f;
            newPos.y = segmentTransform.position.y;
            newPos.z = segmentTransform.position.z;

            float leftStackScaleX = Mathf.Abs(hitPoints[0].x - segmentToCut.bounds.min.x);
            Vector3 minMiddleBound = segmentToCut.bounds.min;
            minMiddleBound.y += segmentTransform.localScale.y * 0.5f;
            Vector3 leftStackPos = hitPoints[0] + (minMiddleBound - hitPoints[0]) * 0.5f;
            leftStackPos.y = segmentTransform.position.y;
            leftStackPos.z = segmentTransform.position.z;
            Vector3 leftStackScale = new Vector3(leftStackScaleX, segmentTransform.localScale.y, segmentTransform.localScale.z);

            new SegmentRuin(leftStackPos, leftStackScale, segmentToCut.material, _gameAssets);

            float rightStackScaleX = Mathf.Abs(hitPoints[1].x - segmentToCut.bounds.max.x);
            Vector3 maxMiddleBound = segmentToCut.bounds.min;
            maxMiddleBound.y += segmentTransform.localScale.y * 0.5f;
            maxMiddleBound.x = segmentToCut.bounds.max.x;
            Vector3 rightStackPos = hitPoints[1] + (maxMiddleBound - hitPoints[1]) * 0.5f;
            rightStackPos.y = segmentTransform.position.y;
            rightStackPos.z = segmentTransform.position.z;
            Vector3 rightStackScale = new Vector3(rightStackScaleX, segmentTransform.localScale.y, segmentTransform.localScale.z);

            new SegmentRuin(rightStackPos, rightStackScale, segmentToCut.material, _gameAssets);

            segmentTransform.localScale = newScale;
            segmentTransform.position = newPos;
        }

        private void CutSegmentFromRightSide(MeshRenderer segmentToCut, Vector3[] hitPoints)
        {
            Transform segmentTransform = segmentToCut.transform;

            float newScaleX = hitPoints[0].x - segmentToCut.bounds.min.x;
            Vector3 newScale = new Vector3(newScaleX, segmentTransform.localScale.y, segmentTransform.localScale.z);
            Vector3 minLeftBound = segmentToCut.bounds.min;
            minLeftBound.y = segmentToCut.bounds.max.y - minLeftBound.y;
            Vector3 newPos = minLeftBound + (hitPoints[0] - minLeftBound) * 0.5f;
            newPos.y = segmentTransform.position.y;
            newPos.z = segmentTransform.position.z;

            float rightStackScaleX = Mathf.Abs(hitPoints[0].x - segmentToCut.bounds.max.x);
            Vector3 maxMiddleBound = segmentToCut.bounds.min;
            maxMiddleBound.y += segmentTransform.localScale.y * 0.5f;
            maxMiddleBound.x = segmentToCut.bounds.max.x;
            Vector3 rightStackPos = hitPoints[0] + (maxMiddleBound - hitPoints[0]) * 0.5f;
            rightStackPos.y = segmentTransform.position.y;
            rightStackPos.z = segmentTransform.position.z;
            Vector3 rightStackScale = new Vector3(rightStackScaleX, segmentTransform.localScale.y, segmentTransform.localScale.z);

            new SegmentRuin(rightStackPos, rightStackScale, segmentToCut.material, _gameAssets);

            segmentTransform.localScale = newScale;
            segmentTransform.position = newPos;
        }

        private void CutSegmentFromLeftSide(MeshRenderer segmentToCut, Vector3[] hitPoints)
        {
            Transform segmentTransform = segmentToCut.transform;

            float newScaleX = segmentToCut.bounds.max.x - hitPoints[0].x;
            Vector3 newScale = new Vector3(newScaleX, segmentTransform.localScale.y, segmentTransform.localScale.z);
            Vector3 minRightBound = segmentToCut.bounds.min;
            minRightBound.x = segmentToCut.bounds.max.x;
            Vector3 newPos = hitPoints[0] + (minRightBound - hitPoints[0]) * 0.5f;
            newPos.y = segmentTransform.position.y;
            newPos.z = segmentTransform.position.z;

            float leftStackScaleX = Mathf.Abs(hitPoints[0].x - segmentToCut.bounds.min.x);
            Vector3 minMiddleBound = segmentToCut.bounds.min;
            minMiddleBound.y += segmentTransform.localScale.y * 0.5f;
            Vector3 leftStackPos = hitPoints[0] + (minMiddleBound - hitPoints[0]) * 0.5f;
            leftStackPos.y = segmentTransform.position.y;
            leftStackPos.z = segmentTransform.position.z;
            Vector3 leftStackScale = new Vector3(leftStackScaleX, segmentTransform.localScale.y, segmentTransform.localScale.z);

            new SegmentRuin(leftStackPos, leftStackScale, segmentToCut.material, _gameAssets);

            segmentTransform.localScale = newScale;
            segmentTransform.position = newPos;
        }
    }
}