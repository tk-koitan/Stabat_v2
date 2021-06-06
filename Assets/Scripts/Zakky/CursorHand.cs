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
    Transform kawacoinTrans;
    [SerializeField]
    Kawacoin kawacoin;

    //プロパティに代入する用
    [SerializeField]
    int id;


    Rigidbody2D rigidbody2D;
    CircleCollider2D circleCollider2D;
    Kawacoin kawakoin;

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
        Havecoin = true;
        ID = id;

        playerKind = PlayerKind.None;
    }

    // Update is called once per frame
    void Update()
    {
        PutChip();

        Move();

        ChangePlayerKind();

        BattleCheck();
    }
    void PutChip()
    {
        if ((KoitanInput.GetDown(ButtonCode.A, ID) && IsCollision()))
        {
            Havecoin = !Havecoin;
        }
        else if (KoitanInput.GetDown(ButtonCode.B, ID))
        {
            Havecoin = true;
        }
    }

    void Move()
    {
        rigidbody2D.velocity += cursorVelocity * KoitanInput.GetStick(ID) * Time.deltaTime;

        if (Havecoin)
        {
            Vector3 ofs = circleCollider2D.offset;
            kawacoinTrans.DOMove(transform.position + ofs , 0.05f);
        }
    }

    void ChangePlayerKind()
    {
        if (KoitanInput.GetDown(ButtonCode.Y, ID))
        {
            playerKind++;
            playerKind = (PlayerKind)((int)playerKind % ((int)PlayerKind.Computer + 1));
            Koitan.BattleSetting.ControllPlayers[ID] = (int)playerKind;
        }
    }

    void BattleCheck()
    {
        if (KoitanInput.GetDown(ButtonCode.X, ID) && kawacoin.IsDecided)
        {
            BattleManager.StartBattle();
        }
    }

    //メンバで持ってるチップと同じIDのときtrue
    bool IsCollision()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, circleCollider2D.radius);

        foreach (Collider2D col in collisions)
        {
            if (col.tag == "Chip" &&
                col.GetComponent<Kawacoin>().CursorHand.ID == ID)
            {
                return true;
            }
        }
        return false;
    }
}
