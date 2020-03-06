using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{

    public static bool gameStart = false;

    //spawn enemy
    public GameObject enemy;
    Vector2 screenBoundary;
    float top,bottom,right,left;
    float time = 0;
    float speed = 0.52f;
    int playTime = 0;

    //timer
    public Text timerText;
    float timer;

    //spawn item
    public GameObject item;
    float itemTime = 0;
    float itemSpeed;

    public static bool spawned = false;

    // Use this for initialization
    void Start()
    {
        timer = 0;
        itemSpeed = Random.Range(3, 5);
        screenBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        top = screenBoundary.y;
        bottom = -screenBoundary.y;
        right = screenBoundary.x;
        left = -screenBoundary.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart == true)
        {
            RandomSpawn();
            SpawnItem();

            timer += Time.deltaTime;
            playTime = (int)timer;
            timerText.text = "" + (int)timer;
        }

    }

    void SpawnItem()
    {
        itemTime += Time.deltaTime;
        if (itemTime > itemSpeed)
        {
            GameObject items = Instantiate(item, GameObject.Find("GameManager").transform);
            items.transform.position = new Vector3(Random.Range(left + 0.5f, right - 0.5f), Random.Range(bottom + 0.5f, top),-1);

            itemTime = 0;
            itemSpeed = Random.Range(3, 5);
        }
    }

    void RandomSpawn()
    {
        time += Time.deltaTime;

        if (time > speed)
        {
            if (playTime % 13 == 0 && speed > 0.1f)
            {
                speed -= 0.01f;
                Debug.Log(speed);
            }

            GameObject enemies;
            float ran = Random.value;

            if (ran > 0.75f)
            {
                enemies = Instantiate(enemy, GameObject.Find("GameManager").transform);
                enemies.transform.position = new Vector3(Random.Range(left, right), top,-1);
            }
            else if (ran > 0.5f && ran < 0.75f)
            {
                enemies = Instantiate(enemy, GameObject.Find("GameManager").transform);
                enemies.transform.position = new Vector3(Random.Range(left, right), bottom,-1);
            }
            else if (ran > 0.25f && ran < 0.5f)
            {
                enemies = Instantiate(enemy, GameObject.Find("GameManager").transform);
                enemies.transform.position = new Vector3(right, Random.Range(bottom, top),-1);
            }
            else
            {
                enemies = Instantiate(enemy, GameObject.Find("GameManager").transform);
                enemies.transform.position = new Vector3(left, Random.Range(bottom, top),-1);
            }


            enemies.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 0.5f), Random.Range(0, 0.5f), 0, 1);

            time = 0;
            spawned = true;
        }
    }

    public GameObject pausePopup;

    public void PausePopup()
    {
        Time.timeScale = 0;
        pausePopup.SetActive(true);
        gameStart = false;
        Player.paused = true;
    }
    public void Continue()
    {
        Time.timeScale = 1;
        pausePopup.SetActive(false);
        gameStart = true;
        Player.paused = false;
    }
}
