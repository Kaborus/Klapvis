using UnityEngine;
using UnityEngine.UI;


public class Fireball : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider = GameObject.Find("Hitpoints").GetComponent<Slider>();
    }

    public void ChangeSliderValue(float newValue)
    {
        slider.value -= newValue;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Controleer of de collision niet met zichzelf plaatsvindt
        if (collision.gameObject != gameObject)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // Stop de beweging van de speler door de fysica uit te schakelen
                Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

                if (playerRigidbody != null)
                {
                    playerRigidbody.velocity = Vector2.zero;
                    playerRigidbody.angularVelocity = 0f;
                }
                ChangeSliderValue(10f);
            }
        }
    }
}
