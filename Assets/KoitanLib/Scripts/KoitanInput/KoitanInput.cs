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
        static List<ControllerInput> ControllerInputList = new List<ControllerInput>();
        //List<int> joinIndexList = new List<int>();
        //public static Action[] actionListWhenPlayerJoin = new Action[10];
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
            //順番変更
            /*
            for (int i = 0; i < controllerInputList.Count; i++)
            {
                if (Input.GetKeyDown($"{i}"))
                {
                    ControllerInput tmpCon = controllerInputList[i];
                    controllerInputList.RemoveAt(i);
                    controllerInputList.Insert(0, tmpCon);
                    for (int j = 0; j < controllerInputList.Count; j++)
                    {
                        controllerInputList[j].SetControllerIndex(j);
                    }
                }
            }
            */

            //つなぎ直す
            /*
            if (Input.GetKeyDown(KeyCode.K))
            {
                for (int i = controllerInputList.Count - 1; i >= 0; i--)
                {
                    Destroy(controllerInputList[i].gameObject);
                    controllerInputList[i] = null;
                    controllerInputList.RemoveAt(i);
                }
            }
            */

            //CPU追加
            /*
            if (Input.GetKeyDown(KeyCode.P) && controllerInputList.Count < Koitan.BattleGlobal.MaxPlayerNum)
            {
                Instantiate(ai);
            }
            */

            //デバッグ
            KoitanDebug.Display("Index : ABXYSStick\n");
            for (int i = 0; i < ControllerInputList.Count; i++)
            {
                ControllerInput controller = ControllerInputList[i];
                KoitanDebug.Display($"{controller.controllerName} : {(controller.Get(ButtonCode.A) ? "1" : "0")}{(controller.Get(ButtonCode.B) ? "1" : "0")}{(controller.Get(ButtonCode.X) ? "1" : "0")}{(controller.Get(ButtonCode.Y) ? "1" : "0")}{(controller.Get(ButtonCode.Start) ? "1" : "0")}{controller.GetStick()}\n");
            }
        }

        public static int SetHumanInput(ControllerInput ci)
        {
            ControllerInputList.Add(ci);
            //actionListWhenPlayerJoin[controllerInputList.Count - 1]?.Invoke();
            return ControllerInputList.Count - 1;
        }

        public static void ClearAllController()
        {
            for (int i = ControllerInputList.Count - 1; i >= 0; i--)
            {
                Destroy(ControllerInputList[i].gameObject);
                ControllerInputList[i] = null;
                ControllerInputList.RemoveAt(i);
            }
        }

        public static void ClearAllCPU()
        {
            for (int i = ControllerInputList.Count - 1; i >= 0; i--)
            {
                if (ControllerInputList[i].GetType() != typeof(InputSystemPlayer))
                {
                    Destroy(ControllerInputList[i].gameObject);
                    ControllerInputList[i] = null;
                    ControllerInputList.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// {index}番目のプレイヤーが{code}を押しているか
        /// </summary>
        /// <param name="code"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool Get(ButtonCode code, int index)
        {
            return index < ControllerInputList.Count && ControllerInputList[index].Get(code);
        }

        /// <summary>
        /// いずれかのプレイヤーが{code}を押しているか
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool Get(ButtonCode code, bool includeCPU = false)
        {
            for (int i = 0; i < ControllerInputList.Count; i++)
            {
                if (!includeCPU && !ControllerInputList[i].isHuman) continue;
                if (ControllerInputList[i].Get(code))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// {index}番目のプレイヤーが{code}を押した瞬間
        /// </summary>
        /// <param name="code"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetDown(ButtonCode code, int index)
        {
            return index < ControllerInputList.Count && ControllerInputList[index].GetDown(code);
        }

        /// <summary>
        /// いずれかのプレイヤーが{code}を押した瞬間
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool GetDown(ButtonCode code, bool includeCPU = false)
        {
            for (int i = 0; i < ControllerInputList.Count; i++)
            {
                if (!includeCPU && !ControllerInputList[i].isHuman) continue;
                if (ControllerInputList[i].GetDown(code))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// {index}番目のプレイヤーが{code}を離した瞬間
        /// </summary>
        /// <param name="code"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetUp(ButtonCode code, int index)
        {
            return index < ControllerInputList.Count && ControllerInputList[index].GetUp(code);
        }

        /// <summary>
        /// いずれかのプレイヤーが{code}を離した瞬間
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool GetUp(ButtonCode code, bool includeCPU = false)
        {
            for (int i = 0; i < ControllerInputList.Count; i++)
            {
                if (!includeCPU && !ControllerInputList[i].isHuman) continue;
                if (ControllerInputList[i].GetUp(code))
                {
                    return true;
                }
            }
            return false;
        }

        public static Vector2 GetStick(int index)
        {
            if (index < ControllerInputList.Count)
            {
                return ControllerInputList[index].GetStick();
            }
            return Vector2.zero;
        }

        public static int GetControllerNum()
        {
            return ControllerInputList.Count;
        }
    }

    /// <summary>
    /// Enumの名前をActionの名前と一致させる
    /// </summary>
    public enum ButtonCode
    {
        A,
        B,
        X,
        Y,
        Start,
        Select,
        Up,
        Down,
        Right,
        Left
    }
}
