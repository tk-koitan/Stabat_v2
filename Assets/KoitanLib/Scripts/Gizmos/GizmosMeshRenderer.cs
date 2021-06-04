using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoitanLib
{
    public class GizmosMeshRenderer : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            var preColor = Gizmos.color;
            var preMatrix = Gizmos.matrix;
            Gizmos.color = Color.blue;
            Gizmos.matrix = transform.localToWorldMatrix;

            // –@ü•ûŒü‚Éƒ‰ƒCƒ“‚ğ•`‰æ
            var mesh = GetComponent<MeshFilter>().sharedMesh;
            for (int i = 0; i < mesh.normals.Length; i++)
            {
                var from = mesh.vertices[i];
                var to = from + mesh.normals[i];
                Gizmos.DrawLine(from, to);
            }

            Gizmos.color = preColor;
            Gizmos.matrix = preMatrix;
        }
    }
}
