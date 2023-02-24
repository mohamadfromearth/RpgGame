using System;
using UnityEngine;

namespace RPG.Control
{
    public class PathPatrol : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWayPoint(i),1f);
                Gizmos.DrawLine(GetWayPoint(i),GetWayPoint(j));
            }
        }

        public int GetNextIndex(int i)
        {
            return i + 1 == transform.childCount ? 0 : i + 1;
        }

        public Vector3 GetWayPoint(int index)
        {
            return transform.GetChild(index).transform.position;
        }
    }
   
}