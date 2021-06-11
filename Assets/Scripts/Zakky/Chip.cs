using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Koitan;

public class Chip : MonoBehaviour
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

    static List<Chip> AllChips = new List<Chip>();
    public static bool CanStartBattle()
    {
        foreach(Chip c in AllChips)
        {
            //None�܂���k��Dicided�ł���
            if (BattleSetting.ControllPlayers[c.CursorHand.ID] != (int)CursorHand.PlayerKind.None && !c.IsDecided) return false;
        }
        return true;
    }

    public static void DeleteChipsList()
    {
        AllChips.Clear();
    }

    void Start()
    {
        AllChips.Add(this);
        CursorHand = cursorHand;
        iniScale = transform.localScale;
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsCollision();

        circleCollider2D.isTrigger = hadCoin;

        Scaler();
    }

    void IsCollision()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, circleCollider2D.radius);

        IsDecided = false;

        foreach (Collider2D col in collisions)
        {
            //�A�C�R���ɐG��Ă�Ȃ�C����ɃJ�[�\�������Ă�Ȃ�
            if (col.tag == "CharaIcon" && !cursorHand.Havecoin)
            {
                //�L�����N�^�[�ς���
                int tmp = col.GetComponent<CharaIcon>().CharaID;
                //�\�����ꂽ�L�����̐F�ς���
                playerController.ChangeColor(tmp, tmp);
                //�����ł̐F���ς���
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
