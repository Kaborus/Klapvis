using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MonoBehaviour
{
    public event Action<Mob> OnDeath;
    public event Action<Mob> OnAttack;
    public Transform target;
    public int maxHealth;
    public int health;
    public int damage = 10;
    public float speed;
    public GameObject drop;
    public Behaviour behaviour;
    public State state;
    public Transform items;

    public float strengthValue;

    float wanderingTime = 0f;
    float idlingTime = 0f;
    int x;
    int y;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        items = transform.Find("Items");

    }

    // Update is called once per frame
    void Update()
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
                FollowPlayer();
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

    public void Wandering()
    {
        if (idlingTime <= 1f)
        {
            
            if (x == 0 && y == 0)
            {
                CalculateDirection();
            }
            idlingTime += Time.deltaTime;
        }

        if (idlingTime >= 1f)
        {
            wanderingTime += Time.deltaTime;
            transform.position += new Vector3(x, y, 0) * speed * Time.deltaTime;
        }

        if (wanderingTime >= 0.5f)
        {
            wanderingTime = 0f;
            idlingTime = 0f;
            CalculateDirection();
        }

    }

    public void CalculateDirection()
    {
        x = UnityEngine.Random.Range(-1, 2);
        y = UnityEngine.Random.Range(-1, 2);
    }

    public void FollowPlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    public void AttackPlayer(Player player) {
        player.health -= damage;
        Debug.Log(player.health);
    }

    public void RunAwayFromPlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position -= direction * speed * Time.deltaTime;
    }


    public void DropItem()
    {
        Instantiate(drop, transform.position, Quaternion.identity, items);
    }

    public void Die()
    {
        if (health <= 0)
        {
            OnDeath?.Invoke(this);
            Destroy(this.gameObject);
            DropItem();
        }
    }

    public void Despawn()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnAttack?.Invoke(this);
        }
        if (collision.gameObject.CompareTag("Ammo"))
        {
            switch (behaviour)
            {
                case Behaviour.Passive:
                    state = State.Retreating;
                    break;
                default:
                    state = State.Attacking;
                    break;
            }
            health--;
            Destroy(collision.gameObject);
        }
    }

    // public void ChangeSliderValue(float newValue)
    // {
    //     slider.value -= newValue;
    // }
}

[System.Serializable]
public enum Behaviour { Passive, Neutral, Aggressive }

[System.Serializable]
public enum State { Wandering, Attacking, Retreating }
