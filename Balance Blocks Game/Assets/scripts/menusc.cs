using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menusc : MonoBehaviour
{
    // Start is called before the first frame update
    bool sound = true;
    bool bgm = true;
    public Text soundText;
    public Text bgmText;
    public Text hsText;
    void Start()
    {
        sound = PlayerPrefs.GetInt("sound", 1) == 1;
        bgm = PlayerPrefs.GetInt("bgm", 1) == 1;
        hsText.text = "High Score: " + PlayerPrefs.GetInt("highscore", 0).ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        sound = PlayerPrefs.GetInt("sound", 1) == 1;
        bgm = PlayerPrefs.GetInt("bgm", 1) == 1;
        soundText.text = "Sound: " + onoff(sound);
        bgmText.text = "BG Music: " + onoff(bgm);
    }
    string onoff(bool x)
    {
        if (x)
        {
            return "On";
        }
        else
        {
            return "Off";
        }
    }
    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void changeSetting(string key)
    {
        if (key == "exit") Application.Quit();
        int setting = PlayerPrefs.GetInt(key,1);
        if(setting == 1)
        {
            PlayerPrefs.SetInt(key, 0);
        }
        else
        {
            PlayerPrefs.SetInt(key, 1);
        }
        PlayerPrefs.Save();
    }
}
