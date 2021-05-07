using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;

namespace KoitanLib
{
    public class KoitanInputTest : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer sprite;
        [SerializeField]
        int n;
        [SerializeField]
        int m;
        List<SpriteRenderer> spList = new List<SpriteRenderer>();
        int currentFrame = 0;
        // Start is called before the first frame update
        void Start()
        {
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    spList.Add(Instantiate(sprite, new Vector3(0.2f * i, 0.2f * j, 0), Quaternion.identity));
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                spList[currentFrame].color = Color.red;
            }
            else
            {
                spList[currentFrame].color = Color.white;
            }
            currentFrame++;
            if (currentFrame == n * m)
            {
                currentFrame = 0;
            }
            KoitanDebug.Display(currentFrame.ToString());
        }
    }
}

