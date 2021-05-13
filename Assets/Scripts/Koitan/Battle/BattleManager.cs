using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;

namespace Koitan
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField]
        GameObject[] players;
        static BattleManager instance;
        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public static GameObject GetPlayer(int index)
        {
            return instance.players[index];
        }
    }
}
