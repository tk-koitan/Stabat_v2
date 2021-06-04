using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace KoitanLib
{
    public class TMPTest : MonoBehaviour
    {
        private TextMeshPro textMeshPro;

        private void Awake()
        {
            textMeshPro = gameObject.GetComponent<TextMeshPro>();
            NormalUpdate();
        }

        void Update()
        {

        }

        [ContextMenu("NormalUpdate")]
        void NormalUpdate()
        {
            textMeshPro = gameObject.GetComponent<TextMeshPro>();
            // メッシュ更新
            textMeshPro.ForceMeshUpdate();

            //テキストメッシュプロの情報
            var textInfo = textMeshPro.textInfo;

            //テキスト数がゼロであれば表示しない
            if (textInfo.characterCount == 0)
            {
                return;
            }


            //1文字毎にloop
            for (int index = 0; index < textInfo.characterCount; index++)
            {
                //1文字単位の情報
                var charaInfo = textInfo.characterInfo[index];

                //ジオメトリない文字はスキップ
                if (!charaInfo.isVisible)
                {
                    continue;
                }

                //Material参照しているindex取得
                int materialIndex = charaInfo.materialReferenceIndex;

                //頂点参照しているindex取得
                int vertexIndex = charaInfo.vertexIndex;

                //Normal表示
                Vector3[] destNormals = textInfo.meshInfo[materialIndex].normals;
                //Debug.Log(string.Join(", ", destNormals));
                destNormals[vertexIndex + 0] = new Vector3(-1, -1, 0);
                destNormals[vertexIndex + 1] = new Vector3(-1, 1, 0);
                destNormals[vertexIndex + 2] = new Vector3(1, 1, 0);
                destNormals[vertexIndex + 3] = new Vector3(1, -1, 0);
                //Debug.Log(string.Join(", ", destNormals));
            }

            //ジオメトリ更新
            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                //メッシュ情報を、実際のメッシュ頂点へ反映
                textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                textInfo.meshInfo[i].mesh.normals = textInfo.meshInfo[i].normals;
                textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }
        }
    }
}
