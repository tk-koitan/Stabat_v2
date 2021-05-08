using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;
using System;

namespace KoitanLib
{
    public class DebugMenu : MonoBehaviour
    {
#if KOITAN_DEBUG
        //メニューを開いているか
        bool isOpen = false;
        //メニューの基本構成
        //文章
        Func<string>[] statements = new Func<string>[16];
        //アクション
        Action[] acts = new Action[16];
        int maxIndex = 16;
        int currentIndex = 0;
        Action currentAct;
        string currentMenuName;
        //履歴
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
        }

        // Update is called once per frame
        void Update()
        {
            //常時表示
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

            //カーソル移動
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentIndex = (currentIndex - 1 + maxIndex) % maxIndex;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentIndex = (currentIndex + 1 + maxIndex) % maxIndex;
            }
            //キャンセル
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (historyActs.Count > 1)
                {
                    historyStrs.RemoveAt(historyStrs.Count - 1);
                    historyStatement = String.Join(" > ", historyStrs);
                    //現在のページの履歴を消す
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
                //実行　
                acts[currentIndex]();
            }

            //描画            
            if (historyStrs.Count > 0)
            {
                //毎フレーム計算するのはよくない
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
        }

        /// <summary>
        /// 最初の画面
        /// </summary>
        void MainMenu()
        {
            currentMenuName = "MainMenu";
            maxIndex = 3;
            statements[0] = () => $"SoundMenu";
            statements[1] = () => $"VideoMenu";
            statements[2] = () => $"DisplayMenu";
            acts[0] = ButtonDownAct(() => SoundMenu());
            acts[1] = ButtonDownAct(() => VideoMenu());
            acts[2] = ButtonDownAct(() => DisplayMenu());
        }

        /// <summary>
        /// テスト
        /// </summary>
        void SoundMenu()
        {
            currentMenuName = "SoundMenu";
            maxIndex = 2;
            statements[0] = () => $"MainMennu";
            statements[1] = () => $"VideoMennu";
            acts[0] = ButtonDownAct(() => MainMenu());
            acts[1] = ButtonDownAct(() => VideoMenu());
        }

        void VideoMenu()
        {
            currentMenuName = "VideoMenu";
            maxIndex = 2;
            statements[0] = () => $"MainMennu";
            statements[1] = () => $"SoundMennu";
            acts[0] = ButtonDownAct(() => MainMenu());
            acts[1] = ButtonDownAct(() => SoundMenu());
        }

        void DisplayMenu()
        {
            currentMenuName = "DisplayMenu";
            maxIndex = 1;
            statements[0] = () => $"Show FPS < {(isShowFPS ? "ON" : "OFF")} >";
            acts[0] = SelectIsShowFPS();
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
            //記憶しておく
            historyStrs.Add(currentMenuName);
            historyActs.Push(currentAct);
            historyIndexes.Push(currentIndex);
            //更新
            currentIndex = 0;
            historyStatement = String.Join(" > ", historyStrs);
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
#endif
    }
}
