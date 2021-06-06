using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KoitanLib;

public class CursorHand : MonoBehaviour
{
    [SerializeField]
    float cursorVelocity = 1f;
    [SerializeField]
    GameObject Kawacoin;


    Rigidbody2D rigidbody2D;
    Kawacoin kawakoin;
    public bool Havecoin { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        kawakoin = Kawacoin.GetComponent<Kawacoin>();
        Havecoin = false;
    }

    // Update is called once per frame
    void Update()
    {
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

    private void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetButtonDown("Fire1") &&
            col.GetComponent<Kawacoin>().ID == Kawacoin.GetComponent<Kawacoin>().ID)
        {
            Havecoin = !Havecoin;
        }
    }
}