using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace KoitanLib
{
    /// <summary>
    /// InputSystemによる入力をInputクラスのように扱うためのクラス
    /// KoitanInputより早くUpdateを更新するようにする
    /// </summary>
    public class InputSystemPlayer : ControllerInput
    {
        private PlayerInput playerInput;
        public int joinIndex { get; private set; }
        private InputAction StickInput;

        /// <summary>
        /// アナログ入力のデッドライン
        /// </summary>
        private float deadline = 0.5f;
        private Dictionary<ButtonCode, InputAction> currentInput = new Dictionary<ButtonCode, InputAction>();

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
            TryGetComponent(out playerInput);
            StickInput = playerInput.currentActionMap.FindAction("Stick");
            foreach (ButtonCode code in Enum.GetValues(typeof(ButtonCode)))
            {
                //ButtonCodeとActionの名前を一致させる
                currentInput.Add(code, playerInput.currentActionMap.FindAction(code.ToString()));
            }
            joinIndex = playerInput.playerIndex;
            //登録
            //KoitanInput.SetController(this);
        }

        // Update is called once per frame
        void Update()
        {
            BeforeUpdate();
            //更新
            Stick = StickInput.ReadValue<Vector2>();
            foreach (ButtonCode code in Enum.GetValues(typeof(ButtonCode)))
            {
                Button[code] = currentInput[code].ReadValue<float>() > deadline;
                //KoitanDebug.Display($"{code} : {currentButton[code]}\n");
            }
        }
    }
}
