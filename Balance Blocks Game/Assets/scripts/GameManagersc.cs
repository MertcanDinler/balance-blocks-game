using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagersc : MonoBehaviour
{
    public GameObject block;
    private GameObject activeBlock;
    private GameObject lastBlock;
    private int count = 0;
    private Color[] colors = new Color[] { Color.black, Color.cyan, Color.gray, Color.green, Color.grey, Color.red, Color.yellow};
    Random random = new Random();
    float animatePos;
    bool animateAxis = true; //true x axis, false y axis
    bool animating = false;
    public GameObject Camera;
    bool upCam = false;
    float newCamPosY;
    public bool finish = false;
    int score = -1;
    public GameObject scoreBoard;
    Text scoreText;
    public AudioSource sound;
    public AudioSource bgm;

    public GameObject firstBlock;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = scoreBoard.GetComponent<Text>();
        scoreText.text = "0";
        animatePos = 4;
        InvokeRepeating("firstBlocks", 0, 0.6f);
        if(PlayerPrefs.GetInt("bgm", 1) == 1)
        {
            bgm.Play();
        }
    }
    void firstBlocks()
    {
        if (count < 3)
        {
            
            int colorIndex = Random.Range(0, colors.Length);
            lastBlock = Instantiate(block, new Vector3(0, 10, 0), new Quaternion());
            lastBlock.GetComponent<MeshRenderer>().material.color = colors[colorIndex];
            lastBlock.tag = "first3Blocks";
            count++;
        }
        else
        {
            CancelInvoke("firstBlocks");
            
        }
        

    }
    // Update is called once per frame
    void Update()
    {
        if (finish)
        {
            finishGame();
            return;
        }
        if(lastBlock.tag == "first3Blocks")
        {
            return;
        }
        if(activeBlock == null && lastBlock.tag == "block")
        {
            spawnBlock();
            score += 1;
            scoreText.text = score.ToString();
            //newCamPosY = Camera.transform.position.y + 0.9f;
            newCamPosY = lastBlock.transform.position.y + 7.3f;
            upCam = true;
            
        }
        
        if(activeBlock != null && activeBlock.tag == "activeBlock")
        {
            checkInput();
            animatePlay(activeBlock);
        }
        upCamera();
    }

    void spawnBlock()
    {
        animating = true;
        float x = 0;
        float z = 0;
        animateAxis = (Random.Range(0, 101) % 2) == 0;
        if (animateAxis)
        {
            x = animatePos;
        }
        else
        {
            z = animatePos;
        }
        int colorIndex = Random.Range(0, colors.Length);
        activeBlock = Instantiate(block, new Vector3(x, lastBlock.transform.position.y + 2, z), new Quaternion());
        activeBlock.GetComponent<Rigidbody>().useGravity = false;
        activeBlock.GetComponent<MeshRenderer>().material.color = colors[colorIndex]; ;
    }

    void animatePlay(GameObject blockc)
    {
        if (!animating) return;
        Vector3 pos = blockc.transform.position;
        float x = 0;
        float z = 0;
        if (animateAxis)
        {
            x = animatePos;
            if (blockc.transform.position.x == animatePos)
            {
                animatePos *= -1;
                x = animatePos;
            }
        }
        else
        {
            z = animatePos;
            if (blockc.transform.position.z == animatePos)
            {
                animatePos *= -1;
                z = animatePos;
            }
        }
        blockc.transform.position = Vector3.MoveTowards(pos, new Vector3(x, pos.y, z), Time.deltaTime * 3);
    }

    void checkInput()
    {
        if(animating && Input.anyKey)
        {
            activeBlock.GetComponent<Rigidbody>().useGravity = true;
            lastBlock = activeBlock;
            activeBlock = null;
            animating = false;
        }
    }

    void upCamera()
    {
        if (!upCam) return;
        if(newCamPosY == Camera.transform.position.y)
        {
            upCam = false;
        }
        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, new Vector3(Camera.transform.position.x, newCamPosY, Camera.transform.position.z), Time.deltaTime * 5);
    }

    void finishGame()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.Save();
        if(activeBlock != null)
        {
            activeBlock.GetComponent<Rigidbody>().useGravity = true;
            activeBlock = null;
            if(PlayerPrefs.GetInt("sound", 1) == 1)
                sound.Play();
        }
        if(firstBlock) Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, new Vector3(Camera.transform.position.x, firstBlock.transform.position.y +5f, Camera.transform.position.z), Time.deltaTime * 10);
        if(firstBlock && Camera.transform.position.y == (firstBlock.transform.position.y + 5f))
        {
            firstBlock.AddComponent<Rigidbody>().mass = 10;
            Destroy(firstBlock);
            
        }

        StartCoroutine(waitAndChangeScene());
    }
    IEnumerator waitAndChangeScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("GameOver");
    }


}
