using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using KoitanLib;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Koitan
{
    public class StageGraph : MonoBehaviour
    {
        public static StageGraph instance;
        [SerializeField]
        Vector3 lowerLeft;
        [SerializeField]
        float width;
        [SerializeField]
        float height;
        [SerializeField]
        int column;
        [SerializeField]
        int row;
        [SerializeField]
        float perLength = 4f;
        [SerializeField]
        GameObject nodePrefab;
        [SerializeField]
        StageNode[] nodeMatrix;
        [SerializeField]
        List<StageNode> nodeList = new List<StageNode>();
        [SerializeField]
        int startNum;
        [SerializeField]
        int endNum;
        [SerializeField]
        Vector3 startPos;
        [SerializeField]
        Vector3 endPos;
        //最短距離隣接行列
        [SerializeField]
        float[] distArray;
        //経路復元用
        [SerializeField]
        int[] prevArray;

        // 無限大
        const float INF = 10000000;
        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            GizmosExtensions2D.DrawGrid2D(lowerLeft, width, height, column, row);

            //GizmosExtensions2D.DrawRect2D(Vector3.zero, width, height);
            /*
            Vector3 mousePosition = Event.current.mousePosition;
            mousePosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePosition.y;
            mousePosition = SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(mousePosition, Vector3.up);
            if (hit)
            {
                GizmosExtensions2D.DrawArrow2D(mousePosition, hit.point);
            }
            else
            {
                GizmosExtensions2D.DrawArrow2D(mousePosition, mousePosition + Vector3.up);
            }
            */
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(startPos, 0.5f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(endPos, 0.5f);
            Gizmos.color = Color.blue;
            /*
            StageNode sNode = GetNearestNode(startPos, false);
            StageNode eNode = GetNearestNode(endPos, true);
            startNum = sNode == null ? -1 : sNode.nodeNumber;
            endNum = eNode == null ? -1 : eNode.nodeNumber;
            if (startNum < 0 || startNum >= nodeList.Count || endNum < 0 || endNum >= nodeList.Count) return;
            var path = GetPathIndexes(startNum, endNum);
            if (path == null) return;
            for (int i = 0; i < path.Count - 1; i++)
            {
                GizmosExtensions2D.DrawRectArrow2D(nodeList[path[i]].transform.position, nodeList[path[i + 1]].transform.position, 0.25f);
            }
            GizmosExtensions2D.DrawRectArrow2D(startPos, sNode.transform.position, 0.25f);
            GizmosExtensions2D.DrawRectArrow2D(eNode.transform.position, endPos, 0.25f);
            */
            var path = GetPath(startPos, endPos);
            if (path == null) return;
            for (int i = 0; i < path.Count - 1; i++)
            {
                //GizmosExtensions2D.DrawRectArrow2D(path[i], path[i + 1], 0.25f);
                GizmosExtensions2D.DrawArrow2D(path[i], path[i + 1]);
            }
        }

        /// <summary>
        /// たどり着けない場合はnullを返します
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<int> GetPathIndexes(int startIndex, int endIndex)
        {
            if (startIndex < 0 || startIndex >= nodeList.Count || endIndex < 0 || endIndex >= nodeList.Count) return null;
            // たどり着けないので抜ける
            if (GetDist(startIndex, endIndex) == INF) return null;
            List<int> path = new List<int>();
            path.Add(endIndex);
            int nextIndex = endIndex;
            while (nextIndex != startIndex)
            {
                nextIndex = GetPrev(startIndex, nextIndex);
                path.Add(nextIndex);
            }
            path.Reverse();
            return path;
        }

        /// <summary>
        /// 始点と終点からパスを求めます
        /// たどり着けない場合はnullを返します
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        public List<Vector3> GetPath(Vector3 startPos, Vector3 endPos)
        {
            StageNode sNode = GetNearestNode(startPos, false);
            StageNode eNode = GetNearestNode(endPos, true);
            int sIndex = sNode == null ? -1 : sNode.nodeNumber;
            int eIndex = eNode == null ? -1 : eNode.nodeNumber;
            if (sIndex < 0 || sIndex >= nodeList.Count || eIndex < 0 || eIndex >= nodeList.Count) return null;
            List<int> pathIndexes = GetPathIndexes(sIndex, eIndex);
            if (pathIndexes == null) return null;
            List<Vector3> path = new List<Vector3>();
            path.Add(startPos);
            for (int i = 0; i < pathIndexes.Count; i++)
            {
                path.Add(nodeList[pathIndexes[i]].transform.position);
            }
            path.Add(endPos);
            return path;
        }

        /// <summary>
        /// グラフの範囲外の場合nullを返します
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public StageNode GetNode(int i, int j)
        {
            if (i < 0 || i >= column || j < 0 || j >= row) return null;
            return nodeMatrix[i + j * column];
        }

        public StageNode GetNode(Vector3 pos)
        {
            int x = (int)((pos.x - lowerLeft.x) / width * column);
            int y = (int)((pos.y - lowerLeft.y) / height * row);
            return GetNode(x, y);
        }

        public bool SetNode(int i, int j, StageNode node)
        {
            if (i < 0 || i >= column || j < 0 || j >= row) return false;
            nodeMatrix[i + j * column] = node;
            return true;
        }

        public bool SetNode(Vector3 pos, StageNode node)
        {
            int i = (int)((pos.x - lowerLeft.x) / width * column);
            int j = (int)((pos.y - lowerLeft.y) / height * row);
            return SetNode(i, j, node);
        }

        public float GetDist(int i, int j)
        {
            return distArray[i * nodeList.Count + j];
        }

        public void SetDist(int i, int j, float dist)
        {
            distArray[i * nodeList.Count + j] = dist;
        }
        public int GetPrev(int i, int j)
        {
            return prevArray[i * nodeList.Count + j];
        }

        public void SetPrev(int i, int j, int prev)
        {
            prevArray[i * nodeList.Count + j] = prev;
        }

        /// <summary>
        /// 近辺の侵入可能なノードを返します
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="isHere">現在地のノードを結果に含めるか</param>
        /// <param name="isFrom">isFrom ? 近辺のノード -> 現在地 : 現在地 -> 近辺のノード</param>
        /// <returns></returns>
        public List<StageNode> GetNearNodes(Vector3 pos, bool isHere, bool isFrom)
        {
            List<StageNode> nearNodes = new List<StageNode>();
            //調べる範囲を計算する
            int xl = (int)(perLength / width * column) + 1;
            int yl = (int)(perLength / height * row) + 1;
            int i = (int)((pos.x - lowerLeft.x) / width * column);
            int j = (int)((pos.y - lowerLeft.y) / height * row);
            //レイヤーマスク
            int layerMask = LayerMask.GetMask("Static Environment");
            for (int y = j - yl; y <= j + yl; y++)
            {
                for (int x = i - xl; x <= i + xl; x++)
                {
                    if (x < 0 || x >= column || y < 0 || y >= row) continue;
                    if (!isHere && x == i && y == j) continue;
                    StageNode endNode = GetNode(x, y);
                    //無いなら抜ける
                    if (endNode == null) continue;
                    //レイを飛ばして進入可能か調べる
                    //なぜかできない
                    Vector3 sPos, ePos;
                    if (isFrom)
                    {
                        sPos = endNode.transform.position;
                        ePos = pos;
                    }
                    else
                    {
                        sPos = pos;
                        ePos = endNode.transform.position;
                    }
                    RaycastHit2D hit;
                    hit = Physics2D.Linecast(sPos, ePos, layerMask);
                    if (hit)
                    {
                        //苦肉の策
                        if (hit.collider.usedByEffector)
                        {
                            //内積で判定
                            if (Vector3.Dot(ePos - sPos, hit.collider.transform.up) > 0)
                            {
                                nearNodes.Add(endNode);
                            }
                        }
                    }
                    else
                    {
                        nearNodes.Add(endNode);
                    }
                }
            }
            return nearNodes;
        }

        /// <summary>
        /// 近辺の最も近いノードを返します
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="isFrom">isFrom ? 近辺のノード -> 現在地 : 現在地 -> 近辺のノード</param>
        /// <returns></returns>
        public StageNode GetNearestNode(Vector3 pos, bool isFrom)
        {
            StageNode resNode = null;
            foreach (StageNode node in GetNearNodes(pos, true, isFrom))
            {
                if (resNode == null)
                {
                    resNode = node;
                }
                else
                {
                    if (Vector3.Distance(pos, node.transform.position) < Vector3.Distance(pos, resNode.transform.position))
                    {
                        resNode = node;
                    }
                }
            }
            return resNode;
        }


#if UNITY_EDITOR
        [ContextMenu("Create Graph With Gird")]
        void CreateGraphWithGrid()
        {
            nodeMatrix = new StageNode[column * row];

            if (column <= 0 || row <= 0) return;
            float w = width / column;
            float h = height / row;

            //頂点を生成
            for (int j = 0; j < row; j++)
            {
                for (int i = 0; i < column; i++)
                {
                    Vector3 pos = lowerLeft + new Vector3(w * (i + 0.5f), h * (j + 0.5f));
                    GameObject nodeObj = Instantiate(nodePrefab);
                    nodeObj.transform.position = pos;
                    nodeObj.transform.SetParent(transform);
                    nodeObj.name = $"Node{i:D2}{j:D2}";
                    StageNode node = nodeObj.GetComponent<StageNode>();
                    SetNode(i, j, node);
                    Undo.RegisterCreatedObjectUndo(nodeObj, "Create Node");
                }
            }
            //近傍を結ぶ
            for (int j = 0; j < row; j++)
            {
                for (int i = 0; i < column; i++)
                {
                    for (int y = j - 1; y <= j + 1; y++)
                    {
                        for (int x = i - 1; x <= i + 1; x++)
                        {
                            if (x < 0 || x >= column || y < 0 || y >= row || (x == i && y == j)) continue;
                            GetNode(i, j).endPoints.Add(GetNode(x, y));
                        }
                    }
                }
            }
            EditorUtility.SetDirty(this);
        }

        [ContextMenu("Create Graph With Collider")]
        void CreateGraphWithCollider()
        {
            //初期化
            nodeMatrix = new StageNode[column * row];
            nodeList.Clear();

            if (column <= 0 || row <= 0) return;
            float w = width / column;
            float h = height / row;

            //現在のノードを削除
            foreach (StageNode node in transform.GetComponentsInChildren<StageNode>())
            {
                DestroyImmediate(node.gameObject);
            }

            //レイヤーマスク
            int layerMask = LayerMask.GetMask("Static Environment");

            //頂点を生成
            //ノードの通し番号
            int nodeNumber = 0;
            // コライダーごとに処理を変える
            // とりあえずBoxCollider2Dのみに対応する
            foreach (BoxCollider2D collider in FindObjectsOfType(typeof(BoxCollider2D)))
            {
                // シーン上に存在するオブジェクトならば処理.
                if (collider.gameObject.activeInHierarchy)
                {
                    if (collider.gameObject.layer == LayerMask.NameToLayer("Static Environment"))
                    {
                        // 四隅の座標の計算
                        Vector3 ur = collider.transform.position + (collider.transform.right * (collider.offset.x + collider.size.x / 2)) * collider.transform.localScale.x + (collider.transform.up * (collider.offset.y + collider.size.y / 2)) * collider.transform.localScale.y;
                        Vector3 ul = collider.transform.position - (collider.transform.right * (collider.offset.x + collider.size.x / 2)) * collider.transform.localScale.x + (collider.transform.up * (collider.offset.y + collider.size.y / 2)) * collider.transform.localScale.y;
                        Vector3 lr = collider.transform.position + (collider.transform.right * (collider.offset.x + collider.size.x / 2)) * collider.transform.localScale.x - (collider.transform.up * (collider.offset.y + collider.size.y / 2)) * collider.transform.localScale.y;
                        Vector3 ll = collider.transform.position - (collider.transform.right * (collider.offset.x + collider.size.x / 2)) * collider.transform.localScale.x - (collider.transform.up * (collider.offset.y + collider.size.y / 2)) * collider.transform.localScale.y;
                        //Debug.Log($"{collider.gameObject.name}, {ur}, {ul}, {lr}, {ll}");
                        Vector3 minToMax = ur - ul;
                        int splitNum = (int)(minToMax.magnitude / perLength);
                        if (splitNum == 0) continue;
                        //Debug.Log($"{collider.gameObject.name} : {collider.gameObject.layer}");
                        for (int i = -1; i <= splitNum + 1; i++)
                        {
                            RaycastHit2D hit;
                            Vector3 rayOrigin = ul + i * minToMax / splitNum + collider.transform.up * 1f;
                            hit = Physics2D.Raycast(rayOrigin, -collider.transform.up, 0.9f, layerMask);
                            //ぶつかったら抜ける
                            if (hit) continue;
                            GameObject nodeObj = Instantiate(nodePrefab);
                            nodeObj.transform.position = rayOrigin;
                            nodeObj.transform.SetParent(transform);
                            nodeObj.name = $"{collider.gameObject.name}Node{nodeNumber:D2}";
                            StageNode entryNode = nodeObj.GetComponent<StageNode>();
                            StageNode firstNode = GetNode(nodeObj.transform.position);
                            if (firstNode == null)
                            {
                                //範囲外か調べる
                                if (SetNode(nodeObj.transform.position, entryNode))
                                {
                                    //通し番号
                                    entryNode.nodeNumber = nodeNumber;
                                    nodeNumber++;
                                    nodeList.Add(entryNode);
                                    Undo.RegisterCreatedObjectUndo(nodeObj, "Create Node");
                                }
                                else
                                {
                                    DestroyImmediate(nodeObj);
                                }
                            }
                            else
                            {
                                //先客がいた場合座標の平均をとる
                                Vector3 midPoint = (firstNode.transform.position + entryNode.transform.position) / 2f;
                                firstNode.transform.position = midPoint;
                                DestroyImmediate(nodeObj);
                            }
                        }
                    }
                }
            }
            /*
            foreach (Collider2D collider in UnityEngine.Object.FindObjectsOfType(typeof(Collider2D)))
            {
                // シーン上に存在するオブジェクトならば処理.
                if (collider.gameObject.activeInHierarchy)
                {
                    // GameObjectの名前を表示.
                    if (collider.gameObject.layer == LayerMask.NameToLayer("Static Environment"))
                    {
                        Vector3 minToMax = collider.bounds.max - collider.bounds.min;
                        int splitNum = (int)(minToMax.magnitude / perLength);
                        //Debug.Log($"{collider.gameObject.name} : {collider.gameObject.layer}");
                        for (int i = 0; i <= splitNum; i++)
                        {
                            RaycastHit2D hit;
                            Vector3 rayOrigin = collider.bounds.min + i * minToMax / splitNum + Vector3.up * 1f;
                            hit = Physics2D.Raycast(rayOrigin, Vector2.down, 2f, layerMask);
                            if (hit)
                            {
                                //壁の中にいたら抜ける
                                if (hit.distance == 0) continue;
                                GameObject nodeObj = Instantiate(nodePrefab);
                                //少し浮かせる
                                nodeObj.transform.position = hit.point + Vector2.up * 0.01f;
                                nodeObj.transform.SetParent(transform);
                                nodeObj.name = $"{collider.gameObject.name}Node{nodeNumber:D2}";
                                StageNode entryNode = nodeObj.GetComponent<StageNode>();
                                StageNode firstNode = GetNode(nodeObj.transform.position);
                                if (firstNode == null)
                                {
                                    //範囲外か調べる
                                    if (SetNode(nodeObj.transform.position, entryNode))
                                    {
                                        //通し番号
                                        entryNode.nodeNumber = nodeNumber;
                                        nodeNumber++;
                                        nodeList.Add(entryNode);
                                        Undo.RegisterCreatedObjectUndo(nodeObj, "Create Node");
                                    }
                                    else
                                    {
                                        DestroyImmediate(nodeObj);
                                    }
                                }
                                else
                                {
                                    //先客がいた場合座標の平均をとる
                                    Vector3 midPoint = (firstNode.transform.position + entryNode.transform.position) / 2f;
                                    firstNode.transform.position = midPoint;
                                    DestroyImmediate(nodeObj);
                                }
                            }
                        }
                    }
                    //Debug.Log($"{collider.gameObject.name} : {collider.gameObject.layer}");

                }
            }
            */
            //近傍を結ぶ
            //調べる範囲を計算する
            /*
            int xl = (int)(perLength / w) + 1;
            int yl = (int)(perLength / h) + 1;
            for (int j = 0; j < row; j++)
            {
                for (int i = 0; i < column; i++)
                {
                    StageNode startNode = GetNode(i, j);
                    //無いなら抜ける
                    if (startNode == null) continue;
                    for (int y = j - yl; y <= j + yl; y++)
                    {
                        for (int x = i - xl; x <= i + xl; x++)
                        {
                            if (x < 0 || x >= column || y < 0 || y >= row || (x == i && y == j)) continue;
                            StageNode endNode = GetNode(x, y);
                            //無いなら抜ける
                            if (endNode == null) continue;
                            //レイを飛ばして進入可能か調べる
                            //なぜかできない
                            RaycastHit2D hit;
                            hit = Physics2D.Linecast(startNode.transform.position, endNode.transform.position, layerMask);
                            //Physics2D.Linecast()
                            if (hit)
                            {
                                //苦肉の策
                                if (hit.collider.usedByEffector)
                                {
                                    //Debug.Log($"{i}, {j}, {hit.collider.transform.up}, {Vector3.Dot(endNode.transform.position - startNode.transform.position, hit.collider.transform.up)}");
                                    //hit.collider.transform.up
                                    //内積で判定
                                    if (Vector3.Dot(endNode.transform.position - startNode.transform.position, hit.collider.transform.up) > 0)
                                    {
                                        startNode.endPoints.Add(endNode);
                                    }
                                }
                            }
                            else
                            {
                                startNode.endPoints.Add(endNode);
                            }
                        }
                    }
                }
            }
            */

            //近傍をつなぐ

            foreach (StageNode startNode in nodeList)
            {
                startNode.endPoints = GetNearNodes(startNode.transform.position, false, false);
            }

            EditorUtility.SetDirty(this);
            // 距離を更新
            foreach (StageNode node in nodeList)
            {
                node.CalcDist();
            }
            // ワーシャルフロイド法
            distArray = new float[nodeList.Count * nodeList.Count];
            prevArray = new int[nodeList.Count * nodeList.Count];
            int V = nodeList.Count;
            // 初期化
            for (int i = 0; i < V; i++)
            {
                // INFで埋める
                for (int j = 0; j < V; j++)
                {
                    if (i == j)
                    {
                        SetDist(i, j, 0);
                    }
                    else
                    {
                        SetDist(i, j, INF);
                    }
                    SetPrev(i, j, i);
                }
                StageNode node = nodeList[i];
                for (int j = 0; j < node.endPoints.Count; j++)
                {
                    int num = node.endPoints[j].nodeNumber;
                    float dist = node.endDistances[j];
                    SetDist(i, num, dist);
                }
            }
            // 経由点
            for (int k = 0; k < V; k++)
            {
                // 始点
                for (int i = 0; i < V; i++)
                {
                    // 終点
                    for (int j = 0; j < V; j++)
                    {

                        // 始点 => 経由点 + 経由点 => 終点の距離が始点 => 終点の距離よりも小さいかチェック
                        float dist = GetDist(i, k) + GetDist(k, j);
                        if (dist < GetDist(i, j))
                        {
                            // 始点 => 終点の距離を更新
                            SetDist(i, j, dist);

                            // 終点距離を更新した頂点を保存
                            SetPrev(i, j, GetPrev(k, j));
                        }
                    }
                }
            }
        }
#endif
    }
}

