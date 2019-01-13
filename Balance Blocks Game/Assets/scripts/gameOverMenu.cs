using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameOverMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Text hsText;
    public Text scoreText;
    void Start()
    {
        int score = PlayerPrefs.GetInt("score", 0);
        int hs = PlayerPrefs.GetInt("highscore", 0);
        if (score > hs)
        {
            PlayerPrefs.SetInt("highscore", score);
            hs = score;
        }
            

        hsText.text = "High Score: " + hs.ToString();
        scoreText.text = "Score: " + score.ToString();
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
