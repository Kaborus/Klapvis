using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    public event Action OnStaminaIncrease;
    public event Action OnStaminaDecrease;
    public Player player;
    public float maxStamina;
    public float stamina;
    public float staminaIncreaseTimer = 0.0f;
    public float runTimer = 0.0f;
    public bool canMove = true;
    public bool isRunning = false;
    public float speed;
    public float walkSpeed;
    private float runSpeed;
    private Vector3 direction;
    public Animator animator;
    private Vector3 moveDelta;
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;

    private void Awake()
    {
        stamina = maxStamina;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        runSpeed = walkSpeed * 2;

    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        AnimateMovement(direction);
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && stamina > 0)
        {
            speed = runSpeed;
            isRunning = true;
        }
        else
        {
            speed = walkSpeed;
            isRunning = false;
        }
        transform.position += direction * speed * Time.deltaTime;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDelta = new Vector3(horizontal, vertical);
        direction = new Vector3(horizontal, vertical);


        IncreaseStamina();
        DecreaseStamina();

    }

    private void IncreaseStamina()
    {
        if (!isRunning && stamina < maxStamina)
        {
            staminaIncreaseTimer += Time.deltaTime;
        }

        if (staminaIncreaseTimer >= 1)
        {
            staminaIncreaseTimer = 0;
            stamina += 5;
            player.DrainFood();

            OnStaminaIncrease?.Invoke();

        }
    }

    private void DecreaseStamina()
    {
        if (isRunning)
        {
            runTimer += Time.deltaTime;
        }
        else
        {
            runTimer = 0;
        }

        if (runTimer >= 1)
        {
            runTimer = 0;
            stamina -= 5;
            OnStaminaDecrease?.Invoke();
        }
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
