using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mob
{
    public GameObject throwable;
    public float firePower = 10.0f;
    public float fireInterval = 1.0f;
    public float fireTimer = 0.0f;

    public void Update()
    {
        if (target == null)
        {
            return;
        }

        float distance = Vector3.Distance(target.transform.position, transform.position);

        switch (state)
        {
            case State.Wandering:
                Wandering();
                if (behaviour == Behaviour.Aggressive)
                {
                    if (distance < 4)
                    {
                        state = State.Attacking;
                    }
                }
                break;
            case State.Attacking:
                if (distance > 2 || gameObject.CompareTag("Beetle"))
                {
                    FollowPlayer();
                }
                if (distance <= 4 && gameObject.CompareTag("Dragon"))
                {
                    // Update de timer
                    fireTimer += Time.deltaTime;

                    if (fireTimer >= fireInterval)
                    {
                        ThrowSomethingAtPlayer();
                        fireTimer = 0.0f; // Reset de timer na het schieten
                    }
                }
                break;
            case State.Retreating:
                RunAwayFromPlayer();
                break;
        }

        Die();

        if (distance > 10f)
        {
            state = State.Wandering;
        }
    }

    public void ThrowSomethingAtPlayer()
    {
        // Spawn het object
        GameObject newObject = Instantiate(throwable, transform.position, Quaternion.identity);

        // Controleer of er een doeltransform is
        if (target == null)
        {
            return;
        }
        // Bereken de richting van het doel
        Vector2 direction = (target.position - transform.position).normalized;

        // Voeg een impuls toe aan het nieuw gespawnde object in de berekende richting
        Rigidbody2D rb2d = newObject.GetComponent<Rigidbody2D>();

        // Gebruik AddForce met ForceMode2D.Impulse om de vuurbal te sturen
        rb2d.AddForce(direction * firePower, ForceMode2D.Impulse);

    }
}
