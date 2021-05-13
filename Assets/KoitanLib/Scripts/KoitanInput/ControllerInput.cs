using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace KoitanLib
{
    public abstract class ControllerInput : MonoBehaviour
    {
        protected int controllerIndex = -1;
        protected Vector2 Stick = Vector2.zero;
        protected Dictionary<ButtonCode, bool> Button = new Dictionary<ButtonCode, bool>();
        protected Dictionary<ButtonCode, bool> oldButton = new Dictionary<ButtonCode, bool>();
        private Dictionary<ButtonCode, float> ButtonTime = new Dictionary<ButtonCode, float>();

        /// <summary>
        /// åpè≥êÊÇÃStartÇÃêÊì™Ç≈åƒÇÒÇ≈â∫Ç≥Ç¢
        /// </summary>
        protected void Initialize()
        {
            foreach (ButtonCode code in Enum.GetValues(typeof(ButtonCode)))
            {
                Button.Add(code, false);
                oldButton.Add(code, false);
                ButtonTime.Add(code, 0f);
            }
            //ìoò^
            controllerIndex = KoitanInput.SetController(this);
        }

        /// <summary>
        /// åpè≥êÊÇÃUpdateÇÃêÊì™Ç≈åƒÇÒÇ≈â∫Ç≥Ç¢
        /// </summary>
        protected void BeforeUpdate()
        {
            Stick = Vector2.zero;
            foreach (ButtonCode code in Enum.GetValues(typeof(ButtonCode)))
            {
                oldButton[code] = Button[code];
                if (ButtonTime[code] > 0)
                {
                    Button[code] = true;
                    ButtonTime[code] -= Time.deltaTime;
                }
                else
                {
                    Button[code] = false;
                }
            }
        }

        protected void PressButtonTime(ButtonCode code, float sec)
        {
            ButtonTime[code] = sec;
            Button[code] = true;
        }

        public bool Get(ButtonCode code)
        {
            return Button[code];
        }

        public bool GetDown(ButtonCode code)
        {
            return !oldButton[code] && Button[code];
        }

        public bool GetUp(ButtonCode code)
        {
            return oldButton[code] && !Button[code];
        }

        public Vector2 GetStick()
        {
            return Stick;
        }
    }
}
