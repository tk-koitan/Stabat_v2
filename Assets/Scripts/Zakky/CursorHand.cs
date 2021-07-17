using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KoitanLib;
using Koitan;

public class CursorHand : MonoBehaviour
{
    [SerializeField]
    float cursorVelocity = 100f;

    [SerializeField]
    Chip chip;
    [SerializeField]
    GameObject Kawaztan;

    //プロパティに代入する用
    [SerializeField]
    int id;


    Rigidbody2D rigidbody2D;
    CircleCollider2D circleCollider2D;

    public bool Havecoin { get; private set; }
    public int ID { get; private set; }

    public enum PlayerKind
    {
        None,
        Human,
        Computer
    }

    public PlayerKind playerKind;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        Havecoin = false;
        ID = id;

        playerKind = PlayerKind.None;
    }

    // Update is called once per frame
    void Update()
    {
        //適当
        switch (playerKind) {
            case PlayerKind.None:
                Kawaztan.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                break;
            case PlayerKind.Human:
                Kawaztan.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case PlayerKind.Computer:
                Kawaztan.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                break;
        }


        PutChip();

        Move();

        if (KoitanInput.GetDown(ButtonCode.Y, ID))
        {
            CursorHand curtmp = IsKawaztanCollision();
            if (curtmp != null) curtmp.ChangePlayerKind();
            else ChangePlayerKind();
        }

        BattleCheck();
    }
    void PutChip()
    {
        Chip chiptmp = IsChipCollision();
        //A押してかつ範囲内にチップがあるとき
        if ((KoitanInput.GetDown(ButtonCode.A, ID) && chiptmp != null))
        {
            chip = chiptmp;
            //チップ持ち置き
            Havecoin = !Havecoin;
            //カーソルの移動を止める
            rigidbody2D.velocity = Vector2.zero;
        }
        else if (KoitanInput.GetDown(ButtonCode.B, ID))
        {
            //B押したときチップ持つ
            Havecoin = true;
        }
        chip.hadCoin = Havecoin;
    }

    void Move()
    {
        //移動入力で速度を加速
        rigidbody2D.velocity += cursorVelocity * KoitanInput.GetStick(ID) * Time.deltaTime;

        //コイン持ってる判定のとき
        if (Havecoin)
        {
            //指先にチップの持つ場所を移動する．
            Vector3 ofs = circleCollider2D.offset;
            chip.gameObject.transform.DOMove(transform.position + ofs , 0.05f);
        }
    }

    public void ChangePlayerKind()
    {
        playerKind++;
        playerKind = (PlayerKind)((int)playerKind % ((int)PlayerKind.Computer + 1));
        Koitan.BattleSetting.ControllPlayers[ID] = (int)playerKind;
    }

    void BattleCheck()
    {
        if (KoitanInput.GetDown(ButtonCode.X, ID) && Chip.CanStartBattle())
        {
            Chip.DeleteChipsList();
            BattleManager.StartBattle();
        }
    }

    //メンバで持ってるチップと同じIDのときtrue
    Chip IsChipCollision()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, circleCollider2D.radius);
        Chip chiptmp = null;
        float distance = (float)1e9;

        foreach (Collider2D col in collisions)
        {
            Chip nowchip = col.GetComponent<Chip>();
            //自分の持ってるチップかコンピュータのチップのとき
            if (col.tag == "Chip" &&
                (nowchip.CursorHand.ID == ID ||
                (nowchip.CursorHand.playerKind == PlayerKind.Computer && !nowchip.CursorHand.Havecoin)))
            {
                Vector3 off = circleCollider2D.offset;
                float dis = Vector2.Distance(transform.position + off, col.transform.position);
                //そのchipを取得する
                if (distance > dis && !(chip.hadCoin && !nowchip.hadCoin))
                {
                    distance = dis;
                    chiptmp = nowchip;
                }
            }
        }
        return chiptmp;
    }

    CursorHand IsKawaztanCollision()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, circleCollider2D.radius);

        foreach (Collider2D col in collisions)
        {
            if (col.tag == "Player")
            {
                return col.GetComponent<SelectedCharacter>().CursorHand;
            }
        }
        return null;
    }
}
