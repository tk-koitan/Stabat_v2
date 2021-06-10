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
        //[SerializeField]
        //GameObject mesh;
        CharaColorChanger charaColorChanger;
        [SerializeField]
        CharaLibrarySets librarySets;
        ShopController nearShop = null;
        Bomb nearBomb = null;
        Bomb grabedBomb = null;
        [SerializeField]
        Transform handTf;
        /// <summary>
        /// �d������
        /// </summary>
        float inoperableTime = 0f;
        /// <summary>
        /// ���G����
        /// </summary>
        float invincibleTime = 0f;
        // Start is called before the first frame update
        void Awake()
        {
            TryGetComponent(out animator);
            TryGetComponent(out motor);
            TryGetComponent(out charaColorChanger);
            /*
            mesh.SetActive(false);
            motor.enabled = false;
            KoitanInput.actionListWhenPlayerJoin[playerIndex] += ActionWhenPlayerJoin;
            */
        }

        // Update is called once per frame
        void Update()
        {
            //��\���Ȃ瓮�����Ȃ�
            //if (!mesh.activeSelf) return;

            animator.SetBool("Fall", motor.IsFalling());
            animator.SetBool("Ground", motor.IsGrounded());
            animator.SetBool("Damage", IsInoperable() && IsInvincible());

            //���G����
            if (invincibleTime > 0f)
            {
                invincibleTime -= Time.deltaTime;

            }

            //����s�\
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
                    //�V���b�v����
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

        /// <summary>
        /// ����s�\����
        /// </summary>
        /// <param name="time"></param>
        public void SetInoperableTime(float time)
        {
            inoperableTime = time;
        }

        /// <summary>
        /// ���G����
        /// </summary>
        /// <param name="time"></param>
        public void SetInvincibleTime(float time)
        {
            if (time > invincibleTime)
            {
                invincibleTime = time;
                charaColorChanger.SetFlashTime(time);
            }
        }

        public bool IsInoperable()
        {
            return inoperableTime > 0f;
        }

        public bool IsInvincible()
        {
            return invincibleTime > 0f;
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
            charaColorChanger.ChangeColor(playerIndex, teamIndex);
        }

        public void AddPowerVec(Vector2 vec)
        {
            motor.velocity = vec;
        }

        public void SetDamage(Vector2 vec, float time)
        {
            // ���G�ł���Ζ���
            if (IsInvincible()) return;
            AddPowerVec(vec);
            SetInoperableTime(time);
            //�Ƃ肠������{�̖��G����
            SetInvincibleTime(time * 2f);
            //�Ƃ肠�����A�j���[�V����
            animator.Play("Damage");
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