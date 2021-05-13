using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;
using Cinemachine;


namespace Koitan
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        int playerIndex;
        Animator animator;
        PlatformerMotor2D motor;
        [SerializeField]
        GameObject mesh;
        [SerializeField]
        CinemachineTargetGroup targetGroup;
        // Start is called before the first frame update
        void Start()
        {
            TryGetComponent(out animator);
            TryGetComponent(out motor);
            mesh.SetActive(false);
            motor.enabled = false;
            KoitanInput.actionListWhenPlayerJoin[playerIndex] += ActionWhenPlayerJoin;
        }

        // Update is called once per frame
        void Update()
        {
            //”ñ•\Ž¦‚È‚ç“®‚©‚³‚È‚¢
            if (!mesh.activeSelf) return;

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

            animator.SetBool("Fall", motor.IsFalling());
            animator.SetBool("Ground", motor.IsGrounded());
        }

        void ActionWhenPlayerJoin()
        {
            mesh.SetActive(true);
            motor.enabled = true;
            targetGroup.AddMember(transform, 1f, 3f);
        }
    }
}