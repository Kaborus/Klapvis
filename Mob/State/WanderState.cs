using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : IMobState
{
    private int xDirection;
    private int yDirection;
    float idlingTime = 0f;
    float wanderingTime = 0f;

    public void Handle(MobController mobController)
    {
        mobController.mob.stats.SetSpeedToWalkSpeed();

        if (mobController.behaviour == MobBehaviour.Aggressive)
        {
            if (mobController.distance < 4)
            {
                mobController.SetState(new ChaseState());
            }
        }

        Wander(mobController);
    }

    private void Wander(MobController mobController)
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

            mobController.transform.position += new Vector3(xDirection, yDirection, 0) * mobController.mob.stats.Speed * Time.deltaTime;
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
}
