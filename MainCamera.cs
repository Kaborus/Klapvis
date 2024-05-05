using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public MenuManager menuManager;
    public Movement movement;
    public GameObject human;
    public GameObject goblin;
    public GameObject dwarf;
    public GameObject giant;

    private void Start()
    {
        human = GameObject.Find("Human(Clone)");
        goblin = GameObject.Find("Goblin(Clone)");
        dwarf = GameObject.Find("Dwarf(Clone)");
        giant = GameObject.Find("Giant(Clone)");

        menuManager = FindObjectOfType<MenuManager>();
        movement = FindObjectOfType<Movement>();
    }

    void Update()
    {
        if (menuManager.isHuman && human != null)
        {
            transform.position = human.transform.position + new Vector3(0, 0, -10);
        }
        if (menuManager.isGoblin && goblin != null)
        {
            transform.position = goblin.transform.position + new Vector3(0, 0, -10);
        }
        if (menuManager.isDwarf && dwarf != null)
        {
            transform.position = dwarf.transform.position + new Vector3(0, 0, -10);
        }
        if (menuManager.isGiant && giant != null)
        {
            transform.position = giant.transform.position + new Vector3(0, 0, -10);
        }
    }
}
