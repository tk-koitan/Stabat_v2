using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Koitan;

public class Kawacoin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int id;
    [SerializeField]
    CursorHand cursorHand;
    //[SerializeField]
    //GameObject Player;
    [SerializeField]
    PlayerController playerController;

    public int ID { get; private set; }
    //List<Collider2D> colList;

    void Start()
    {
        ID = id;
        //colList = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsCollision();

        if (cursorHand.Havecoin) transform.DOScale(1.3f, 0.1f);
        else transform.DOScale(1f, 0.1f);
    }

    void IsCollision()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);

        foreach (Collider2D col in collisions)
        {
            if (col.tag == "CharaIcon")
            {
                if (!cursorHand.Havecoin)
                {
                    //BattleSetting.playerIndexes[ID] = col.GetComponent<CharaIcon>().CharaID;
                    playerController.ChangeColor(col.GetComponent<CharaIcon>().CharaID, col.GetComponent<CharaIcon>().CharaID);
                    Debug.Log("Selected");
                    //collision.GetComponent<SelectCharacter>();
                    //ââèo
                }
                else
                {

                }
            }
        }
    }
}
