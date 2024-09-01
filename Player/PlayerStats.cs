using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Player player;

    public float MaxCompletion;
    private float completion;
    public float Completion
    {
        get => completion;
        set => completion = value;
    }

    public float MaxProtection;
    private float protection;
    public float Protection
    {
        get => protection;
        set => protection = value;
    }

    public float MaxHealth;
    private float health;
    public float Health
    {
        get => health;
        set => health = value;
    }

    public float MaxStamina;
    private float stamina;
    public float Stamina
    {
        get => stamina;
        set => stamina = value;
    }

    public float MaxStrength;
    private float strength;
    public float Strength
    {
        get => strength;
        set => strength = value;
    }

    public float MaxFood;
    private float food;
    public float Food
    {
        get => food;
        set => food = value;
    }

    private float speed;
    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    [SerializeField]
    private float walkSpeed;
    public float WalkSpeed
    {
        get => walkSpeed;
    }

    private float runSpeed;
    public float RunSpeed
    {
        get => runSpeed;
        set => runSpeed = value;
    }

    public event Action OnCompletionChange;
    public event Action OnProtectionChange;
    public event Action OnHealthChange;
    public event Action OnStaminaChange;
    public event Action OnStrengthChange;
    public event Action OnFoodChange;

    private float regenerationTime = 0;
    private float starvingTime = 0;
    private float staminaIncreaseTimer = 0.0f;
    private float runTimer = 0.0f;

    public bool isRunning = false;

    private void Awake()
    {
        player = GetComponent<Player>();

        SetUpStats();
    }

    private void SetUpStats()
    {
        player.playerStats.Health = player.playerStats.MaxHealth;
        player.playerStats.Stamina = player.playerStats.MaxStamina;
        player.playerStats.Food = player.playerStats.MaxFood;
        player.playerStats.RunSpeed = player.playerStats.WalkSpeed * 2f;
    }

    private void Update()
    {
        HandleStats();
    }

    private void HandleStats()
    {
        if (player.playerStats.Food >= 0.1)
        {
            if ((player.playerStats.Health < player.playerStats.MaxHealth))
            {
                RegenerateHealth();
            }
            if (!isRunning && player.playerStats.Stamina < player.playerStats.MaxStamina)
            {
                RecoverStamina();
            }
        }
        else
        {
            StarveToDeath();
        }

        DecreaseStamina();

        if (player.playerStats.Health <= 0)
        {
            Die();
        }
    }

    private void UpdateCompletion(float amount)
    {
        player.playerStats.Completion += amount;
        OnCompletionChange?.Invoke();
    }

    public void UpdateProtection(float amount)
    {
        player.playerStats.Protection += amount;
        OnProtectionChange?.Invoke();
    }

    public void UpdateHealth(float amount)
    {
        player.playerStats.Health += amount;
        player.playerStats.Health = Mathf.Round(player.playerStats.Health * 10f) / 10f;

        if (player.playerStats.Health > player.playerStats.MaxHealth)
        {
            player.playerStats.Health = player.playerStats.MaxHealth;
        }

        OnHealthChange?.Invoke();
    }

    public void UpdateStamina(float amount)
    {
        player.playerStats.Stamina += amount;
        OnStaminaChange?.Invoke();
    }

    public void UpdateStrength(float amount)
    {
        player.playerStats.Strength += amount;
        OnStrengthChange?.Invoke();
    }

    public void UpdateFood(float amount)
    {
        player.playerStats.Food += amount;
        player.playerStats.Food = Mathf.Round(player.playerStats.Food * 10f) / 10f;

        if (player.playerStats.Food > player.playerStats.MaxFood)
        {
            player.playerStats.Food = player.playerStats.MaxFood;
        }

        OnFoodChange?.Invoke();
    }

    private void RegenerateHealth()
    {
        regenerationTime += Time.deltaTime;

        if (regenerationTime >= 0.2f)
        {
            UpdateHealth(0.2f);
            UpdateFood(-0.2f);
            regenerationTime = 0;
        }
    }

    private void StarveToDeath()
    {
        starvingTime += Time.deltaTime;

        if (starvingTime >= 0.5f)
        {
            UpdateHealth(-0.1f);
            starvingTime = 0;
        }
    }

    private void RecoverStamina()
    {
        staminaIncreaseTimer += Time.deltaTime;

        if (staminaIncreaseTimer >= 0.2f)
        {
            staminaIncreaseTimer = 0;
            UpdateStamina(1);
            UpdateFood(-0.2f);
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

        if (runTimer >= 0.2f)
        {
            runTimer = 0;
            UpdateStamina(-1);
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
