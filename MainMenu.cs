using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public CharacterSelection characterSelection;

    void Start()
    {
        characterSelection = FindObjectOfType<CharacterSelection>();
    }

    public void PickHuman()
    {
        characterSelection.character = Character.Human;
    }

    public void PickGoblin()
    {
        characterSelection.character = Character.Goblin;
    }

    public void PickDwarf()
    {
        characterSelection.character = Character.Dwarf;
    }

    public void PickGiant()
    {
        characterSelection.character = Character.Giant;
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
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
