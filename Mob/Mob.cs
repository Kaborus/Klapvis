using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    internal MobStats mobStats;
    internal MobController mobController;
    internal MobAnimation mobAnimation;
    internal MobCombat mobCombat;

    private void Awake()
    {
        SetUpComponent();
    }

    private void SetUpComponent()
    {
        mobStats = GetComponent<MobStats>();
        mobController = GetComponent<MobController>();
        mobAnimation = GetComponent<MobAnimation>();
        mobCombat = GetComponent<MobCombat>();
    }
}
