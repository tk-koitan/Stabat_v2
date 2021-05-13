using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace KoitanLib
{
    public class KoitanInput : MonoBehaviour
    {
        public static KoitanInput instance;
        //List<InputSystemPlayer> inputSystemPlayers = new List<InputSystemPlayer>();
        static List<ControllerInput> controllerInputList = new List<ControllerInput>();
        //List<int> joinIndexList = new List<int>();
        public static Action[] actionListWhenPlayerJoin = new Action[10];
        [SerializeField]
        Koitan.SimpleAI ai;
        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(this);
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        // Update is called once per frame
        void Update()
        {
            //���ԕύX           
            for (int i = 0; i < controllerInputList.Count; i++)
            {
                if (Input.GetKeyDown($"{i}"))
                {
                    ControllerInput tmpCon = controllerInputList[i];
                    controllerInputList.RemoveAt(i);
                    controllerInputList.Insert(0, tmpCon);
                }
            }

            //CPU�ǉ�
            if (Input.GetKeyDown(KeyCode.P) && controllerInputList.Count < 10)
            {
                Instantiate(ai);
            }

            //�f�o�b�O
            KoitanDebug.Display("Index : ABXYSStick\n");
            for (int i = 0; i < controllerInputList.Count; i++)
            {
                ControllerInput controller = controllerInputList[i];
                KoitanDebug.Display($"{i} : {(controller.Get(ButtonCode.A) ? "1" : "0")}{(controller.Get(ButtonCode.B) ? "1" : "0")}{(controller.Get(ButtonCode.X) ? "1" : "0")}{(controller.Get(ButtonCode.Y) ? "1" : "0")}{(controller.Get(ButtonCode.Start) ? "1" : "0")}{controller.GetStick()}\n");
            }
        }

        public static int SetController(ControllerInput ci)
        {
            controllerInputList.Add(ci);
            actionListWhenPlayerJoin[controllerInputList.Count - 1].Invoke();
            return controllerInputList.Count - 1;
        }

        /// <summary>
        /// {index}�Ԗڂ̃v���C���[��{code}�������Ă��邩
        /// </summary>
        /// <param name="code"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool Get(ButtonCode code, int index)
        {
            return index < controllerInputList.Count && controllerInputList[index].Get(code);
        }

        /// <summary>
        /// �����ꂩ�̃v���C���[��{code}�������Ă��邩
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool Get(ButtonCode code)
        {
            for (int i = 0; i < controllerInputList.Count; i++)
            {
                if (controllerInputList[i].Get(code))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// {index}�Ԗڂ̃v���C���[��{code}���������u��
        /// </summary>
        /// <param name="code"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetDown(ButtonCode code, int index)
        {
            return index < controllerInputList.Count && controllerInputList[index].GetDown(code);
        }

        /// <summary>
        /// �����ꂩ�̃v���C���[��{code}���������u��
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool GetDown(ButtonCode code)
        {
            for (int i = 0; i < controllerInputList.Count; i++)
            {
                if (controllerInputList[i].GetDown(code))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// {index}�Ԗڂ̃v���C���[��{code}�𗣂����u��
        /// </summary>
        /// <param name="code"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetUp(ButtonCode code, int index)
        {
            return index < controllerInputList.Count && controllerInputList[index].GetUp(code);
        }

        /// <summary>
        /// �����ꂩ�̃v���C���[��{code}�𗣂����u��
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool GetUp(ButtonCode code)
        {
            for (int i = 0; i < controllerInputList.Count; i++)
            {
                if (controllerInputList[i].GetUp(code))
                {
                    return true;
                }
            }
            return false;
        }

        public static Vector2 GetStick(int index)
        {
            if (index < controllerInputList.Count)
            {
                return controllerInputList[index].GetStick();
            }
            return Vector2.zero;
        }
    }

    /// <summary>
    /// Enum�̖��O��Action�̖��O�ƈ�v������
    /// </summary>
    public enum ButtonCode
    {
        A,
        B,
        X,
        Y,
        Start,
        Up,
        Down,
        Right,
        Left
    }
}
