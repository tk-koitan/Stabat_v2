using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Koitan
{
    public class Money : MonoBehaviour
    {
        public float moneyValue;
        private float valueSpeed = 0;
        public int teamColorIndex;
        public SpriteRenderer outline;
        float valueMin = 1000;
        float valueMax = 10000;
        float scaleMin = 1f;
        float scaleMax = 2f;
        // Start is called before the first frame update
        void Start()
        {
            SetTeamColor(teamColorIndex);
        }

        // Update is called once per frame
        void Update()
        {
            moneyValue += valueSpeed * Time.deltaTime;
            float scale;
            if (moneyValue < valueMin)
            {
                scale = scaleMin;
            }
            else if (moneyValue > valueMax)
            {
                scale = scaleMax;
            }
            else
            {
                scale = scaleMin + (scaleMax - scaleMin) * (moneyValue - valueMin) / (valueMax - valueMin);
            }
            transform.localScale = Vector3.one * scale;
        }

        public float GetMoney()
        {
            Destroy(gameObject);
            return moneyValue;
        }

        public bool IsGetable(int teamColorIndex)
        {
            if (this.teamColorIndex < 0)
            {
                return true;
            }
            if (this.teamColorIndex == teamColorIndex)
            {
                return true;
            }
            return false;
        }

        public void SetTeamColor(int teamColorIndex)
        {
            this.teamColorIndex = teamColorIndex;
            if (teamColorIndex < 0)
            {
                outline.color = Color.white;
            }
            else
            {
                outline.color = BattleManager.ColorSets.colors[teamColorIndex];
            }
        }

        public void SetSpeed(float speed)
        {
            valueSpeed = speed;
        }

        public Rigidbody2D GetRigidbody()
        {
            return GetComponent<Rigidbody2D>();
        }
    }
}
