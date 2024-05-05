using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public bool melee;
    public bool range;
    public bool pickaxe;
    public bool axe;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MeleeEquiped();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RangeEquiped();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PickaxeEquiped();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AxeEquiped();
        }
    }
    public void MeleeEquiped()
    {
        melee = true;
        range = false;
        pickaxe = false;
        axe = false;
    }

    public void RangeEquiped()
    {
        melee = false;
        range = true;
        pickaxe = false;
        axe = false;
    }

    public void PickaxeEquiped()
    {
        melee = false;
        range = false;
        pickaxe = true;
        axe = false;
    }

    public void AxeEquiped()
    {
        melee = false;
        range = false;
        pickaxe = false;
        axe = true;
    }
}
