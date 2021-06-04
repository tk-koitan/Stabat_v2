using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Koitan
{
    [CreateAssetMenu(
        fileName = "ColorSetsData",
        menuName = "ScriptableObject/ColorSetsData",
        order = 0)
    ]
    public class ColorSets : ScriptableObject
    {
        public Color[] colors;
    }
}
