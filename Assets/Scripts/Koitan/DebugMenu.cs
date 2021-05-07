using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;
using System;

namespace KoitanLib
{
    public class DebugMenu : MonoBehaviour
    {
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
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (isOpen)
                {
                    isOpen = false;
                }
                else
                {
                    isOpen = true;
                    OpenRootPage(MainMenu);
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
                if (historyActs.Count > 0)
                {
                    historyStrs.RemoveAt(historyStrs.Count - 1);
                    historyActs.Pop()();
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
                historyStatement = String.Join(" > ", historyStrs);
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
        /// 最初の画面
        /// </summary>
        void MainMenu()
        {
            currentMenuName = "MainMenu";
            maxIndex = 3;
            statements[0] = () => $"TestMenu";
            statements[1] = () => $"Time.deltaTime = {Time.deltaTime}";
            statements[2] = () => $"Time.deltaTime = {Time.deltaTime}";
            acts[0] = ButtonDownAct(() => TestMenu());
            acts[1] = ButtonDownAct(() => TestMenu());
            acts[2] = ButtonDownAct(() => TestMenu());
        }

        /// <summary>
        /// テスト
        /// </summary>
        void TestMenu()
        {
            currentMenuName = "TestMenu";
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
                    EnterPage(act);
                }
            };
        }

        void EnterPage(Action act)
        {
            //記憶しておく
            historyStrs.Add(currentMenuName);
            historyActs.Push(currentAct);
            historyIndexes.Push(currentIndex);
            currentIndex = 0;
            currentAct = act;
            act();
        }

        void OpenRootPage(Action act)
        {
            currentIndex = 0;
            currentAct = act;
            act();
        }
    }
}
