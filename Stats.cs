using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public Slider levelSlider;
    public Slider hitpointsSlider;
    public Slider staminaSlider;
    public Slider foodSlider;
    public Slider strengthSlider;
    public Movement movement;
    private float regenTime = 0.0f;
    private float runTime = 0.0f;
    //public GameObject player;

    private float level;
    //private float maxLevel = 100f;
    private float health;
    private float maxHealth = 100f;
    private float stamina;
    private float maxStamina = 100f;
    private float food;
    private float maxFood = 100f;
    public float strength;
    //private float maxStrength = 100f;

    public Text levelText;
    public Text healthText;
    public Text staminaText;
    public Text foodText;
    public Text strengthText;

    void Start()
    {
        movement = FindObjectOfType<Movement>();
        movement.speed = 2f;

        levelSlider.value = level;
        levelText.text = level.ToString();

        health = maxHealth;
        hitpointsSlider.value = health;
        healthText.text = health.ToString();

        stamina = maxStamina;
        staminaSlider.value = stamina;
        staminaText.text = stamina.ToString();

        food = maxFood;
        foodSlider.value = food;
        foodText.text = food.ToString();

        strength = 10f;
        strengthSlider.value = strength;
        strengthText.text = strength.ToString();
    }

    void Update()
    {
        health = hitpointsSlider.value;
        //Debug.Log(health);
        //Debug.Log(food);
        if (movement != null)
        {
            UpdateHealth();
        }
        if (movement != null)
        {
            UpdateStamina();
        }
    }

    public void UpdateHealth()
    {
        if (health < 100 && food > 0)
        {
            //Debug.Log(regenTime);
            regenTime += Time.deltaTime;
            if (regenTime >= 1)
            {
                health += 0.1f;
                food -= 0.1f;
                regenTime = 0;
            }
        }
        healthText.text = health.ToString();
        hitpointsSlider.value = health;
        foodText.text = food.ToString();
        foodSlider.value = food;
    }

    public void UpdateStamina()
    {
        if (stamina > 0 && movement.isRunning)
        {
            movement.speed = 4f;
            runTime += Time.deltaTime;
            if (runTime >= 1)
            {
                stamina--;
                food -= 0.1f;
                runTime = 0;
            }
        }
        else
        {
            movement.speed = 2f;
            if (stamina < 100)
            {
                runTime += Time.deltaTime;
                if (runTime >= 1)
                {
                    stamina += 5;
                    runTime = 0;
                    if (stamina > 100)
                    {
                        stamina = 100;
                    }
                }
            }
        }
        if (stamina <= 0)
        {
            movement.speed = 2f;
        }
        staminaText.text = stamina.ToString();
        staminaSlider.value = stamina;
        foodText.text = food.ToString();
        foodSlider.value = food;
    }
}
