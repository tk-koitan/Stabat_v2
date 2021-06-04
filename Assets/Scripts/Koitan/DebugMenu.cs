using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;
using Koitan;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
        int[,] resolutions = {
            {640,360},
            {720,405},
            {960,540},
            {1280,720},
            {1440,810},
            {1920,1080},
        };
        int resIndex = 3;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
            TryGetComponent(out fpsCounter);
            isShowFPS = true;
            Screen.SetResolution(resolutions[resIndex, 0], resolutions[resIndex, 1], Screen.fullScreen);
        }

        // Update is called once per frame
        void Update()
        {
            //�펞�\��
            if (isShowFPS)
            {
                KoitanDebug.Display($"FPS {fpsCounter.fps}\n");
            }

            if (Input.GetKeyDown(KeyCode.Tab) || KoitanInput.GetDown(ButtonCode.Select))
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
            if (Input.GetKeyDown(KeyCode.UpArrow) || KoitanInput.GetDown(ButtonCode.Up))
            {
                currentIndex = (currentIndex - 1 + maxIndex) % maxIndex;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || KoitanInput.GetDown(ButtonCode.Down))
            {
                currentIndex = (currentIndex + 1 + maxIndex) % maxIndex;
            }
            //�L�����Z��
            if (Input.GetKeyDown(KeyCode.X) || KoitanInput.GetDown(ButtonCode.B))
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
            for (int i = 0; i < 8; i++)
            {
                if (i == currentIndex)
                {
                    KoitanDebug.Display($"��{statements[i]()}\n");
                }
                else if (i < maxIndex)
                {
                    KoitanDebug.Display($"�@{statements[i]()}\n");
                }
                else
                {
                    KoitanDebug.Display($"\n");
                }
            }
            KoitanDebug.Display($"INFO...{infoMessages[currentIndex]()}\n");
        }

        /// <summary>
        /// �ŏ��̉��
        /// </summary>
        void MainMenu()
        {
            currentMenuName = "�f�o�b�O���j���[";
            maxIndex = 5;
            statements[0] = () => $"�T�E���h";
            statements[1] = () => $"�f�B�X�v���C";
            statements[2] = () => $"�펞�\�����";
            statements[3] = () => $"�o�g��";
            statements[4] = () => $"�R���g���[���[�ݒ�";
            acts[0] = ButtonDownAct(SoundMenu);
            acts[1] = ButtonDownAct(VideoMenu);
            acts[2] = ButtonDownAct(DisplayMenu);
            acts[3] = ButtonDownAct(BattleMenu);
            acts[4] = ButtonDownAct(ControllerMenu);
            infoMessages[0] = () => $"�T�E���h�ݒ���J���܂�";
            infoMessages[1] = () => $"�f�B�X�v���C�ݒ���J���܂�";
            infoMessages[2] = () => $"�펞�\�����ݒ���J���܂�";
            infoMessages[3] = () => $"�o�g���ݒ���J���܂�";
            infoMessages[4] = () => $"�R���g���[���[�ݒ���J���܂�";
            //�ǉ��ł��Ȃ��悤��
            PlayerInputManager.instance.DisableJoining();
        }


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
            currentMenuName = "�f�B�X�v���C�ݒ�";
            maxIndex = 4;
            statements[0] = () => $"�t���X�N���[�� < {Screen.fullScreen} >";
            statements[1] = () => $"�𑜓x < {resolutions[resIndex, 0]} x {resolutions[resIndex, 1]} >";
            statements[2] = () => $"VSync < {QualitySettings.vSyncCount} >";
            statements[3] = () => $"�|�X�g�G�t�F�N�g < ON >";
            acts[0] = SelectFullScreen;
            acts[1] = SelectResolution;
            acts[2] = SelectVSync;
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

        void BattleMenu()
        {
            currentMenuName = "�o�g�����j���[";
            maxIndex = 3 + BattleSetting.playerCount;
            statements[0] = () => $"�o�g���J�n";
            statements[1] = () => $"�l�� < {BattleSetting.playerCount} >";
            statements[2] = () => $"�����ݒ�";
            acts[0] = ButtonDownActNoHistory(BattleStart);
            acts[1] = () =>
            {
                SelectInt(ref BattleSetting.playerCount, 2, BattleGlobal.MaxPlayerNum);
                maxIndex = 3 + BattleSetting.playerCount;
            };
            acts[2] = ButtonDownAct(BattleAutoSetting);
            infoMessages[0] = () => $"�o�g�����J�n���܂�";
            infoMessages[1] = () => $"�l����ݒ肵�܂�";
            infoMessages[2] = () => $"���݂̃R���g���[���[�̐l�Ԑݒ�A�c���CPU�Ŗ��߂܂�";

            for (int i = 0; i < BattleGlobal.MaxPlayerNum; i++)
            {
                int tmpI = i;
                statements[i + 3] = () => $"Player{tmpI}, {(ControllPlayer)BattleSetting.ControllPlayers[tmpI]}, {BattleSetting.teamColorIndexes[tmpI]}, {BattleSetting.playerIndexes[tmpI]}, {BattleSetting.charaColorIndexes[tmpI]}";
                acts[i + 3] = ButtonDownAct(PlayerSetting(tmpI));
                infoMessages[i + 3] = () => $"Player{tmpI}�̐ݒ��ύX���܂�";
            }
        }

        void ControllerMenu()
        {
            currentMenuName = "�R���g���[���[�ݒ�";
            maxIndex = 1;
            statements[0] = () => $"�R���g���[���[���Ȃ�����";
            acts[0] = ButtonDownActNoHistory(KoitanInput.ClearAllController);
            infoMessages[0] = () => $"���̉�ʂŃ{�^���������ƃR���g���[���[�̓o�^���ł��܂�";
            //�R���g���[���[�̎�t
            PlayerInputManager.instance.EnableJoining();
        }

        Action PlayerSetting(int index)
        {
            return () =>
            {
                currentMenuName = $"Player{index}Setting";
                maxIndex = 4;
                statements[0] = () => $"����� < {(ControllPlayer)BattleSetting.ControllPlayers[index]} >";
                statements[1] = () => $"�`�[�� < {BattleSetting.teamColorIndexes[index]} >";
                statements[2] = () => $"�v���C���[�ԍ� < {BattleSetting.playerIndexes[index]} >";
                statements[3] = () => $"�L�����J���[ < {BattleSetting.charaColorIndexes[index]} >";
                acts[0] = () => SelectInt(ref BattleSetting.ControllPlayers[index], 0, 2);
                acts[1] = () => SelectInt(ref BattleSetting.teamColorIndexes[index], 0, BattleGlobal.MaxPlayerNum - 1);
                acts[2] = () => SelectInt(ref BattleSetting.playerIndexes[index], 0, BattleGlobal.MaxPlayerNum - 1);
                acts[3] = () => SelectInt(ref BattleSetting.charaColorIndexes[index], 0, BattleGlobal.MaxPlayerNum - 1);
                infoMessages[0] = () => $"���삷��l��ύX�ł��܂�";
                infoMessages[1] = () => $"�`�[����ύX�ł��܂�";
                infoMessages[2] = () => $"�v���C���[�ԍ���ύX�ł��܂�";
                infoMessages[3] = () => $"�L�����J���[��ύX�ł��܂�";
            };
        }

        Action ButtonDownAct(Action act)
        {
            return () =>
            {
                if (Input.GetKeyDown(KeyCode.Z) || KoitanInput.GetDown(ButtonCode.A))
                {
                    EnterPage(act);
                }
            };
        }

        Action ButtonDownActNoHistory(Action act)
        {
            return () =>
            {
                if (Input.GetKeyDown(KeyCode.Z) || KoitanInput.GetDown(ButtonCode.A))
                {
                    act();
                }
            };
        }

        void BattleStart()
        {
            KoitanInput.ClearAllCPU();
            SceneManager.LoadScene("BattleScene");
        }

        void BattleAutoSetting()
        {
            for (int i = 0; i < BattleSetting.playerCount; i++)
            {
                if (i < KoitanInput.GetControllerNum())
                {
                    BattleSetting.ControllPlayers[i] = 1;
                }
                else
                {
                    BattleSetting.ControllPlayers[i] = 2;
                }
                BattleSetting.playerIndexes[i] = i;
                BattleSetting.teamColorIndexes[i] = i;
                BattleSetting.charaColorIndexes[i] = i;
            }
        }

        void SelectInt(ref int i, int min, int max)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || KoitanInput.GetDown(ButtonCode.Left))
            {
                i--;
                if (i < min)
                {
                    i = max;
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || KoitanInput.GetDown(ButtonCode.Right))
            {
                i++;
                if (i > max)
                {
                    i = min;
                }
            }
        }

        void SelectBool(ref bool b)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || KoitanInput.GetDown(ButtonCode.Left) || KoitanInput.GetDown(ButtonCode.Right))
            {
                b = !b;
            }
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

        void SelectFullScreen()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || KoitanInput.GetDown(ButtonCode.Left) || KoitanInput.GetDown(ButtonCode.Right))
            {
                Screen.fullScreen = !Screen.fullScreen;
            }
        }

        void SelectResolution()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || KoitanInput.GetDown(ButtonCode.Left))
            {
                resIndex--;
                if (resIndex < 0)
                {
                    resIndex = 5;
                }
                Screen.SetResolution(resolutions[resIndex, 0], resolutions[resIndex, 1], Screen.fullScreen);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || KoitanInput.GetDown(ButtonCode.Right))
            {
                resIndex++;
                if (resIndex > 5)
                {
                    resIndex = 0;
                }
                Screen.SetResolution(resolutions[resIndex, 0], resolutions[resIndex, 1], Screen.fullScreen);

            }
        }

        void SelectVSync()
        {
            int vsync = QualitySettings.vSyncCount;
            if (Input.GetKeyDown(KeyCode.LeftArrow) || KoitanInput.GetDown(ButtonCode.Left))
            {
                vsync--;
                if (vsync < 0)
                {
                    vsync = 3;
                }
                QualitySettings.vSyncCount = vsync;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || KoitanInput.GetDown(ButtonCode.Right))
            {
                vsync++;
                if (vsync > 3)
                {
                    vsync = 0;
                }
                QualitySettings.vSyncCount = vsync;
            }
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
