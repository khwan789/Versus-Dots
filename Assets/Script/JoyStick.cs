using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    //Joystick
    Vector2 initialPos;
    bool mouseDown;
    Vector2 movingPos;
    Vector2 direction;
    Vector2 screenBounds;
    Vector3 center;

    //player
    float moveSpeed;

    //Gameplay
    bool gameStart;
    public GameObject bullet;
    GameObject myBullet;
    GameObject closestEnemy;
    float fireTime;
    float fireSpeed;

    //items
    public GameObject beelet;
    GameObject _beelet;
    public GameObject pencil;
    float rapidTime;
    bool rapidOn;

    // Start is called before the first frame update
    void Start()
    {
        //Joystick
        mouseDown = false;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        center = new Vector3(0, 0, -1);
        //player
        moveSpeed = 1.5f;
        //gameplay
        gameStart = false;
        fireTime = 0;
        fireSpeed = 1;
        pencil.SetActive(false);

        //item
        rapidTime = 0;
        rapidOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        //character move
        Click();
        Vector3 newDirection = new Vector3(direction.x, direction.y, 0);
        this.transform.position += newDirection * Time.deltaTime * moveSpeed;

        WithinScreen();

        Spawn.gameStart = gameStart;

        //GamePlay
        if (Spawn.spawned)
        {
            fireTime += Time.deltaTime;
            if (fireTime > fireSpeed)
            {
                FireBullet();
                fireTime = 0;
            }
        }

        if (rapidOn)
        {
            RapidFire();
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;

        float distance = 100;
        Vector3 position = this.transform.position;

        foreach (GameObject ene in enemies)
        {
            Vector3 diff = ene.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = ene;
                distance = curDistance;
            }
        }
        return closest;
    }

    void FireBullet()
    {
        closestEnemy = FindClosestEnemy();
        this.transform.right = closestEnemy.transform.position - this.transform.position;


        myBullet = Instantiate(bullet, GameObject.Find("GameManager").transform);
        myBullet.transform.position = GameObject.Find("right").transform.position;
    }

    void Click()
    {
        if (Input.GetMouseButton(0)) //mouseDown
        {
            gameStart = true;
            if (!mouseDown)
            {
                initialPos = Input.mousePosition;
                mouseDown = true;
            }
            movingPos = Input.mousePosition;
            direction = (movingPos - initialPos).normalized;
        }
        else //mouseUp
        {
            direction = new Vector2(0, 0);
            mouseDown = false;
        }
    }

    void WithinScreen()
    {
        Vector2 myPos = this.gameObject.transform.position;
        if (myPos.x > screenBounds.x || myPos.x < -screenBounds.x || myPos.y > screenBounds.y || myPos.y < -screenBounds.y)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, center, moveSpeed * Time.deltaTime);
        }
    }

    void UseItem()
    {

        switch (3)//(int)Random.Range(1, 2))
        {
            case 1:
                ItemBeelet();
                break;
            case 2:
                if (!pencil.activeSelf)
                {
                    pencil.SetActive(true);
                }
                pencil.GetComponent<Animator>().Play("pencilAnim", -1, 0);
                break;
            case 3:
                rapidOn = true;
                break;
            default:
                break;
        }
    }

    void ItemBeelet()
    {
        Debug.Log("use item");
        for (int i = 0; i < 4; i++)
        {
            float ang = i * Mathf.PI * 2f / 4;
            Vector3 pos;
            pos.x = GameObject.Find("player").transform.position.x + 1f * Mathf.Sin(ang);
            pos.y = GameObject.Find("player").transform.position.y + 1f * Mathf.Cos(ang);
            pos.z = GameObject.Find("player").transform.position.z;

            _beelet = Instantiate(beelet, GameObject.Find("GameManager").transform);
            _beelet.transform.position = pos;
            _beelet.transform.right = _beelet.transform.position - GameObject.Find("player").transform.position;
        }
    }

    void RapidFire()
    {
        rapidTime += Time.deltaTime;
        if(rapidTime <= 3)
        {
            fireSpeed = 0.2f;
        }
        else
        {
            fireSpeed = 1;
            rapidTime = 0;
            rapidOn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            UseItem();
            Destroy(collision.gameObject);
        }
    }
}
