using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ItemManager itemManager;
    public UI_Manager uiManager;
    public GameOverScreen gameover;
    public Movement movement;
    public MenuManager menuManager;
    public GameObject human;
    public GameObject goblin;
    public GameObject dwarf;
    public GameObject giant;
    public Player player;
    public Slider slider;

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
        gameover = FindObjectOfType<GameOverScreen>();
        menuManager = FindObjectOfType<MenuManager>();
        NewPlayer();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        slider = GameObject.Find("Hitpoints").GetComponent<Slider>();
    }

    private void Update()
    {
        if (slider.value == 0)
        {
            RemovePlayer();
        }
    }

    public void NewPlayer()
    {
        if (menuManager.isHuman)
        {
            Instantiate(human);
        }
        if (menuManager.isGoblin)
        {
            Instantiate(goblin);
        }
        if (menuManager.isDwarf)
        {
            Instantiate(dwarf);
        }
        if (menuManager.isGiant)
        {
            Instantiate(giant);
        }
    }

    public void RemovePlayer()
    {
        if (menuManager.isHuman && human != null)
        {
            human = GameObject.Find("Human(Clone)");
            Destroy(human);
        }
        if (menuManager.isGoblin && goblin != null)
        {
            goblin = GameObject.Find("Goblin(Clone)");
            Destroy(goblin);
        }
        if (menuManager.isDwarf && dwarf != null)
        {
            dwarf = GameObject.Find("Dwarf(Clone)");
            Destroy(dwarf);
        }
        if (menuManager.isGiant && giant != null)
        {
            giant = GameObject.Find("Giant(Clone)");
            Destroy(giant);
        }
    }
}
