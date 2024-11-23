using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCombat : MonoBehaviour
{
    private Mob mob;
    public AttackType attackType;

    private float attackTimer;

    private void Start()
    {
        mob = GetComponent<Mob>();
        attackType = GetComponent<AttackType>();
    }

    public void AttackState()
    {
        if (attackTimer == 0)
        {
            mob.anim.SetAttack(true);
        }

        CheckAnimationStateForAttack();

        attackTimer += Time.deltaTime;

        if (attackTimer >= 1f)
        {
            ResetAttackTimer();
        }
    }

    private void CheckAnimationStateForAttack()
    {
        AnimatorStateInfo stateInfo = mob.anim.GetCurrentStateInfo();

        if (stateInfo.IsName("Attack"))
        {
            if (stateInfo.normalizedTime % 1 >= 0.4990f && stateInfo.normalizedTime % 1 < 0.5020f)
            {
                Attack(mob.controller.target);
            }
        }
    }

    private void Attack(Transform target)
    {
        attackType.Attack(target);
    }

    public void ResetAttackTimer()
    {
        attackTimer = 0;
    }
}
