using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public Mob mob;

    public Transform target;
    public float distance;
    private Vector3 previousPosition;
    public Vector3 direction;

    public MobBehaviour behaviour;
    public IMobState state;

    public MobAttack attack;

    public List<Item> drops;
    public Transform items;

    private void Start()
    {
        mob = GetComponent<Mob>();
        items = GameObject.Find("Items").transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        previousPosition = transform.position;

        SetState(new WanderState());
    }

    private void Update()
    {
        direction = transform.position - previousPosition;
        previousPosition = transform.position;

        if (target == null) return;

        distance = Vector3.Distance(target.transform.position, transform.position);

        state.Handle(this);

        switch (distance)
        {
            case float n when n > 20f:
                mob.controller.Despawn();
                break;
            case float n when n > 10f:

                if (attack == MobAttack.Range)
                {
                    mob.anim.SetAttack(false);
                }
                SetState(new WanderState());
                break;
        }
    }

    public void SetState(IMobState mobState) => state = mobState;

    public IMobState GetState() => state;

    public void FollowTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * mob.stats.Speed * Time.deltaTime;
        }
    }

    public void Die()
    {
        GameManager.instance.player.stats.UpdateStrength(mob.stats.strengthValue);
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
                    SetState(new AttackState());
                }
                break;
            case "Ammo":
                if (behaviour == MobBehaviour.Passive)
                {
                    SetState(new FleeState());
                }
                else
                {
                    SetState(new ChaseState());
                }
                break;
        }
    }
}
