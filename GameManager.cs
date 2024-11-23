using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ItemManager itemManager;
    public SpriteManager spriteManager;
    public DialogueManager dialogueManager;
    public UI_Manager uiManager;
    public GameOverScreen gameover;
    public GameObject character;
    public Player player;

    public Transform spawners;
    public Transform resourceNodes;
    public Transform items;
    public Transform mobs;
    public Transform Npcs;

    public Npc guide;
    public Npc farmer;
    public Npc goblin;
    public Npc dwarf;
    public Npc giant;
    public Npc fisherman;
    public Npc swampGuide;
    public Npc queen;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        itemManager = GetComponent<ItemManager>();
        spriteManager = GetComponent<SpriteManager>();
        dialogueManager = GetComponent<DialogueManager>();
        uiManager = GetComponent<UI_Manager>();
        gameover = FindObjectOfType<GameOverScreen>();

        SpawnPlayer();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        guide = GameObject.Find("Guide").GetComponent<Npc>();
        farmer = GameObject.Find("Farmer").GetComponent<Npc>();
    }

    public void SpawnPlayer()
    {
        Instantiate(character);
    }
}