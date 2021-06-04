using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;

namespace Koitan
{
    public class SimpleAI : ControllerInput
    {
        private List<Vector3> path = new List<Vector3>();
        private Vector3 myPos = Vector3.zero;
        private float retryTimeMax = 1f;
        float retryTime;
        // Start is called before the first frame update
        void Start()
        {
            //必ずStartの最初に呼んで下さい
            Initialize();
            controllerName = "CPU";
            //わざと消えるようにする
            //DontDestroyOnLoad(this);
        }

        // Update is called once per frame
        void Update()
        {
            //必ずUpdateの最初に呼んで下さい
            BeforeUpdate();
            //if (controllerIndex == 0) return;
            //バトル中以外は何もしない
            if (BattleManager.instance == null || controllerIndex >= BattleManager.Players.Count) return;
            Vector3 otherPos;
            myPos = BattleManager.Players[controllerIndex].transform.position;
            //otherPos = BattleManager.Players[controllerIndex - 1].transform.position;

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                path = StageGraph.instance.GetPath(myPos, mousePos);
                retryTime = 0;
            }
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 randamPos = new Vector3(Random.Range(-30f, 30f), Random.Range(-20f, 20f));
                path = StageGraph.instance.GetPath(myPos, randamPos);
                retryTime = 0;
            }

            if (path == null) return;
            if (path.Count > 0)
            {
                if (retryTime >= retryTimeMax)
                {
                    path = StageGraph.instance.GetPath(myPos, path[path.Count - 1]);
                    otherPos = path[0];
                    retryTime = 0;
                }
                else
                {
                    otherPos = path[0];
                    retryTime += Time.deltaTime;
                }
            }
            else
            {
                return;
            }

            if (otherPos.y - myPos.y > 3f && !oldButton[ButtonCode.B])
            {
                PressButtonTime(ButtonCode.B, 0.2f);
            }
            float distance = (otherPos - myPos).magnitude;
            if (distance > 3f)
            {
                if (otherPos.x - myPos.x > 0f)
                {
                    Stick = new Vector2(1f, 0);
                }
                else if (otherPos.x - myPos.x < 0f)
                {
                    Stick = new Vector2(-1f, 0);
                }
            }
            else
            {
                path.RemoveAt(0);
                retryTime = 0;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (path != null)
            {
                if (path.Count > 0)
                {
                    //GizmosExtensions2D.DrawRectArrow2D(myPos, path[0], 0.25f);
                    GizmosExtensions2D.DrawArrow2D(myPos, path[0], 0.25f);
                    Gizmos.DrawWireSphere(path[path.Count - 1], 0.5f);
                }
                for (int i = 0; i < path.Count - 1; i++)
                {
                    //GizmosExtensions2D.DrawRectArrow2D(path[i], path[i + 1], 0.25f);
                    GizmosExtensions2D.DrawArrow2D(path[i], path[i + 1], 0.25f);
                }
            }
        }
    }
}