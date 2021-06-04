using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;

namespace Koitan
{
    public class StageNode : MonoBehaviour
    {
        float radius = 0.5f;
        public List<StageNode> endPoints;
        public float[] endDistances;
        public int nodeNumber;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CalcDist()
        {
            endDistances = new float[endPoints.Count];
            for (int i = 0; i < endDistances.Length; i++)
            {
                endDistances[i] = Vector3.Distance(transform.position, endPoints[i].transform.position);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(transform.position, radius);
            Gizmos.color = Color.red;
            foreach (StageNode endPoint in endPoints)
            {
                Vector3 dir = (endPoint.transform.position - transform.position).normalized;
                GizmosExtensions2D.DrawArrow2D(transform.position + dir * radius, endPoint.transform.position - dir * radius);
            }
        }
    }
}
