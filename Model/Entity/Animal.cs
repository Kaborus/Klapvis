using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animal : MonoBehaviour
{
    public MenuManager menuManager;
    public Movement movement;
    public GameObject player;
    public float speed;
    public float hp;
    public float distanceBetween;
    public GameObject geefbareItem;
    public Transform items;
    public GameObject sliderObject;
    public Slider slider;
    public Slider strengthSlider;
    public float strengthValue;
    private float distance;

    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        if (menuManager.isHuman)
        {
            player = GameObject.Find("Human(Clone)");
        }
        if (menuManager.isGoblin)
        {
            player = GameObject.Find("Goblin(Clone)");
        }

        movement = FindObjectOfType<Movement>();
        slider = GameObject.Find("Hitpoints").GetComponent<Slider>();
        strengthSlider = GameObject.Find("Strength").GetComponent<Slider>();
        items = transform.Find("Items");
    }

    void Update()
    {
        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (distance < distanceBetween)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }

            if (hp <= 0)
            {
                Destroy(this.gameObject);
                Instantiate(geefbareItem, gameObject.transform.position, Quaternion.identity, items);
                StrengthUp(strengthValue);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChangeSliderValue(10f);
        }

        if (collision.gameObject.CompareTag("Ammo"))
        {
            hp -= 10;
            Destroy(collision.gameObject);
        }
    }

    public void ChangeSliderValue(float newValue)
    {
        slider.value -= newValue;
    }

    public void StrengthUp(float newValue)
    {
        strengthSlider.value += newValue;
    }
}
