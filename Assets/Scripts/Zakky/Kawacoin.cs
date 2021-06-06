using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Koitan;

public class Kawacoin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int id;
    [SerializeField]
    CursorHand cursorHand;

    public int ID { get; private set; }

    void Start()
    {
        ID = id;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "SelectCharacter")
        {
            if (!cursorHand.Havecoin)
            {
                BattleSetting.charaColorIndexes[ID] = 0;
                //collision.GetComponent<SelectCharacter>()
                //ââèo
            }
            else
            {

            }
        }
    }
}
