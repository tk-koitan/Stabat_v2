using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

namespace Koitan
{
    [CreateAssetMenu(
        fileName = "CharaLibrarySetsData",
        menuName = "ScriptableObject/CharaLibrarySetsData",
        order = 0)
    ]
    public class CharaLibrarySets : ScriptableObject
    {
        public SpriteLibraryAsset[] librarys;
    }
}