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
    GameObject Kawacoin;
    

    Rigidbody2D rigidbody2D;
    Kawacoin kawakoin;
    //List<Collider2D> colList;

    public bool Havecoin { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        kawakoin = Kawacoin.GetComponent<Kawacoin>();
        //colList = new List<Collider2D>();
        Havecoin = false;
    }

    // Update is called once per frame
    void Update()
    {
        IsCollision();

        Move();
    }

    void Move()
    {
        rigidbody2D.velocity += cursorVelocity * KoitanInput.GetStick(kawakoin.ID);

        if (Havecoin)
        {
            Vector3 ofs = GetComponent<CircleCollider2D>().offset;
            Kawacoin.transform.DOMove(transform.position + ofs , 0.1f);
        }
    }

    void IsCollision()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);

        foreach (Collider2D col in collisions)
        {
            if (KoitanInput.GetDown(ButtonCode.A) &&
            col.tag == "Chip" &&
            col.GetComponent<Kawacoin>().ID == Kawacoin.GetComponent<Kawacoin>().ID)
            {
                Havecoin = !Havecoin;
            }
        }
    }
}
