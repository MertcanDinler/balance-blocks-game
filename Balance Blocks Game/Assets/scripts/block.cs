using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    bool soundplay = true;
    // Start is called before the first frame update
    public AudioSource sound;
    private GameManagersc manager;
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManagersc>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "block" || collision.gameObject.tag == "startBlock")
        {
            this.gameObject.tag = "block";
            if (soundplay && PlayerPrefs.GetInt("sound", 1) == 1)
            {
                sound.Play();
                soundplay = false;
            }
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        manager.finish = true;
    }
}
