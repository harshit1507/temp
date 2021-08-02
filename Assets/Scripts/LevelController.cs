using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
    int sceneIndex;
    public void LoadStartMenu()
    {
        LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGame()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        if(gameSession)
        {
            gameSession.ResetScore();
        }
        LoadScene(1);
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        LoadScene(2);
    }
}
