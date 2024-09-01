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
            mob.mobAnimation.SetAttack(true);
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
        AnimatorStateInfo stateInfo = mob.mobAnimation.GetCurrentStateInfo();

        if (stateInfo.IsName("Attack"))
        {
            if (stateInfo.normalizedTime % 1 >= 0.4990f && stateInfo.normalizedTime % 1 < 0.5020f)
            {
                Attack(mob.mobController.target);
            }
        }
    }

    private void Attack(Transform target)
    {
        attackType.Attack(target);
    }

    public void ResetAttackTimer()
    {
        this.attackTimer = 0;
    }
}
