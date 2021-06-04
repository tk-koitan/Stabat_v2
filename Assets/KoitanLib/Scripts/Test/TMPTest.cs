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
            // ���b�V���X�V
            textMeshPro.ForceMeshUpdate();

            //�e�L�X�g���b�V���v���̏��
            var textInfo = textMeshPro.textInfo;

            //�e�L�X�g�����[���ł���Ε\�����Ȃ�
            if (textInfo.characterCount == 0)
            {
                return;
            }


            //1��������loop
            for (int index = 0; index < textInfo.characterCount; index++)
            {
                //1�����P�ʂ̏��
                var charaInfo = textInfo.characterInfo[index];

                //�W�I���g���Ȃ������̓X�L�b�v
                if (!charaInfo.isVisible)
                {
                    continue;
                }

                //Material�Q�Ƃ��Ă���index�擾
                int materialIndex = charaInfo.materialReferenceIndex;

                //���_�Q�Ƃ��Ă���index�擾
                int vertexIndex = charaInfo.vertexIndex;

                //Normal�\��
                Vector3[] destNormals = textInfo.meshInfo[materialIndex].normals;
                //Debug.Log(string.Join(", ", destNormals));
                destNormals[vertexIndex + 0] = new Vector3(-1, -1, 0);
                destNormals[vertexIndex + 1] = new Vector3(-1, 1, 0);
                destNormals[vertexIndex + 2] = new Vector3(1, 1, 0);
                destNormals[vertexIndex + 3] = new Vector3(1, -1, 0);
                //Debug.Log(string.Join(", ", destNormals));
            }

            //�W�I���g���X�V
            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                //���b�V�������A���ۂ̃��b�V�����_�֔��f
                textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                textInfo.meshInfo[i].mesh.normals = textInfo.meshInfo[i].normals;
                textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }
        }
    }
}
