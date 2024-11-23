using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    internal MobStats stats;
    internal MobController controller;
    internal MobAnimation anim;
    internal MobCombat combat;

    private void Awake()
    {
        SetUpComponents();
    }

    private void SetUpComponents()
    {
        stats = GetComponent<MobStats>();
        controller = GetComponent<MobController>();
        anim = GetComponent<MobAnimation>();
        combat = GetComponent<MobCombat>();
    }
}
