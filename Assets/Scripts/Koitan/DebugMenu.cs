using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;
using System;

namespace KoitanLib
{
    public class DebugMenu : MonoBehaviour
    {
        //���j���[���J���Ă��邩
        bool isOpen = false;
        //���j���[�̊�{�\��
        //����
        Func<string>[] statements = new Func<string>[16];
        //�A�N�V����
        Action[] acts = new Action[16];
        int maxIndex = 16;
        int currentIndex = 0;
        Action currentAct;
        string currentMenuName;
        //����
        List<string> historyStrs = new List<string>();
        string historyStatement;
        Queue<Action> historyActs = new Queue<Action>();
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (!isOpen && Input.GetKeyDown(KeyCode.Tab))
            {
                isOpen = true;
                MainMenu();
            }

            if (!isOpen)
            {
                return;
            }

            //�J�[�\���ړ�
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentIndex = (currentIndex - 1 + maxIndex) % maxIndex;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentIndex = (currentIndex + 1 + maxIndex) % maxIndex;
            }
            //�L�����Z��
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (historyActs.Count > 0)
                {
                    historyStrs.RemoveAt(historyStrs.Count - 1);
                    historyActs.Dequeue()();
                }
                else
                {
                    //CloseMenu
                    isOpen = false;
                }
            }
            else
            {
                //���s�@
                acts[currentIndex]();
            }

            //�`��
            if (historyStrs.Count > 0)
            {
                KoitanDebug.Display($"{historyStatement} > {currentMenuName}\n");
            }
            else
            {
                KoitanDebug.Display($"{currentMenuName}\n");
            }
            for (int i = 0; i < maxIndex; i++)
            {
                if (i == currentIndex)
                {
                    KoitanDebug.Display($"> {statements[i]()}\n");
                }
                else
                {
                    KoitanDebug.Display($"  {statements[i]()}\n");
                }
            }
        }

        /// <summary>
        /// �ŏ��̉��
        /// </summary>
        void MainMenu()
        {
            currentMenuName = "MainMenu";
            currentAct = MainMenu;
            historyStatement = String.Join(" > ", historyStrs);
            currentIndex = 0;
            maxIndex = 3;
            statements[0] = () => $"TestMenu";
            statements[1] = () => $"Time.deltaTime = {Time.deltaTime}";
            statements[2] = () => $"Time.deltaTime = {Time.deltaTime}";
            acts[0] = ButtonDownAct(() => TestMenu());
            acts[1] = ButtonDownAct(() => TestMenu());
            acts[2] = ButtonDownAct(() => TestMenu());
        }

        /// <summary>
        /// �e�X�g
        /// </summary>
        void TestMenu()
        {
            currentMenuName = "TestMenu";
            currentAct = TestMenu;
            historyStatement = String.Join(" > ", historyStrs);
            currentIndex = 0;
            maxIndex = 1;
            statements[0] = () => $"MainMennu";
            acts[0] = ButtonDownAct(() => MainMenu());
        }

        Action ButtonDownAct(Action act)
        {
            return () =>
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    //�L�����Ă���
                    historyStrs.Add(currentMenuName);
                    historyActs.Enqueue(currentAct);
                    act();
                }
            };
        }
    }
}
