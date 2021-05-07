using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;

public class DebugTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        KoitanDebug.Display("DebugTest");
        KoitanDebug.DisplayBox($"pos = {transform.position}", this);
    }
}
