using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ItemManager itemManager;
    public UI_Manager uiManager;
    public HUDManager hudManager;
    public EquipmentManager equipmentManager;
    public GameOverScreen gameover;
    public Movement movement;
    public MenuManager menuManager;
    public GameObject human;
    public GameObject goblin;
    public GameObject dwarf;
    public GameObject giant;
    public Player player;

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
        uiManager = GetComponent<UI_Manager>();
        hudManager = GetComponent<HUDManager>();
        gameover = FindObjectOfType<GameOverScreen>();
        menuManager = FindObjectOfType<MenuManager>();
        equipmentManager = GetComponent<EquipmentManager>();
        NewPlayer();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        if (hudManager.healthSlider.value == 0)
        {
            RemovePlayer();
        }
    }

    public void NewPlayer()
    {
        switch (menuManager.character)
        {
            case Character.Human:
                Instantiate(human);
                break;
            case Character.Goblin:
                Instantiate(goblin);
                break;
            case Character.Dwarf:
                Instantiate(dwarf);
                break;
            case Character.Giant:
                Instantiate(giant);
                break;
        }
    }

    public void RemovePlayer()
    {
        Destroy(player.gameObject);
    }
}

public enum Character { None, Human, Goblin, Dwarf, Giant }
