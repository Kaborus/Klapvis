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
        mob.mobStats.Health = mob.mobStats.MaxHealth;
        mob.mobStats.Speed = mob.mobStats.WalkSpeed;
        mob.mobStats.RunSpeed = mob.mobStats.WalkSpeed * 2;
    }

    private void Update()
    {
        if (mob.mobStats.Health <= 0)
        {
            mob.mobController.Die();
        }
    }

    public void UpdateHealth(float amount)
    {
        mob.mobStats.Health += amount;
    }

    public void SetSpeedToWalkSpeed()
    {
        mob.mobStats.Speed = mob.mobStats.WalkSpeed;
    }

    public void SetSpeedToRunSpeed()
    {
        mob.mobStats.Speed = mob.mobStats.RunSpeed;
    }
}
