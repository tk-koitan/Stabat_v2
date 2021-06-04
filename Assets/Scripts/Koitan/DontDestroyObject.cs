using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Koitan
{
    public class DontDestroyObject : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
