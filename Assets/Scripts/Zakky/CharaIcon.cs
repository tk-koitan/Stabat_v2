using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;
using DG.Tweening;

public class CharaIcon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int charaID;
    //[SerializeField]
    //CursorHand cursorHand;
    public int CharaID { get; private set; }
    Vector3 iniScale;
    BoxCollider2D boxCollider2D;

    void Start()
    {
        CharaID = charaID;
        iniScale = transform.localScale;
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsCollision();
    }

    public void Puruun()
    {
        transform.DOScale(iniScale * 1.3f, 0.1f);
    }

    public void IniScale()
    {
        transform.DOScale(iniScale, 0.1f);
    }

    //触れてるcollider全部まわす
    void IsCollision()
    {
        Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position, boxCollider2D.size, 0f);

        bool belowKawacoin = false;
        foreach (Collider2D col in collisions)
        {
            //コインに触れてるかつ置かれてないなら
            if (col.tag == "Chip")
            {
                if (col.GetComponent<Kawacoin>().hadCoin)
                {
                    belowKawacoin = true;
                }
            }
        }

        if (belowKawacoin)
        {
            Puruun();
        }
        else
        {
            IniScale();
        }
    }
}
