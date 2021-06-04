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
            //レイヤーマスク作成
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
                    //新しいゲームオブジェクトを作成、その事をUndoに記録
                    Undo.RegisterCreatedObjectUndo(newGameObject, "Create New GameObject");
                }
                else
                {
                    //Debug.Log("Nohit");
                }
            }
            */
            // typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
            foreach (Collider2D collider in UnityEngine.Object.FindObjectsOfType(typeof(Collider2D)))
            {
                // シーン上に存在するオブジェクトならば処理.
                if (collider.gameObject.activeInHierarchy)
                {
                    // GameObjectの名前を表示.
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
                                //壁の中にいたら抜ける
                                if (hit.distance == 0) continue;
                                GameObject newGameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                                newGameObject.transform.position = hit.point;
                                //新しいゲームオブジェクトを作成、その事をUndoに記録
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
