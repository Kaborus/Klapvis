using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AttackType
{
    private Mob mob;

    private void Start()
    {
        mob = GetComponent<Mob>();
    }

    public override void Attack(Transform target)
    {
        GameManager.instance.player.playerStats.UpdateHealth(-(GetDamage()));
    }

    private float GetDamage()
    {
        float dmg = (mob.mobStats.Damage - (GameManager.instance.player.playerStats.Protection * 0.5f));

        return dmg < 1 ? 1 : dmg;
    }
}
