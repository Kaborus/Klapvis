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

    public float MaxColdProtection;
    private float coldProtection;
    public float ColdProtection
    {
        get => coldProtection;
        set => coldProtection = value;
    }

    public float MaxHeatProtection;
    private float heatProtection;
    public float HeatProtection
    {
        get => heatProtection;
        set => heatProtection = value;
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
    public event Action OnColdProtectionChange;
    public event Action OnHeatProtectionChange;
    public event Action OnHealthChange;
    public event Action OnStaminaChange;
    public event Action OnStrengthChange;
    public event Action OnFoodChange;

    public bool canRegenerate = true;

    private float regenerateTimer = 0.0f;

    private float regenerationTime = 0;
    private float healthLoseTime = 0;
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
        player.stats.Health = player.stats.MaxHealth;
        player.stats.Stamina = player.stats.MaxStamina;
        player.stats.Food = player.stats.MaxFood;
        player.stats.RunSpeed = player.stats.WalkSpeed * 2f;
    }

    private void Update()
    {
        HandleStats();
    }

    private void HandleStats()
    {
        if (!canRegenerate)
        {
            regenerateTimer += Time.deltaTime;

            if (regenerateTimer >= 1.0f)
            {
                canRegenerate = true;
                regenerateTimer = 0.0f;
            }
        }

        if (player.stats.Food >= 0.1)
        {
            if ((player.stats.Health < player.stats.MaxHealth))
            {
                RegenerateHealth();
            }
            if (!isRunning && player.stats.Stamina < player.stats.MaxStamina)
            {
                RecoverStamina();
            }
        }
        else
        {
            LoseHealth(-0.1f);
        }

        DecreaseStamina();

        if (player.stats.Health <= 0)
        {
            Die();
        }
    }

    private void UpdateCompletion(float amount)
    {
        player.stats.Completion += amount;
        OnCompletionChange?.Invoke();
    }

    public void UpdateProtection(float amount)
    {
        player.stats.Protection += amount;
        OnProtectionChange?.Invoke();
    }

    public void UpdateColdProtection(float amount)
    {
        player.stats.ColdProtection += amount;
        OnColdProtectionChange?.Invoke();
    }

    public void UpdateHeatProtection(float amount)
    {
        player.stats.HeatProtection += amount;
        OnHeatProtectionChange?.Invoke();
    }

    public void UpdateHealth(float amount)
    {
        player.stats.Health += amount;
        player.stats.Health = Mathf.Round(player.stats.Health * 10f) / 10f;

        if (player.stats.Health > player.stats.MaxHealth)
        {
            player.stats.Health = player.stats.MaxHealth;
        }

        OnHealthChange?.Invoke();
    }

    public void UpdateStamina(float amount)
    {
        player.stats.Stamina += amount;
        OnStaminaChange?.Invoke();
    }

    public void UpdateStrength(float amount)
    {
        player.stats.Strength += amount;
        OnStrengthChange?.Invoke();
    }

    public void UpdateFood(float amount)
    {
        player.stats.Food += amount;
        player.stats.Food = Mathf.Round(player.stats.Food * 10f) / 10f;

        if (player.stats.Food > player.stats.MaxFood)
        {
            player.stats.Food = player.stats.MaxFood;
        }

        OnFoodChange?.Invoke();
    }

    private void RegenerateHealth()
    {
        if (!canRegenerate) return;

        regenerationTime += Time.deltaTime;

        if (regenerationTime >= 0.2f)
        {
            UpdateHealth(1.0f);
            UpdateFood(-0.2f);
            regenerationTime = 0;
        }
    }

    public void LoseHealth(float amount)
    {
        canRegenerate = false;
        regenerateTimer = 0.0f;
        healthLoseTime += Time.deltaTime;

        if (healthLoseTime >= 0.5f)
        {
            UpdateHealth(amount);
            healthLoseTime = 0;
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
