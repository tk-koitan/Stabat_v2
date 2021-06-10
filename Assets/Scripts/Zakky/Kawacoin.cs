using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Koitan;

public class Kawacoin : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    CursorHand cursorHand;
    [SerializeField]
    PlayerController playerController;
    public CursorHand CursorHand { get; private set; }
    public bool IsDecided { get; private set; }
    public bool hadCoin;

    Vector3 iniScale;
    CircleCollider2D circleCollider2D;

    static List<Kawacoin> AllKawacoins = new List<Kawacoin>();
    public static bool CanStartBattle()
    {
        foreach(Kawacoin k in AllKawacoins)
        {
            //NoneまたはkがDicidedである
            if (BattleSetting.ControllPlayers[k.CursorHand.ID] != (int)CursorHand.PlayerKind.None && !k.IsDecided) return false;
        }
        return true;
    }

    public static void DeleteKawacoinsList()
    {
        AllKawacoins.Clear();
    }

    void Start()
    {
        AllKawacoins.Add(this);
        CursorHand = cursorHand;
        iniScale = transform.localScale;
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsCollision();

        Scaler();
    }

    void IsCollision()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, circleCollider2D.radius);

        IsDecided = false;

        foreach (Collider2D col in collisions)
        {
            //アイコンに触れてるなら，さらにカーソルおいてるなら
            if (col.tag == "CharaIcon" && !cursorHand.Havecoin)
            {
                //キャラクター変える
                int tmp = col.GetComponent<CharaIcon>().CharaID;
                //表示されたキャラの色変える
                playerController.ChangeColor(tmp, tmp);
                //内部での色も変える
                BattleSetting.charaColorIndexes[CursorHand.ID] = tmp;

                IsDecided = true;
            }
        }
    }

    void Scaler()
    {
        if (hadCoin) transform.DOScale(1.3f, 0.1f);
        else transform.DOScale(1f, 0.1f);
    }
}
