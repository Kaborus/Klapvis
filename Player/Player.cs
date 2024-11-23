
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    internal PlayerStats stats;
    internal PlayerInventory inventory;
    internal PlayerController controller;
    internal PlayerMovement movement;
    internal PlayerCombat combat;
    internal PlayerAnimation anim;

    private void Awake()
    {
        SetUpComponents();
    }

    private void SetUpComponents()
    {
        stats = GetComponent<PlayerStats>();
        inventory = GetComponent<PlayerInventory>();
        controller = GetComponent<PlayerController>();
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();
        anim = GetComponent<PlayerAnimation>();
    }
}
