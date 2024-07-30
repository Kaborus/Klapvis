using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Slider levelSlider;
    public Slider healthSlider;
    public Slider staminaSlider;
    public Slider strengthSlider;
    public Slider foodSlider;

    public Text levelText;
    public Text healthText;
    public Text staminaText;
    public Text strengthText;
    public Text foodText;

    public Player player;
    public Movement movement;

    void Start()
    {
        SetUpSliders();
        SetUpText();

        player = FindObjectOfType<Player>();
        movement = FindObjectOfType<Movement>();

        SetUpStats();

        player.OnHealthChange += UpdateHealth;
        player.OnHealthChange += UpdateFood;

        movement.OnStaminaIncrease += UpdateFood;
        movement.OnStaminaIncrease += UpdateStamina;

        movement.OnStaminaDecrease += UpdateStamina;
    }

    private void SetUpSliders()
    {
        levelSlider = GameObject.Find("Level").GetComponent<Slider>();
        healthSlider = GameObject.Find("Health").GetComponent<Slider>();
        staminaSlider = GameObject.Find("Stamina").GetComponent<Slider>();
        strengthSlider = GameObject.Find("Strength").GetComponent<Slider>();
        foodSlider = GameObject.Find("Food").GetComponent<Slider>();
    }

    private void SetUpText()
    {
        levelText = GameObject.Find("xp text").GetComponent<Text>();
        healthText = GameObject.Find("hp text").GetComponent<Text>();
        staminaText = GameObject.Find("stamina text").GetComponent<Text>();
        strengthText = GameObject.Find("strength text").GetComponent<Text>();
        foodText = GameObject.Find("food text").GetComponent<Text>();
    }

    private void SetUpStats()
    {
        UpdateLevel();
        UpdateHealth();
        UpdateStamina();
        UpdateStrength();
        UpdateFood();
    }

    private void UpdateLevel()
    {
        levelSlider.value = 0;
        levelText.text = "0";
    }

    public void UpdateHealth()
    {
        healthSlider.value = player.health;
        healthText.text = player.health.ToString();
    }

    private void UpdateStamina()
    {
        staminaSlider.value = movement.stamina;
        staminaText.text = movement.stamina.ToString();
    }

    private void UpdateStrength()
    {
        strengthSlider.value = player.strength;
        strengthText.text = player.strength.ToString();
    }

    private void UpdateFood()
    {
        foodSlider.value = player.food;
        foodText.text = player.food.ToString();
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
