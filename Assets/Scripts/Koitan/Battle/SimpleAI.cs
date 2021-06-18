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
            //�K��Start�̍ŏ��ɌĂ�ŉ�����
            Initialize();
            controllerName = "CPU";
            //�킴�Ə�����悤�ɂ���
            //DontDestroyOnLoad(this);
        }

        // Update is called once per frame
        void Update()
        {
            //�K��Update�̍ŏ��ɌĂ�ŉ�����
            BeforeUpdate();
            //if (controllerIndex == 0) return;
            //�o�g�����ȊO�͉������Ȃ�
            if (BattleManager.instance == null || controllerIndex >= BattleManager.Players.Count) return;
            Vector3 otherPos;
            myPos = BattleManager.Players[controllerIndex].transform.position;
            //otherPos = BattleManager.Players[controllerIndex - 1].transform.position;

            if (Input.GetMouseButtonDown(0))
            {
                /*
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                */
                SearchPath();
            }
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 randamPos = new Vector3(Random.Range(-30f, 30f), Random.Range(-20f, 20f));
                path = StageGraph.instance.GetPath(myPos, randamPos);
                retryTime = 0;
            }

            if (path == null)
            {
                SearchPath();
                return;
            }
            if (path.Count > 0)
            {
                if (retryTime >= retryTimeMax)
                {
                    //path = StageGraph.instance.GetPath(myPos, path[path.Count - 1]);
                    SearchPath();
                    if (path == null) return;
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
                SearchPath();
                return;
            }

            if (otherPos.y - myPos.y > 3f && !oldButton[ButtonCode.B])
            {
                PressButtonTime(ButtonCode.B, 0.2f);
            }
            float distance = (otherPos - myPos).magnitude;
            if (distance > 2f)
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
                if (path.Count == 0)
                {
                    Button[ButtonCode.A] = true;
                }
            }
        }

        public void SearchPath()
        {
            // �܂��󂢂Ă��锄�n��T��
            Vector3 shopPos = myPos;
            float shopDist;
            shopDist = 1000000;
            //Debug.Log("uritisearch");
            foreach (ShopController shop in BattleManager.Shops)
            {
                if (!shop.isBuild)
                {
                    Vector3 tmpShopPos = shop.transform.position;
                    float dist = Vector3.Distance(myPos, tmpShopPos);
                    if (dist < shopDist)
                    {
                        shopDist = dist;
                        shopPos = tmpShopPos;
                    }
                }
            }
            // �󂢂Ă��锄�n��������
            if (shopPos != myPos)
            {
                path = StageGraph.instance.GetPath(myPos, shopPos);
                retryTime = 0;
                // ��������̂Ŕ�����
                if (path != null) return;
            }
            //Debug.Log("okanesearch");
            // �Ȃ������炨�������
            Vector3 moneyPos = myPos;
            float moneyDist;
            moneyDist = 1000000;
            int teamIndex = BattleManager.Players[controllerIndex].teamIndex;
            foreach (Money money in BattleManager.moneyInstances)
            {
                // �`�[���J���[�s��v�ɂ�����s��
                if (!money.IsGetable(teamIndex)) continue;
                Vector3 tmpMoneyPos = money.transform.position;
                float dist = Vector3.Distance(myPos, tmpMoneyPos);
                if (dist < moneyDist)
                {
                    moneyDist = dist;
                    moneyPos = tmpMoneyPos;
                }
            }
            // ������������
            if (moneyPos != myPos)
            {
                path = StageGraph.instance.GetPath(myPos, moneyPos);
                retryTime = 0;
                return;
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
                    Gizmos.DrawWireSphere(path[path.Count - 1], 1f);
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