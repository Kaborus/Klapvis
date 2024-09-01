using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    private Mob mob;

    public Transform target;
    public float distance;
    private Vector3 previousPosition;
    public Vector3 direction;
    float wanderingTime = 0f;
    float idlingTime = 0f;
    private int xDirection;
    private int yDirection;

    public MobBehaviour behaviour;
    public MobState state;
    public MobAttack attack;

    public List<Item> drops;
    public Transform items;

    private void Start()
    {
        mob = GetComponent<Mob>();
        items = GameObject.Find("Items").transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        previousPosition = transform.position;
    }

    private void Update()
    {
        direction = transform.position - previousPosition;
        previousPosition = transform.position;

        if (target == null)
        {
            return;
        }

        distance = Vector3.Distance(target.transform.position, transform.position);

        switch (state)
        {
            case MobState.Wander:
                HandleWanderState();
                break;
            case MobState.Chase:
                HandleChaseState();
                break;
            case MobState.Attack:
                HandleAttackState();
                break;
            case MobState.Retreat:
                HandleRetreatState();
                break;
        }

        switch (distance)
        {
            case float n when n > 20f:
                mob.mobController.Despawn();
                break;
            case float n when n > 10f:

                if (attack == MobAttack.Range)
                {
                    mob.mobAnimation.SetAttack(false);
                }
                state = MobState.Wander;
                break;
        }
    }

    private void HandleWanderState()
    {
        mob.mobStats.SetSpeedToWalkSpeed();

        if (behaviour == MobBehaviour.Aggressive)
        {
            if (distance < 4)
            {
                state = MobState.Chase;
            }
        }

        Wander();
    }

    private void HandleChaseState()
    {
        mob.mobStats.SetSpeedToRunSpeed();

        if (attack == MobAttack.Melee || attack == MobAttack.Range && distance > 3)
        {
            mob.mobController.FollowTarget();
        }

        if (attack == MobAttack.Range)
        {
            mob.mobCombat.AttackState();
        }
    }

    private void HandleAttackState()
    {
        if (attack == MobAttack.Range && distance > 3f)
        {
            state = MobState.Chase;
            return;
        }

        if (attack == MobAttack.Melee)
        {
            if (distance > 1.5)
            {
                mob.mobCombat.ResetAttackTimer();
                state = MobState.Chase;
                return;
            }

            mob.mobCombat.AttackState();
        }
    }

    private void HandleRetreatState()
    {
        mob.mobStats.SetSpeedToRunSpeed();
        RunAway();
    }

    private void Wander()
    {
        if (idlingTime <= 1f)
        {
            if (xDirection == 0 && yDirection == 0)
            {
                CalculateDirection();
            }
            idlingTime += Time.deltaTime;
        }

        if (idlingTime >= 1f)
        {
            wanderingTime += Time.deltaTime;

            transform.position += new Vector3(xDirection, yDirection, 0) * mob.mobStats.Speed * Time.deltaTime;
        }

        if (wanderingTime >= 0.5f)
        {
            wanderingTime = 0f;
            idlingTime = 0f;
            CalculateDirection();
        }
    }

    private void CalculateDirection()
    {
        xDirection = UnityEngine.Random.Range(-1, 2);
        yDirection = UnityEngine.Random.Range(-1, 2);
    }

    private void FollowTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * mob.mobStats.Speed * Time.deltaTime;
        }
    }

    private void RunAway()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position -= direction * mob.mobStats.Speed * Time.deltaTime;
    }

    public void Die()
    {
        GameManager.instance.player.playerStats.UpdateStrength(mob.mobStats.strengthValue);
        DropItems();
        Destroy(gameObject);
    }

    private void DropItems()
    {
        foreach (Item item in drops)
        {
            Instantiate(item, transform.position, Quaternion.identity, items);
        }
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                if (attack == MobAttack.Melee)
                {
                    state = MobState.Attack;
                }
                break;
            case "Ammo":
                state = (behaviour == MobBehaviour.Passive) ? MobState.Retreat : MobState.Chase;
                break;
        }
    }
}
