using System.Collections.Generic;
using UnityEngine;

namespace GameGuruCaseTwo.Utilities.Physic
{
    public static class PhysicUtility
    {
        private static Ray _tempRay;
        private static RaycastHit _tempRaycastHit;

        public static T GetInteractableOnRayPoint<T>(Vector3 position, LayerMask layerMask)
        {
            _tempRay = Camera.main.ScreenPointToRay(position);
            if (Physics.Raycast(_tempRay, out _tempRaycastHit, float.MaxValue, layerMask))
                return _tempRaycastHit.collider.gameObject.GetComponent<T>();
            else return default;
        }

        public static Vector3[] GetHitPoints(Vector3[] castPoints, LayerMask layerMask)
        {
            List<Vector3> hitPoints = new List<Vector3>();

            if (Physics.Raycast(castPoints[0], Vector3.forward, out RaycastHit hit1, 1f, layerMask))
            {
                hitPoints.Add(hit1.point);
            }

            if (Physics.Raycast(castPoints[1], Vector3.forward, out RaycastHit hit2, 1f, layerMask))
            {
                hitPoints.Add(hit2.point);
            }

            return hitPoints.ToArray();
        }
    }
}