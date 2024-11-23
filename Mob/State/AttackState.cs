using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IMobState
{
    public void Handle(MobController controller)
    {
        if (controller.attack == MobAttack.Range && controller.distance > 3f)
        {
            controller.SetState(new ChaseState());
            return;
        }

        if (controller.attack == MobAttack.Melee)
        {
            if (controller.distance > 1.5)
            {
                controller.mob.combat.ResetAttackTimer();
                controller.SetState(new ChaseState());
                return;
            }

            controller.mob.combat.AttackState();
        }
    }
}
