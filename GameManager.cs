using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ItemManager itemManager;
    public QuestManager questManager;
    public UI_Manager uiManager;
    public GameOverScreen gameover;
    public CharacterSelection characterSelection;
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
        questManager = GetComponent<QuestManager>();
        uiManager = GetComponent<UI_Manager>();
        gameover = FindObjectOfType<GameOverScreen>();
        characterSelection = FindObjectOfType<CharacterSelection>();

        NewPlayer();

        Destroy(GameObject.Find("CharacterSelection"));
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void NewPlayer()
    {
        switch (characterSelection.character)
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

    // public void RemovePlayer()
    // {
    //     Destroy(player.gameObject);
    // }
}