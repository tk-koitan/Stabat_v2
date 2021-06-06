using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;


namespace Koitan
{
    public class CharaColorChanger : MonoBehaviour
    {
        [SerializeField]
        GameObject mesh;
        [SerializeField]
        CharaLibrarySets charaColorSets;
        [SerializeField]
        ColorSets outlineSets;

        public void ChangeColor(int charaColorIndex, int outlineColorIndex)
        {
            SpriteLibrary sl = mesh.transform.Find("body").GetComponent<SpriteLibrary>();
            sl.spriteLibraryAsset = charaColorSets.librarys[charaColorIndex];
            foreach (SpriteRenderer sr in mesh.transform.Find("outline").GetComponentsInChildren<SpriteRenderer>())
            {
                sr.color = outlineSets.colors[outlineColorIndex];
            }
        }
    }
}
