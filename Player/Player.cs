
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    internal PlayerStats playerStats;
    internal PlayerInventory playerInventory;
    internal PlayerController playerController;
    internal PlayerMovement playerMovement;
    internal PlayerCombat playerCombat;
    internal PlayerAnimation playerAnimation;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        playerInventory = GetComponent<PlayerInventory>();
        playerController = GetComponent<PlayerController>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }
}
