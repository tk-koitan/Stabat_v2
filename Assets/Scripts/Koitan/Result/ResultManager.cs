using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Koitan
{
    public class ResultManager : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI[] moneyTexts;
        [SerializeField]
        TextMeshProUGUI[] rankTexts;
        [SerializeField]
        GameObject[] moneyUis;
        int moneyMax = 0;
        List<int[]> sortRank = new List<int[]>();
        // Start is called before the first frame update
        void Start()
        {
            //UIの表示
            for (int i = 0; i < BattleGlobal.MaxPlayerNum; i++)
            {
                if (i < Result.playerCount)
                {
                    moneyUis[i].SetActive(true);
                    moneyUis[i].transform.localPosition = new Vector3(1920 / Result.playerCount * (i + 0.5f) - 960, -420);
                    moneyTexts[i].text = $"{Result.playerMoneys[i]}G";
                    moneyMax = Mathf.Max(moneyMax, Result.playerMoneys[i]);
                    sortRank.Add(new int[] { i, Result.playerMoneys[i] });
                }
                else
                {
                    moneyUis[i].SetActive(false);
                }
            }
            // ソート
            sortRank.Sort((a, b) => b[1] - a[1]);
            for (int i = 0; i < Result.playerCount; i++)
            {
                rankTexts[sortRank[i][0]].text = $"{i + 1}";
                rankTexts[sortRank[i][0]].transform.localScale = Vector3.zero;
                rankTexts[sortRank[i][0]].transform.DOScale(3f - i * 0.75f, 1).SetDelay(i * 0.25f + 2);
            }
            // 結果の座標を計算する
            // 順位が1位の人が基準
            for (int i = 0; i < Result.playerCount; i++)
            {
                moneyUis[i].transform.DOMoveY((float)Result.playerMoneys[i] / moneyMax * 500f, 2f).SetRelative();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
