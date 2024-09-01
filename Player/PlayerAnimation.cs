using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;
    public Animator animator;

    private void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        animator.SetFloat("vertical", -1);
    }

    private void FixedUpdate()
    {
        AnimateMovement(player.playerMovement.direction);
    }

    public void AnimateMovement(Vector3 direction)
    {
        if (animator != null)
        {
            if (direction.magnitude > 0)
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
}
