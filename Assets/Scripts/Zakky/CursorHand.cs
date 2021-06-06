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
    [SerializeField]
    int id;


    Rigidbody2D rigidbody2D;
    CircleCollider2D circleCollider2D;
    Kawacoin kawakoin;

    public bool Havecoin { get; private set; }
    public int ID { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        Havecoin = true;
        ID = id;
    }

    // Update is called once per frame
    void Update()
    {
        PutChip();

        Move();
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
