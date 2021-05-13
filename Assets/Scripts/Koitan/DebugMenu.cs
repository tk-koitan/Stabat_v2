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
        Func<string>[] infoMessages = new Func<string>[16];
        //�A�N�V����
        Action[] acts = new Action[16];
        int maxIndex = 16;
        int currentIndex = 0;
        Action currentAct;
        string currentMenuName;
        //����
        List<string> historyStrs = new List<string>();
        string historyStatement;
        Stack<Action> historyActs = new Stack<Action>();
        Stack<int> historyIndexes = new Stack<int>();

        FPSCounter fpsCounter;
        bool isShowFPS;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
            TryGetComponent(out fpsCounter);
            isShowFPS = true;
        }

        // Update is called once per frame
        void Update()
        {
            //�펞�\��
            if (isShowFPS)
            {
                KoitanDebug.Display($"FPS {fpsCounter.fps}\n");
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (isOpen)
                {
                    isOpen = false;
                }
                else
                {
                    isOpen = true;
                    //OpenRootPage(MainMenu);
                    if (historyActs.Count == 0)
                    {
                        EnterPage(MainMenu);
                    }
                }
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
                if (historyActs.Count > 1)
                {
                    historyStrs.RemoveAt(historyStrs.Count - 1);
                    historyStatement = String.Join(" > ", historyStrs);
                    //���݂̃y�[�W�̗���������
                    historyActs.Pop();
                    currentAct = historyActs.Peek();
                    currentAct();
                    currentIndex = historyIndexes.Pop();
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
                //���t���[���v�Z����̂͂悭�Ȃ�
                //KoitanDebug.Display($"{historyStatement}\n");
                KoitanDebug.Display($"--- {currentMenuName} ---\n");
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
            KoitanDebug.Display($"***INFO***\n{infoMessages[currentIndex]()}\n");
        }

        /// <summary>
        /// �ŏ��̉��
        /// </summary>
        void MainMenu()
        {
            currentMenuName = "�f�o�b�O���j���[";
            maxIndex = 3;
            statements[0] = () => $"�T�E���h";
            statements[1] = () => $"�f�B�X�v���C";
            statements[2] = () => $"�펞�\�����";
            acts[0] = ButtonDownAct(SoundMenu);
            acts[1] = ButtonDownAct(VideoMenu);
            acts[2] = ButtonDownAct(DisplayMenu);
            infoMessages[0] = () => $"�T�E���h�ݒ���J���܂�";
            infoMessages[1] = () => $"�f�B�X�v���C�ݒ���J���܂�";
            infoMessages[2] = () => $"�펞�\�����ݒ���J���܂�";
        }

        /// <summary>
        /// �e�X�g
        /// </summary>
        void SoundMenu()
        {
            currentMenuName = "SoundMenu";
            maxIndex = 3;
            statements[0] = () => $"�S�� < 8 >";
            statements[1] = () => $"BGM < 8 >";
            statements[2] = () => $"SE < 8 >";
            acts[0] = NoneAction;
            acts[1] = ButtonDownAct(NoneAction);
            acts[2] = ButtonDownAct(NoneAction);
            infoMessages[0] = () => $"�S�̂̉��ʂ𒲐��ł��܂�";
            infoMessages[1] = () => $"BGM�̉��ʂ𒲐��ł��܂�";
            infoMessages[2] = () => $"SE�̉��ʂ𒲐��ł��܂�";
        }

        void VideoMenu()
        {
            currentMenuName = "VideoMenu";
            maxIndex = 4;
            statements[0] = () => $"�t���X�N���[�� < ON >";
            statements[1] = () => $"�𑜓x <1920 x 1080>";
            statements[2] = () => $"VSync < 1 >";
            statements[3] = () => $"�|�X�g�G�t�F�N�g < ON >";
            acts[0] = NoneAction;
            acts[1] = ButtonDownAct(NoneAction);
            acts[2] = ButtonDownAct(NoneAction);
            acts[3] = ButtonDownAct(NoneAction);
            infoMessages[0] = () => $"�t���X�N���[���̐ݒ�";
            infoMessages[1] = () => $"�𑜓x��ύX�ł��܂�";
            infoMessages[2] = () => $"Vsync��ύX�ł��܂�";
            infoMessages[3] = () => $"�|�X�g�G�t�F�N�g";
        }

        void DisplayMenu()
        {
            currentMenuName = "DisplayMenu";
            maxIndex = 1;
            statements[0] = () => $"Show FPS < {(isShowFPS ? "ON" : "OFF")} >";
            acts[0] = () => SelectBool(ref isShowFPS);
            infoMessages[0] = () => $"FPS���펞�\���ł��܂�";
        }

        Action ButtonDownAct(Action act)
        {
            return () =>
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    EnterPage(act);
                }
            };
        }

        void SelectBool(ref bool b)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                b = !b;
            }
        }

        Action SelectIsShowFPS()
        {
            return () =>
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    isShowFPS = !isShowFPS;
                }
            };
        }

        void EnterPage(Action act)
        {
            currentAct = act;
            act();
            //�L�����Ă���
            historyStrs.Add(currentMenuName);
            historyActs.Push(currentAct);
            historyIndexes.Push(currentIndex);
            //�X�V
            currentIndex = 0;
            historyStatement = String.Join(" > ", historyStrs);
        }

        void NoneAction()
        {

        }

        void CancelPage(Action act)
        {

        }

        string boolToStr(bool b)
        {
            return b ? "ON" : "OFF";
        }

        void OpenRootPage(Action act)
        {
            currentIndex = 0;
            currentAct = act;
            act();
        }
    }
}
