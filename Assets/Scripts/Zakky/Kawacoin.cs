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
    //[SerializeField]
    //CharaIcon charaIcon;
    public CursorHand CursorHand { get; private set; }
    [SerializeField]
    PlayerController playerController;

    Vector3 iniScale;

    void Start()
    {
        CursorHand = cursorHand;
        iniScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        IsCollision();

        Scaler();
    }

    void IsCollision()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);

        foreach (Collider2D col in collisions)
        {
            //アイコンに触れてるなら
            if (col.tag == "CharaIcon")
            {
                //さらにカーソルおいてるなら
                if (!cursorHand.Havecoin)
                {
                    //キャラクター変える
                    playerController.ChangeColor(col.GetComponent<CharaIcon>().CharaID, col.GetComponent<CharaIcon>().CharaID);
                }
                else
                {

                }
            }
        }
    }

    void Scaler()
    {
        if (cursorHand.Havecoin) transform.DOScale(1.3f, 0.1f);
        else transform.DOScale(1f, 0.1f);
    }
}
