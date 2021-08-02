using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreDisplay : MonoBehaviour
{
    Text scoreText;
    [SerializeField] Text highScore;
    GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = gameSession.GetScore().ToString();
        if (gameSession.GetScore() > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", gameSession.GetScore());
            if(highScore)
                highScore.text = gameSession.GetScore().ToString();
        }
        if(highScore)
        {
            highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
        
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore.text = "0";
    }
}
