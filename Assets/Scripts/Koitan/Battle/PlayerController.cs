using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;
using UnityEngine.Experimental.U2D.Animation;
using Cinemachine;


namespace Koitan
{
    public class PlayerController : MonoBehaviour
    {
        public int playerIndex;
        public int teamIndex = -1;
        Animator animator;
        PlatformerMotor2D motor;
        [SerializeField]
        GameObject mesh;
        [SerializeField]
        CharaLibrarySets librarySets;
        ShopController nearShop = null;
        Bomb nearBomb = null;
        Bomb grabedBomb = null;
        [SerializeField]
        Transform handTf;
        float inoperableTime = 0f;
        // Start is called before the first frame update
        void Start()
        {
            TryGetComponent(out animator);
            TryGetComponent(out motor);
            /*
            mesh.SetActive(false);
            motor.enabled = false;
            KoitanInput.actionListWhenPlayerJoin[playerIndex] += ActionWhenPlayerJoin;
            */
        }

        // Update is called once per frame
        void Update()
        {
            //非表示なら動かさない
            if (!mesh.activeSelf) return;

            animator.SetBool("Fall", motor.IsFalling());
            animator.SetBool("Ground", motor.IsGrounded());

            //操作不能
            if (inoperableTime > 0f)
            {
                inoperableTime -= Time.deltaTime;
                motor.normalizedXMovement = 0;
                motor.normalizedYMovement = 0;
                return;
            }

            if (KoitanInput.GetStick(playerIndex).x > 0.1f)
            {
                animator.SetBool("Run", true);
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (KoitanInput.GetStick(playerIndex).x < -0.1f)
            {
                animator.SetBool("Run", true);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                animator.SetBool("Run", false);
            }

            motor.normalizedXMovement = KoitanInput.GetStick(playerIndex).x;
            motor.normalizedYMovement = KoitanInput.GetStick(playerIndex).y;

            if (KoitanInput.GetDown(ButtonCode.B, playerIndex) && motor.IsGrounded())
            {
                animator.Play("Jump");
                motor.Jump();
            }

            motor.jumpingHeld = KoitanInput.Get(ButtonCode.B, playerIndex);


            if (KoitanInput.GetDown(ButtonCode.A, playerIndex))
            {
                if (nearShop != null)
                {
                    //ショップ建設
                    nearShop.BuildShop(teamIndex);
                }
                else if (grabedBomb != null)
                {
                    animator.Play("Throw");
                    SetInoperableTime(0.25f);
                }
                else if (nearBomb != null)
                {
                    //Bomb
                    nearBomb.Pick(handTf, playerIndex);
                    grabedBomb = nearBomb;
                    nearBomb = null;
                }
            }
        }

        void ActionWhenPlayerJoin()
        {
            mesh.SetActive(true);
            motor.enabled = true;
            BattleManager.TargetGroup.AddMember(transform, 1f, 3f);
        }

        public void SetInoperableTime(float time)
        {
            inoperableTime = time;
        }

        public void ThrowBomb()
        {
            if (grabedBomb != null)
            {
                grabedBomb.Throw(new Vector3(10 * transform.localScale.x, 2, 0));
                grabedBomb = null;
            }
        }

        public void ChangeColor(int playerIndex, int teamIndex)
        {
            this.playerIndex = playerIndex;
            this.teamIndex = teamIndex;
            SpriteLibrary sl = mesh.transform.Find("body").GetComponent<SpriteLibrary>();
            sl.spriteLibraryAsset = librarySets.librarys[playerIndex];
            foreach (SpriteRenderer sr in mesh.transform.Find("outline").GetComponentsInChildren<SpriteRenderer>())
            {
                sr.color = BattleManager.ColorSets.colors[teamIndex];
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Land":
                    if (nearShop == null)
                    {
                        nearShop = collision.transform.parent.GetComponent<ShopController>();
                    }
                    break;
                case "Bomb":
                    if (nearBomb == null)
                    {
                        nearBomb = collision.transform.parent.GetComponent<Bomb>();
                    }
                    break;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Land":
                    nearShop = null;
                    break;
                case "Bomb":
                    nearBomb = null;
                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Money")
            {
                Money money = collision.collider.GetComponentInParent<Money>();
                if (money.IsGetable(teamIndex))
                {
                    BattleManager.Moneys[playerIndex] += (int)money.GetMoney();
                }
            }
        }
    }
}