using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;
using Cinemachine;
using UnityEngine.SceneManagement;

namespace Koitan
{
    public class BattleManager : MonoBehaviour
    {
        List<PlayerController> players = new List<PlayerController>();
        public static List<PlayerController> Players => instance.players;
        int[] moneys = new int[BattleGlobal.MaxPlayerNum];
        public static int[] Moneys => instance.moneys;
        [SerializeField]
        CinemachineTargetGroup targetGroup;
        [SerializeField]
        Money moneyPrefab;
        //この書き方超便利
        public static CinemachineTargetGroup TargetGroup => instance.targetGroup;
        public static BattleManager instance { private set; get; }
        [SerializeField]
        ColorSets colorSets;
        public static ColorSets ColorSets => instance.colorSets;
        [SerializeField]
        PlayerController charaPrefab;
        [SerializeField]
        ControllerInput ai;
        [SerializeField]
        Transform[] initPositions;
        [SerializeField]
        GameObject[] items;
        [SerializeField]
        float stageWidth;
        [SerializeField]
        float stageHeight;
        [SerializeField]
        float intervalTime;
        float itemCreateTime;
        [SerializeField]
        ShopController[] shops;
        public static ShopController[] Shops => instance.shops;
        public static List<Money> moneyInstances = new List<Money>();
        // Start is called before the first frame update
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                for (int i = 0; i < BattleGlobal.MaxPlayerNum; i++)
                {
                    if (BattleSetting.ControllPlayers[i] == 0) continue;
                    PlayerController player = Instantiate(charaPrefab, initPositions[i].position, Quaternion.identity);
                    player.ChangeColor(BattleSetting.playerIndexes[i], BattleSetting.teamColorIndexes[i]);
                    if (BattleSetting.ControllPlayers[i] == 2)
                    {
                        Instantiate(ai);
                    }
                    players.Add(player);
                    targetGroup.AddMember(player.transform, 1f, 3f);

                    moneys[i] = 1000;
                }
                /*
                for (int i = 0; i < KoitanInput.GetControllerNum(); i++)
                {
                    PlayerController player = Instantiate(charaPrefab);
                    player.ChangeColor(i, i);
                    players.Add(player.gameObject);
                    targetGroup.AddMember(player.transform, 1f, 3f);
                }
                */
            }
            else
            {
                Destroy(this);
            }
        }

        private void OnDestroy()
        {
            instance = null;
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < players.Count; i++)
            {
                KoitanDebug.DisplayBox($"{moneys[i]}", players[i]);
            }
            KoitanDebug.Display($"MoneyInstances.Count = {moneyInstances.Count}");
            itemCreateTime += Time.deltaTime;
            if (itemCreateTime > intervalTime)
            {
                itemCreateTime = 0;
                CreateItem();
            }

        }

        void CreateItem()
        {
            GameObject item = items[Random.Range(0, items.Length)];
            //100回で打ち切り
            for (int i = 0; i < 100; i++)
            {
                Vector2 pos = new Vector2(Random.Range(-stageWidth / 2, stageWidth / 2), Random.Range(-stageHeight / 2, stageHeight / 2));
                RaycastHit2D hit;
                hit = Physics2D.BoxCast(pos, Vector2.one, 0, Vector2.zero);
                if (!hit)
                {
                    Instantiate(item, pos, Quaternion.identity);
                    break;
                }
            }
        }

        /// <summary>
        /// お金を生成する
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static Money CreateMoney(Vector3 pos)
        {
            Money tmp = Instantiate(instance.moneyPrefab, pos, Quaternion.identity);
            moneyInstances.Add(tmp);
            return tmp;
        }

        /// <summary>
        /// お金を破棄する
        /// </summary>
        /// <param name="money"></param>
        public static void DestroyMoney(Money money)
        {
            moneyInstances.Remove(money);
        }

        public static void StartBattle()
        {
            KoitanInput.ClearAllCPU();
            SceneManager.LoadScene(BattleGlobal.stageSceneNames[BattleSetting.battleStageIndex]);
        }

        private void OnDrawGizmosSelected()
        {
            GizmosExtensions2D.DrawWireRect2D(Vector3.zero, stageWidth, stageHeight);
        }
    }
}
