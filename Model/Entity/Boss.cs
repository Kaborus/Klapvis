using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform target;
    float moveSpeed = 2f;
    public State currentState;
    public int hp = 10;
    public GameObject reward;
    public Transform items;
    public GameObject bestuurdObject;
    public float schietKracht = 10.0f;
    public float schietInterval = 1.0f;
    private float schietTimer = 0.0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = State.Idle;
    }

    void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);

            switch (currentState)
            {
                case State.Idle:
                    break;
                case State.Attack:
                    if (distance > 2 || gameObject.CompareTag("Beetle"))
                    {
                        FollowPlayer();
                    }
                    if (distance <= 4 && gameObject.CompareTag("Dragon"))
                    {
                        // Update de timer
                        schietTimer += Time.deltaTime;

                        if (schietTimer >= schietInterval)
                        {
                            Shoot();
                            schietTimer = 0.0f; // Reset de timer na het schieten
                        }
                    }
                    break;
                case State.Retreat:
                    Debug.Log("Run Away!");
                    break;
            }

            if (hp <= 0)
            {
                Destroy(gameObject);
                Instantiate(reward, transform.position, Quaternion.identity, items);
            }

            if (distance > 10f)
            {
                currentState = State.Idle;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            currentState = State.Attack;
            hp--;
        }
    }

    void FollowPlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void Shoot()
    {
        // Spawn het object
        GameObject nieuwObject = Instantiate(bestuurdObject, transform.position, Quaternion.identity);

        // Controleer of er een doeltransform is
        if (target != null)
        {
            // Bereken de richting van het doel
            Vector2 richting = (target.position - transform.position).normalized;

            // Voeg een impuls toe aan het nieuw gespawnde object in de berekende richting
            Rigidbody2D rb2d = nieuwObject.GetComponent<Rigidbody2D>();

            // Gebruik AddForce met ForceMode2D.Impulse om de vuurbal te sturen
            rb2d.AddForce(richting * schietKracht, ForceMode2D.Impulse);
        }
    }
}

[System.Serializable]
public enum State { Idle, Attack, Retreat }
