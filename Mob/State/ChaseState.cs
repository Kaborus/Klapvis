using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IMobState
{
    public void Handle(MobController controller)
    {
        controller.mob.stats.SetSpeedToRunSpeed();

        if (controller.attack == MobAttack.Melee || controller.attack == MobAttack.Range && controller.distance > 3)
        {
            controller.FollowTarget();
        }

        if (controller.attack == MobAttack.Range)
        {
            controller.mob.combat.AttackState();
        }
    }
}
