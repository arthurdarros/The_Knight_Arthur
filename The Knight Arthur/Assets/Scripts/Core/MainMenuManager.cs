using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string lvlName;
    [SerializeField]private GameObject mainMenuPanel;
    [SerializeField]private GameObject optionsPanel;
    public void Play()
    {
        SceneManager.LoadScene(lvlName);
    }
    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void ExitOptions()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ExitGame()
    {  
        Debug.Log("Exit");
        Application.Quit();
    }
}
