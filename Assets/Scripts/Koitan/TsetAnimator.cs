using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;

public class TsetAnimator : MonoBehaviour
{
    [SerializeField]
    int playerIndex;
    Animator animator;
    PlatformerMotor2D motor;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out animator);
        TryGetComponent(out motor);
    }

    // Update is called once per frame
    void Update()
    {
        if (KoitanInput.Get(ButtonCode.Right, playerIndex))
        {
            animator.SetBool("Run", true);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (KoitanInput.Get(ButtonCode.Left, playerIndex))
        {
            animator.SetBool("Run", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if (KoitanInput.GetDown(ButtonCode.B, playerIndex) && motor.IsGrounded())
        {
            animator.Play("Jump");
        }

        animator.SetBool("Fall", motor.IsFalling());
        animator.SetBool("Ground", motor.IsGrounded());
    }
}
