using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;

namespace Koitan
{
    public class SimpleAI : ControllerInput
    {
        // Start is called before the first frame update
        void Start()
        {
            //�K��Start�̍ŏ��ɌĂ�ŉ�����
            Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            //�K��Update�̍ŏ��ɌĂ�ŉ�����
            BeforeUpdate();
            if (controllerIndex == 0) return;
            Vector3 myPos = BattleManager.GetPlayer(controllerIndex).transform.position;
            Vector3 otherPos = BattleManager.GetPlayer(controllerIndex - 1).transform.position;
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
        }
    }

}