using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : IMobState
{
    public void Handle(MobController controller)
    {
        controller.mob.stats.SetSpeedToRunSpeed();
        Flee(controller);
    }

    private void Flee(MobController controller)
    {
        Vector3 direction = (controller.target.position - controller.transform.position).normalized;
        controller.transform.position -= controller.direction * controller.mob.stats.Speed * Time.deltaTime;
    }
}
