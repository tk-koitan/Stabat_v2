using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StageSelectHand : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    CircleCollider2D circleCollider2D;

    [SerializeField]
    float cursorVelocity = 100f;
    [SerializeField]
    Chip chip;

    public bool Havecoin { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        Havecoin = false;
    }

    // Update is called once per frame
    void Update()
    {
        PutChip();

        Move();
    }

    void Move()
    {
        //移動入力で速度を加速
        for (int i = 0; i < 1; i++)
        {
            rigidbody2D.velocity += cursorVelocity * KoitanInput.GetStick(i) * Time.deltaTime;
        }

        //コイン持ってる判定のとき
        if (Havecoin)
        {
            //指先にチップの持つ場所を移動する．
            Vector3 ofs = circleCollider2D.offset;
            chip.gameObject.transform.DOMove(transform.position + ofs, 0.05f);
        }
    }

    void PutChip()
    {
        Chip chiptmp = IsChipCollision();
        for (int i = 0; i < 1; i++)
        {
            //A押してかつ範囲内にチップがあるとき
            if ((KoitanInput.GetDown(ButtonCode.A, i) && chiptmp != null))
            {
                chip = chiptmp;
                //チップ持ち置き
                Havecoin = !Havecoin;
                //カーソルの移動を止める
                rigidbody2D.velocity = Vector2.zero;
            }
            else if (KoitanInput.GetDown(ButtonCode.B, i))
            {
                //B押したときチップ持つ
                Havecoin = true;
            }
            Debug.Log(Havecoin);
        }
        
        chip.hadCoin = Havecoin;
    }
    Chip IsChipCollision()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, circleCollider2D.radius);
        Chip chiptmp = null;
        float distance = (float)1e9;

        foreach (Collider2D col in collisions)
        {
            Chip nowchip = col.GetComponent<Chip>();
            //自分の持ってるチップかコンピュータのチップのとき
            if (col.tag == "Chip")
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
}