using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoitanLib
{
    public class FPSCounter : MonoBehaviour
    {
        float timer = 0;
        int frameCnt = 0;
        /// <summary>
        /// フレームレート
        /// </summary>
        public int fps { private set; get; }
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

            frameCnt++;
            timer += Time.unscaledDeltaTime;
            if (timer > 1f)
            {
                timer -= 1f;
                //KoitanDebug.Display($"FPS = {frameCnt}\n");
                fps = frameCnt;
                frameCnt = 0;
            }
        }
    }
}
