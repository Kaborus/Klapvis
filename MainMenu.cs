using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public MenuManager menuManager;

    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    public void PickHuman()
    {
        menuManager.character = Character.Human;
    }

    public void PickGoblin()
    {
        menuManager.character = Character.Goblin;
    }

    public void PickDwarf()
    {
        menuManager.character = Character.Dwarf;
    }

    public void PickGiant()
    {
        menuManager.character = Character.Giant;
    }

    public void PlayPrehistory()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayAntiquity()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayMiddleAge()
    {
        SceneManager.LoadScene(3);
    }

    public void PlayModernAge()
    {
        SceneManager.LoadScene(4);
    }

    public void QuitGame()
    {
        Debug.Log("Het spel is afgesloten.");
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
