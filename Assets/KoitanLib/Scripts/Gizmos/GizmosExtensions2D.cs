using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoitanLib
{
    public static class GizmosExtensions2D
    {
        public static void DrawWireArc2D(Vector3 center, float radius, float startAngle, float endAngle, int segments = 20)
        {
            /*
            var sceneCamera = UnityEditor.SceneView.currentDrawingSceneView.camera;
            var cameraDistance = Vector3.Distance(sceneCamera.transform.position, center);
            Debug.Log(cameraDistance);
            segments = (int)(1000 / cameraDistance);
            segments = Mathf.Clamp(segments, 0, 36);
            UnityEditor.Handles.ArrowCap(0, center, Quaternion.Euler(0,90,0), 1);
            */
            if (radius == 0 || segments == 0) return;
            Vector3 from = center + new Vector3(radius * Mathf.Cos(startAngle * Mathf.Deg2Rad), radius * Mathf.Sin(startAngle * Mathf.Deg2Rad));
            var step = (endAngle - startAngle) / segments;
            if (step == 0) return;
            for (float i = startAngle + step; i <= endAngle; i += step)
            {
                var to = center + new Vector3(radius * Mathf.Cos(i * Mathf.Deg2Rad), radius * Mathf.Sin(i * Mathf.Deg2Rad));
                Gizmos.DrawLine(from, to);
                from = to;
            }
        }

        public static void DrawWireCircle2D(Vector3 center, float radius, int segments = 20)
        {
            DrawWireArc2D(center, radius, 0, 360, segments);
        }

        public static void DrawArrow2D(Vector3 from, Vector3 to, float arrowHeadLength = 1f, float arrowHeadAngle = 20.0f)
        {
            Vector3 direction = (to - from).normalized;
            Gizmos.DrawLine(from, to);
            Gizmos.DrawRay(to, Quaternion.Euler(0, 0, 30) * -direction * arrowHeadLength);
            Gizmos.DrawRay(to, Quaternion.Euler(0, 0, -30) * -direction * arrowHeadLength);
        }

        public static void DrawWireArcArrow2D(Vector3 center, float radius, float startAngle, float endAngle, int segments = 20)
        {
            var step = (endAngle - startAngle) / segments;
            if (step == 0) return;
            DrawWireArc2D(center, radius, startAngle, endAngle - step, segments - 1);
            var from = center + new Vector3(radius * Mathf.Cos((endAngle - step) * Mathf.Deg2Rad), radius * Mathf.Sin((endAngle - step) * Mathf.Deg2Rad));
            var to = center + new Vector3(radius * Mathf.Cos(endAngle * Mathf.Deg2Rad), radius * Mathf.Sin(endAngle * Mathf.Deg2Rad));
            DrawArrow2D(from, to);
        }

        public static void DrawWireRect2D(Vector3 center, float width, float height, float angle = 0)
        {
            Vector3 upperRight = Quaternion.Euler(0, 0, angle) * new Vector3(width / 2, height / 2) + center;
            Vector3 upperLeft = Quaternion.Euler(0, 0, angle) * new Vector3(-width / 2, height / 2) + center;
            Vector3 lowerRight = Quaternion.Euler(0, 0, angle) * new Vector3(width / 2, -height / 2) + center;
            Vector3 lowerLeft = Quaternion.Euler(0, 0, angle) * new Vector3(-width / 2, -height / 2) + center;
            Gizmos.DrawLine(upperRight, upperLeft);
            Gizmos.DrawLine(upperLeft, lowerLeft);
            Gizmos.DrawLine(lowerLeft, lowerRight);
            Gizmos.DrawLine(lowerRight, upperRight);
        }

        public static void DrawGrid2D(Vector3 lowerLeft, float width, float height, int column, int row)
        {
            if (column <= 0 || row <= 0) return;
            float w = width / column;
            float h = height / row;

            for (int i = 0; i <= column; i++)
            {
                Gizmos.DrawLine(lowerLeft + new Vector3(w * i, 0), lowerLeft + new Vector3(w * i, height));
            }
            for (int i = 0; i <= row; i++)
            {
                Gizmos.DrawLine(lowerLeft + new Vector3(0, h * i), lowerLeft + new Vector3(width, h * i));
            }
        }

        public static void DrawRect2D(Vector3 center, float width, float height, float angle = 0)
        {
            Vector3 upperRight = Quaternion.Euler(0, 0, angle) * new Vector3(width / 2, height / 2) + center;
            Vector3 upperLeft = Quaternion.Euler(0, 0, angle) * new Vector3(-width / 2, height / 2) + center;
            Vector3 lowerRight = Quaternion.Euler(0, 0, angle) * new Vector3(width / 2, -height / 2) + center;
            Vector3 lowerLeft = Quaternion.Euler(0, 0, angle) * new Vector3(-width / 2, -height / 2) + center;
            Mesh mesh = new Mesh();
            Vector3[] vartices = { lowerLeft, upperLeft, upperRight, lowerRight };
            int[] triangles = { 0, 1, 2, 0, 2, 3 };
            mesh.vertices = vartices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            Gizmos.DrawMesh(mesh);
        }

        // ???????????????H???????g?p???~
        public static void DrawLine2D(Vector3 from, Vector3 to, float width = 0.1f)
        {
            Vector3 direction = (to - from).normalized;
            Vector3 normal = Quaternion.Euler(0, 0, 90) * direction.normalized;
            Vector3 upperRight = from + normal * width / 2;
            Vector3 upperLeft = from - normal * width / 2;
            Vector3 lowerRight = to + normal * width / 2;
            Vector3 lowerLeft = to - normal * width / 2;
            Mesh mesh = new Mesh();
            Vector3[] vartices = { lowerLeft, upperLeft, upperRight, lowerRight };
            int[] triangles = { 0, 1, 2, 2, 1, 0, 0, 2, 3, 3, 2, 0 };
            mesh.vertices = vartices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            Gizmos.DrawMesh(mesh);
        }

        // ???????????????H???????g?p???~
        public static void DrawRectArrow2D(Vector3 from, Vector3 to, float width = 0.1f, float arrowHeadLength = 1f, float arrowHeadAngle = 20.0f)
        {
            Vector3 direction = (to - from).normalized;
            DrawLine2D(from, to, width);
            DrawLine2D(to, to + Quaternion.Euler(0, 0, 30) * -direction * arrowHeadLength, width);
            DrawLine2D(to, to + Quaternion.Euler(0, 0, -30) * -direction * arrowHeadLength, width);
        }
    }
}
