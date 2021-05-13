using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace KoitanLib
{
    /// <summary>
    /// InputSystem�ɂ����͂�Input�N���X�̂悤�Ɉ������߂̃N���X
    /// KoitanInput��葁��Update���X�V����悤�ɂ���
    /// </summary>
    public class InputSystemPlayer : ControllerInput
    {
        private PlayerInput playerInput;
        public int joinIndex { get; private set; }
        private InputAction StickInput;

        /// <summary>
        /// �A�i���O���͂̃f�b�h���C��
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
                //ButtonCode��Action�̖��O����v������
                currentInput.Add(code, playerInput.currentActionMap.FindAction(code.ToString()));
            }
            joinIndex = playerInput.playerIndex;
            //�o�^
            //KoitanInput.SetController(this);
        }

        // Update is called once per frame
        void Update()
        {
            BeforeUpdate();
            //�X�V
            Stick = StickInput.ReadValue<Vector2>();
            foreach (ButtonCode code in Enum.GetValues(typeof(ButtonCode)))
            {
                Button[code] = currentInput[code].ReadValue<float>() > deadline;
                //KoitanDebug.Display($"{code} : {currentButton[code]}\n");
            }
        }
    }
}
