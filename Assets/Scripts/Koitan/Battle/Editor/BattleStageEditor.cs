using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Koitan
{
    public class BattleStageEditor : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        [MenuItem("Stabat/Create")]
        public static void Create()
        {
            //���C���[�}�X�N�쐬
            int layerMask = LayerMask.GetMask("Static Environment");
            //Debug.Log($"layerMask = {layerMask}");

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Editor/Node.prefab");
            /*
            for (int i = 0; i < 10; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(i, 0), Vector2.down, 100f, layerMask);
                if (hit)
                {
                    GameObject newGameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                    newGameObject.transform.position = hit.point;
                    //�V�����Q�[���I�u�W�F�N�g���쐬�A���̎���Undo�ɋL�^
                    Undo.RegisterCreatedObjectUndo(newGameObject, "Create New GameObject");
                }
                else
                {
                    //Debug.Log("Nohit");
                }
            }
            */
            // type�Ŏw�肵���^�̑S�ẴI�u�W�F�N�g��z��Ŏ擾��,���̗v�f�����J��Ԃ�.
            foreach (Collider2D collider in UnityEngine.Object.FindObjectsOfType(typeof(Collider2D)))
            {
                // �V�[����ɑ��݂���I�u�W�F�N�g�Ȃ�Ώ���.
                if (collider.gameObject.activeInHierarchy)
                {
                    // GameObject�̖��O��\��.
                    if (collider.gameObject.layer == LayerMask.NameToLayer("Static Environment"))
                    {
                        Vector3 minToMax = collider.bounds.max - collider.bounds.min;
                        float perLength = 4f;
                        int splitNum = (int)(minToMax.magnitude / perLength);
                        //Debug.Log($"{collider.gameObject.name} : {collider.gameObject.layer}");
                        for (int i = 0; i <= splitNum; i++)
                        {
                            RaycastHit2D hit;
                            Vector3 rayOrigin = collider.bounds.min + (collider.bounds.max - collider.bounds.min) / splitNum * i + Vector3.up * 1f;
                            hit = Physics2D.Raycast(rayOrigin, Vector2.down, 2f, layerMask);
                            if (hit)
                            {
                                //�ǂ̒��ɂ����甲����
                                if (hit.distance == 0) continue;
                                GameObject newGameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                                newGameObject.transform.position = hit.point;
                                //�V�����Q�[���I�u�W�F�N�g���쐬�A���̎���Undo�ɋL�^
                                Undo.RegisterCreatedObjectUndo(newGameObject, "Create New GameObject");
                            }
                        }
                    }
                    //Debug.Log($"{collider.gameObject.name} : {collider.gameObject.layer}");

                }
            }
        }
    }
}
