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
        KoitanDebug.Display("KoitanDebug.Display�̃f�o�b�O�e�X�g�ł�");
        KoitanDebug.DisplayBox($"pos = {transform.position}", this);
    }
}
