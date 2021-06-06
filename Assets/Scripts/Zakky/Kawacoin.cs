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
    public CursorHand CursorHand { get; private set; }
    [SerializeField]
    PlayerController playerController;

    Vector3 iniScale;
    CircleCollider2D circleCollider2D;

    void Start()
    {
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

        foreach (Collider2D col in collisions)
        {
            //アイコンに触れてるなら，さらにカーソルおいてるなら
            if (col.tag == "CharaIcon" && !cursorHand.Havecoin)
            {
                //キャラクター変える
                int tmp = col.GetComponent<CharaIcon>().CharaID;

                playerController.ChangeColor(tmp, tmp);
                BattleSetting.charaColorIndexes[CursorHand.ID] = tmp;
            }
        }
    }

    void Scaler()
    {
        if (CursorHand.Havecoin) transform.DOScale(1.3f, 0.1f);
        else transform.DOScale(1f, 0.1f);
    }
}
