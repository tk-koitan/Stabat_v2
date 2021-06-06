using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;

namespace Koitan
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField]
        Transform landParent;
        [SerializeField]
        Transform shopParent;
        [SerializeField]
        SpriteRenderer shopOutline;
        [SerializeField]
        Transform moneyInitTf;
        public int playerIndex;
        public int teamIndex;
        [SerializeField]
        Money moneyPrefab;
        Money moneyInstance;
        bool isBuild;
        float moneyLostingTime = 0;
        float moneyCreateIntevalTime = 1f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isBuild)
            {
                if (moneyInstance == null)
                {
                    moneyLostingTime += Time.deltaTime;
                    if (moneyLostingTime >= moneyCreateIntevalTime)
                    {
                        CreateMoney();
                        moneyLostingTime = 0;
                    }
                }
                if (KoitanInput.GetDown(ButtonCode.X))
                {
                    BrokenShop();
                }
            }
        }

        public void BuildShop(int teamIndex)
        {
            isBuild = true;
            moneyLostingTime = 0;
            landParent.gameObject.SetActive(false);
            shopParent.gameObject.SetActive(true);
            shopOutline.color = BattleManager.ColorSets.colors[teamIndex];
            this.teamIndex = teamIndex;
            CreateMoney();
        }

        public void BrokenShop()
        {
            isBuild = false;
            landParent.gameObject.SetActive(true);
            shopParent.gameObject.SetActive(false);
            if (moneyInstance != null)
            {
                moneyInstance.SetTeamColor(-1);
                moneyInstance.GetRigidbody().isKinematic = false;
                moneyInstance.SetSpeed(0);
                moneyInstance.transform.SetParent(null);
            }
        }

        public void CreateMoney()
        {
            moneyInstance = Instantiate(moneyPrefab, moneyInitTf.position, Quaternion.identity);
            moneyInstance.SetTeamColor(teamIndex);
            moneyInstance.GetRigidbody().isKinematic = true;
            moneyInstance.SetSpeed(500);
            moneyInstance.transform.SetParent(transform);
        }
    }
}
