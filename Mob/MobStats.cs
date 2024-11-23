using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStats : MonoBehaviour
{
    private Mob mob;

    public float MaxHealth;
    private float health;
    public float Health
    {
        get => health;
        set => health = value;
    }

    [SerializeField]
    private int damage;
    public int Damage
    {
        get => damage;
    }

    private float speed;
    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    [SerializeField]
    private float walkSpeed;

    public float WalkSpeed
    {
        get => walkSpeed;
    }

    private float runSpeed;
    public float RunSpeed
    {
        get => runSpeed;
        set => runSpeed = value;
    }

    public float strengthValue;

    private void Start()
    {
        mob = GetComponent<Mob>();
        SetUpStats();
    }

    private void SetUpStats()
    {
        mob.stats.Health = mob.stats.MaxHealth;
        mob.stats.Speed = mob.stats.WalkSpeed;
        mob.stats.RunSpeed = mob.stats.WalkSpeed * 2;
    }

    private void Update()
    {
        if (mob.stats.Health <= 0)
        {
            mob.controller.Die();
        }
    }

    public void UpdateHealth(float amount)
    {
        mob.stats.Health += amount;
    }

    public void SetSpeedToWalkSpeed()
    {
        mob.stats.Speed = mob.stats.WalkSpeed;
    }

    public void SetSpeedToRunSpeed()
    {
        mob.stats.Speed = mob.stats.RunSpeed;
    }
}
