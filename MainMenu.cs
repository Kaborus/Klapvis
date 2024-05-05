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
        menuManager.isHuman = true;
        menuManager.isGoblin = false;
        menuManager.isDwarf = false;
        menuManager.isGiant = false;
    }

    public void PickGoblin()
    {
        menuManager.isHuman = false;
        menuManager.isGoblin = true;
        menuManager.isDwarf = false;
        menuManager.isGiant = false;
    }

    public void PickDwarf()
    {
        menuManager.isHuman = false;
        menuManager.isGoblin = false;
        menuManager.isDwarf = true;
        menuManager.isGiant = false;
    }

    public void PickGiant()
    {
        menuManager.isHuman = false;
        menuManager.isGoblin = false;
        menuManager.isDwarf = false;
        menuManager.isGiant = true;
    }

    public void CharacterBackButton()
    {
        menuManager.isHuman = false;
        menuManager.isGoblin = false;
        menuManager.isDwarf = false;
        menuManager.isGiant = false;
    }

    public void PlayPrehistory()
    {
        menuManager.inMenu = false;
        SceneManager.LoadScene(1);
    }

    public void PlayAntiquity()
    {
        SceneManager.LoadScene(2);
        menuManager.inMenu = false;
    }

    public void PlayMiddleAge()
    {
        SceneManager.LoadScene(3);
        menuManager.inMenu = false;
    }

    public void PlayModernAge()
    {
        SceneManager.LoadScene(4);
        menuManager.inMenu = false;
    }

    public void QuitGame()
    {
        Debug.Log("Het spel is afgesloten.");
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        menuManager.inMenu = true;
    }
}
