using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kawacoin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int id;
    public int ID { get; private set; }

    void Start()
    {
        ID = id;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
