using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaIcon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int charaID;
    public int CharaID { get; private set; }

    void Start()
    {
        CharaID = charaID;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
