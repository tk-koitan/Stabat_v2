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

    void Start()
    {
        CharaID = charaID;
        iniScale = transform.localScale;
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

    void IsCollision()
    {
        Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0f);

        bool belowKawacoin = false;
        foreach (Collider2D col in collisions)
        {
            //ÉRÉCÉìÇ…êGÇÍÇƒÇÈÇ©Ç¬íuÇ©ÇÍÇƒÇ»Ç¢Ç»ÇÁ
            if (col.tag == "Chip" && col.GetComponent<Kawacoin>().CursorHand.Havecoin)
            {
                belowKawacoin = true;
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
