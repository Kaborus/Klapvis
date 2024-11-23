using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Player player;
    public bool canMove = true;
    public Vector3 direction;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (canMove)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            direction = new Vector3(horizontal, vertical).normalized;


            if (Input.GetKey(KeyCode.LeftShift) && direction != Vector3.zero && player.stats.Stamina > 0)
            {
                player.stats.Speed = player.stats.RunSpeed;
                player.stats.isRunning = true;
            }
            else
            {
                player.stats.Speed = player.stats.WalkSpeed;
                player.stats.isRunning = false;
            }
        }
    }

    private void HandleMovement()
    {
        if (canMove && direction != Vector3.zero)
        {
            transform.position += direction * player.stats.Speed * Time.deltaTime;
        }
    }

    public void EnableMovement(bool enable)
    {
        canMove = enable;
    }
}
