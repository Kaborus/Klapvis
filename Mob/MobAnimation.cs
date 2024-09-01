using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAnimation : MonoBehaviour
{
    private Mob mob;
    private Animator animator;

    private void Start()
    {
        mob = GetComponent<Mob>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        AnimateMovement(mob.mobController.direction);
    }

    private void AnimateMovement(Vector3 direction)
    {
        if (animator != null)
        {
            if (mob.mobController.direction.magnitude > 0)
            {
                animator.SetBool("isMoving", true);
                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    public AnimatorStateInfo GetCurrentStateInfo() => animator.GetCurrentAnimatorStateInfo(0);

    public void SetAttack(bool attackState)
    {
        mob.mobAnimation.animator.SetBool("isAttacking", attackState);
    }
}
