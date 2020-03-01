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
    Vector2 center;

    //player
    float moveSpeed;

    //Gameplay
    bool gameStart;
    public GameObject bullet;
    GameObject myBullet;
    GameObject closestEnemy;
    float fireTime;
    float fireSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //Joystick
        mouseDown = false;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        center = new Vector2(0, 0);
        //player
        moveSpeed = 1.2f;
        //gameplay
        gameStart = false;
        fireTime = 0;
        fireSpeed = 1;
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
            if(fireTime > fireSpeed)
            {
                FireBullet();
                fireTime = 0;
            }
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
        Debug.Log(Screen.width);
        if(myPos.x > screenBounds.x || myPos.x < -screenBounds.x || myPos.y > screenBounds.y || myPos.y < -screenBounds.y)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, center, moveSpeed * Time.deltaTime);
        }
    }

    void UseItem()
    {
        switch ((int)Random.Range(0, 1))
        {
            case 0:
                break;
            case 1:
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "item")
        {
            UseItem();
            Destroy(collision.gameObject);
        }
    }
}
