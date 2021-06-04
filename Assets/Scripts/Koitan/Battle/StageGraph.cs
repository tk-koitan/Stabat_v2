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
        //�ŒZ�����אڍs��
        [SerializeField]
        float[] distArray;
        //�o�H�����p
        [SerializeField]
        int[] prevArray;

        // ������
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
        /// ���ǂ蒅���Ȃ��ꍇ��null��Ԃ��܂�
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<int> GetPathIndexes(int startIndex, int endIndex)
        {
            if (startIndex < 0 || startIndex >= nodeList.Count || endIndex < 0 || endIndex >= nodeList.Count) return null;
            // ���ǂ蒅���Ȃ��̂Ŕ�����
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
        /// �n�_�ƏI�_����p�X�����߂܂�
        /// ���ǂ蒅���Ȃ��ꍇ��null��Ԃ��܂�
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
        /// �O���t�͈̔͊O�̏ꍇnull��Ԃ��܂�
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
        /// �ߕӂ̐N���\�ȃm�[�h��Ԃ��܂�
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="isHere">���ݒn�̃m�[�h�����ʂɊ܂߂邩</param>
        /// <param name="isFrom">isFrom ? �ߕӂ̃m�[�h -> ���ݒn : ���ݒn -> �ߕӂ̃m�[�h</param>
        /// <returns></returns>
        public List<StageNode> GetNearNodes(Vector3 pos, bool isHere, bool isFrom)
        {
            List<StageNode> nearNodes = new List<StageNode>();
            //���ׂ�͈͂��v�Z����
            int xl = (int)(perLength / width * column) + 1;
            int yl = (int)(perLength / height * row) + 1;
            int i = (int)((pos.x - lowerLeft.x) / width * column);
            int j = (int)((pos.y - lowerLeft.y) / height * row);
            //���C���[�}�X�N
            int layerMask = LayerMask.GetMask("Static Environment");
            for (int y = j - yl; y <= j + yl; y++)
            {
                for (int x = i - xl; x <= i + xl; x++)
                {
                    if (x < 0 || x >= column || y < 0 || y >= row) continue;
                    if (!isHere && x == i && y == j) continue;
                    StageNode endNode = GetNode(x, y);
                    //�����Ȃ甲����
                    if (endNode == null) continue;
                    //���C���΂��Đi���\�����ׂ�
                    //�Ȃ����ł��Ȃ�
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
                        //����̍�
                        if (hit.collider.usedByEffector)
                        {
                            //���ςŔ���
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
        /// �ߕӂ̍ł��߂��m�[�h��Ԃ��܂�
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="isFrom">isFrom ? �ߕӂ̃m�[�h -> ���ݒn : ���ݒn -> �ߕӂ̃m�[�h</param>
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

            //���_�𐶐�
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
            //�ߖT������
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
            //������
            nodeMatrix = new StageNode[column * row];
            nodeList.Clear();

            if (column <= 0 || row <= 0) return;
            float w = width / column;
            float h = height / row;

            //���݂̃m�[�h���폜
            foreach (StageNode node in transform.GetComponentsInChildren<StageNode>())
            {
                DestroyImmediate(node.gameObject);
            }

            //���C���[�}�X�N
            int layerMask = LayerMask.GetMask("Static Environment");

            //���_�𐶐�
            //�m�[�h�̒ʂ��ԍ�
            int nodeNumber = 0;
            // �R���C�_�[���Ƃɏ�����ς���
            // �Ƃ肠����BoxCollider2D�݂̂ɑΉ�����
            foreach (BoxCollider2D collider in FindObjectsOfType(typeof(BoxCollider2D)))
            {
                // �V�[����ɑ��݂���I�u�W�F�N�g�Ȃ�Ώ���.
                if (collider.gameObject.activeInHierarchy)
                {
                    if (collider.gameObject.layer == LayerMask.NameToLayer("Static Environment"))
                    {
                        // �l���̍��W�̌v�Z
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
                            //�Ԃ������甲����
                            if (hit) continue;
                            GameObject nodeObj = Instantiate(nodePrefab);
                            nodeObj.transform.position = rayOrigin;
                            nodeObj.transform.SetParent(transform);
                            nodeObj.name = $"{collider.gameObject.name}Node{nodeNumber:D2}";
                            StageNode entryNode = nodeObj.GetComponent<StageNode>();
                            StageNode firstNode = GetNode(nodeObj.transform.position);
                            if (firstNode == null)
                            {
                                //�͈͊O�����ׂ�
                                if (SetNode(nodeObj.transform.position, entryNode))
                                {
                                    //�ʂ��ԍ�
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
                                //��q�������ꍇ���W�̕��ς��Ƃ�
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
                // �V�[����ɑ��݂���I�u�W�F�N�g�Ȃ�Ώ���.
                if (collider.gameObject.activeInHierarchy)
                {
                    // GameObject�̖��O��\��.
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
                                //�ǂ̒��ɂ����甲����
                                if (hit.distance == 0) continue;
                                GameObject nodeObj = Instantiate(nodePrefab);
                                //������������
                                nodeObj.transform.position = hit.point + Vector2.up * 0.01f;
                                nodeObj.transform.SetParent(transform);
                                nodeObj.name = $"{collider.gameObject.name}Node{nodeNumber:D2}";
                                StageNode entryNode = nodeObj.GetComponent<StageNode>();
                                StageNode firstNode = GetNode(nodeObj.transform.position);
                                if (firstNode == null)
                                {
                                    //�͈͊O�����ׂ�
                                    if (SetNode(nodeObj.transform.position, entryNode))
                                    {
                                        //�ʂ��ԍ�
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
                                    //��q�������ꍇ���W�̕��ς��Ƃ�
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
            //�ߖT������
            //���ׂ�͈͂��v�Z����
            /*
            int xl = (int)(perLength / w) + 1;
            int yl = (int)(perLength / h) + 1;
            for (int j = 0; j < row; j++)
            {
                for (int i = 0; i < column; i++)
                {
                    StageNode startNode = GetNode(i, j);
                    //�����Ȃ甲����
                    if (startNode == null) continue;
                    for (int y = j - yl; y <= j + yl; y++)
                    {
                        for (int x = i - xl; x <= i + xl; x++)
                        {
                            if (x < 0 || x >= column || y < 0 || y >= row || (x == i && y == j)) continue;
                            StageNode endNode = GetNode(x, y);
                            //�����Ȃ甲����
                            if (endNode == null) continue;
                            //���C���΂��Đi���\�����ׂ�
                            //�Ȃ����ł��Ȃ�
                            RaycastHit2D hit;
                            hit = Physics2D.Linecast(startNode.transform.position, endNode.transform.position, layerMask);
                            //Physics2D.Linecast()
                            if (hit)
                            {
                                //����̍�
                                if (hit.collider.usedByEffector)
                                {
                                    //Debug.Log($"{i}, {j}, {hit.collider.transform.up}, {Vector3.Dot(endNode.transform.position - startNode.transform.position, hit.collider.transform.up)}");
                                    //hit.collider.transform.up
                                    //���ςŔ���
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

            //�ߖT���Ȃ�

            foreach (StageNode startNode in nodeList)
            {
                startNode.endPoints = GetNearNodes(startNode.transform.position, false, false);
            }

            EditorUtility.SetDirty(this);
            // �������X�V
            foreach (StageNode node in nodeList)
            {
                node.CalcDist();
            }
            // ���[�V�����t���C�h�@
            distArray = new float[nodeList.Count * nodeList.Count];
            prevArray = new int[nodeList.Count * nodeList.Count];
            int V = nodeList.Count;
            // ������
            for (int i = 0; i < V; i++)
            {
                // INF�Ŗ��߂�
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
            // �o�R�_
            for (int k = 0; k < V; k++)
            {
                // �n�_
                for (int i = 0; i < V; i++)
                {
                    // �I�_
                    for (int j = 0; j < V; j++)
                    {

                        // �n�_ => �o�R�_ + �o�R�_ => �I�_�̋������n�_ => �I�_�̋����������������`�F�b�N
                        float dist = GetDist(i, k) + GetDist(k, j);
                        if (dist < GetDist(i, j))
                        {
                            // �n�_ => �I�_�̋������X�V
                            SetDist(i, j, dist);

                            // �I�_�������X�V�������_��ۑ�
                            SetPrev(i, j, GetPrev(k, j));
                        }
                    }
                }
            }
        }
#endif
    }
}

