using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Slider completionSlider;
    public Slider protectionSlider;
    public Slider healthSlider;
    public Slider staminaSlider;
    public Slider strengthSlider;
    public Slider foodSlider;

    public Text completionText;
    public Text protectionText;
    public Text healthText;
    public Text staminaText;
    public Text strengthText;
    public Text foodText;

    public Player player;

    void Start()
    {
        SetUpSliders();
        SetUpText();

        player = FindObjectOfType<Player>();

        SetUpStats();

        SubscribeEvents();
    }

    private void SetUpSliders()
    {
        completionSlider = GameObject.Find("Completion").GetComponent<Slider>();
        protectionSlider = GameObject.Find("Loadout").GetComponent<Slider>();
        healthSlider = GameObject.Find("Health").GetComponent<Slider>();
        staminaSlider = GameObject.Find("Stamina").GetComponent<Slider>();
        strengthSlider = GameObject.Find("Strength").GetComponent<Slider>();
        foodSlider = GameObject.Find("Food").GetComponent<Slider>();
    }

    private void SetUpText()
    {
        completionText = GameObject.Find("xp text").GetComponent<Text>();
        protectionText = GameObject.Find("loadout text").GetComponent<Text>();
        healthText = GameObject.Find("hp text").GetComponent<Text>();
        staminaText = GameObject.Find("stamina text").GetComponent<Text>();
        strengthText = GameObject.Find("strength text").GetComponent<Text>();
        foodText = GameObject.Find("food text").GetComponent<Text>();
    }

    private void SubscribeEvents()
    {
        player.playerStats.OnCompletionChange += UpdateCompletion;
        player.playerStats.OnProtectionChange += UpdateProtection;
        player.playerStats.OnHealthChange += UpdateHealth;
        player.playerStats.OnStaminaChange += UpdateStamina;
        player.playerStats.OnStrengthChange += UpdateStrength;
        player.playerStats.OnFoodChange += UpdateFood;
    }

    private void SetUpStats()
    {
        UpdateCompletion();
        UpdateProtection();
        UpdateHealth();
        UpdateStamina();
        UpdateStrength();
        UpdateFood();
    }

    private void UpdateSlider(Slider slider, Text text, float stat)
    {
        slider.value = stat;
        text.text = stat.ToString();
    }

    private void UpdateCompletion()
    {
        completionSlider.value = 0;
        completionText.text = "0";
    }

    public void UpdateProtection()
    {
        UpdateSlider(protectionSlider, protectionText, player.playerStats.Protection);
    }

    public void UpdateHealth()
    {
        UpdateSlider(healthSlider, healthText, player.playerStats.Health);
    }

    private void UpdateStamina()
    {
        UpdateSlider(staminaSlider, staminaText, player.playerStats.Stamina);
    }

    private void UpdateStrength()
    {
        UpdateSlider(strengthSlider, strengthText, player.playerStats.Strength);
    }

    private void UpdateFood()
    {
        UpdateSlider(foodSlider, foodText, player.playerStats.Food);
    }

    public void DecreaseHealth(Mob mob)
    {
        UpdateHealth();
    }

    public void IncreaseStrength(Mob mob)
    {
        UpdateStrength();
    }
}
