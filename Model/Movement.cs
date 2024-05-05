using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    
    public bool canMove = true;
    public bool isRunning = false;
    public float speed;
    private Vector3 direction;
    public Animator animator;
    private Vector3 moveDelta;
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;
    
    private void FixedUpdate()
    {
        if (canMove)
        {
            AnimateMovement(direction);
            if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDelta = new Vector3(horizontal, vertical);
        direction = new Vector3(horizontal, vertical);
        
    }

    public void EnableMovement(bool invOpen)
    {
        switch (invOpen)
        {
            case true:
                canMove = false;
                break;
            case false:
                canMove = true;
                break;
        }
    }

    void AnimateMovement(Vector3 direction)
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
