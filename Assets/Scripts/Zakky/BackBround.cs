using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;
using Koitan;

public class BackBround : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[BattleSetting.battleStageIndex];
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            BattleSetting.battleStageIndex += 1;
            BattleSetting.battleStageIndex %= sprites.Length;
            spriteRenderer.sprite = sprites[BattleSetting.battleStageIndex];
        }
        */
    }
}
