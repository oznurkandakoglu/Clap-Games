using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] GameObject taptostartPanel;
    [SerializeField] GameObject congratsPanel;
    [SerializeField] GameObject gamePanel;

    public static bool gameOver;
    public static bool nextScene;
    void Start()
    {
        Time.timeScale = 0;
        gameoverPanel.SetActive(false);
        congratsPanel.SetActive(false);
        taptostartPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameOver = false;
        nextScene=false;
    }

    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameoverPanel.SetActive(true);
        }

        if (nextScene)
        {
            GamePanel();           
        }
    }
    public void PlayGame()
    {

        Time.timeScale = 1;
        taptostartPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void GamePanel()
    {
        Time.timeScale = 0;
        congratsPanel.SetActive(true);
    }
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameoverPanel.SetActive(false);

    }

    public void nextLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        
    }
}
