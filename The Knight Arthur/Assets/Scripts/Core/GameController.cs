using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject endGame;
    [SerializeField] private GameObject enterCastle;

    public static GameController instance;
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public void ShowEndGame()
    {
        enterCastle.SetActive(false);
        endGame.SetActive(true);
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }

    public void EnterCastle()
    {
        enterCastle.SetActive(true);
    }

    public void ExitCastle()
    {
        enterCastle.SetActive(false);
    }
}
