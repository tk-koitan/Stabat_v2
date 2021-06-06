using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KoitanLib;
using Koitan;

public class CursorHand : MonoBehaviour
{
    [SerializeField]
    float cursorVelocity = 1f;
    [SerializeField]
    Transform kawacoinTrans;
    [SerializeField]
    Kawacoin kawacoin;
    [SerializeField]
    int id;


    Rigidbody2D rigidbody2D;
    Kawacoin kawakoin;

    public bool Havecoin { get; private set; }
    public int ID { get; private set; }
    public bool IsDicided { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        //kawakoin = Kawacoin.GetComponent<Kawacoin>();
        Havecoin = false;
        ID = id;
    }

    // Update is called once per frame
    void Update()
    {
        IsCollision();

        Move();
    }

    void Move()
    {
        rigidbody2D.velocity += cursorVelocity * KoitanInput.GetStick(ID);

        if (Havecoin)
        {
            Vector3 ofs = GetComponent<CircleCollider2D>().offset;
            kawacoinTrans.DOMove(transform.position + ofs , 0.1f);
        }
    }

    void IsCollision()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);

        foreach (Collider2D col in collisions)
        {
            if ((KoitanInput.GetDown(ButtonCode.A, ID) &&
                col.tag == "Chip" &&
                col.GetComponent<Kawacoin>().CursorHand.ID == ID)
                ||
                KoitanInput.GetDown(ButtonCode.B, ID))
            {
                IsDicided = true;
                Havecoin = !Havecoin;
            }
            else
            {
                IsDicided = false;
            }
        }
    }
}
