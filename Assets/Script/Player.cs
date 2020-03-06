using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    float moveSpeed;

    public GameObject bullet;

    float speed = 1f;
    float time = 0;

    Vector2 mousePosi;

    float roundTime = 0;
    float speedTime = 0;
    float bombTime = 0;

    public GameObject closestEnemy;
    public GameObject bomb;
    public GameObject field;
    RectTransform rt;
    float width;
    float height;

    float bTime = 0;

    public static bool paused;

    // Use this for initialization
    void Start()
    {
        paused = false;
        rb = this.GetComponent<Rigidbody2D>();
        moveSpeed = 2;
        //rt = (RectTransform)field.transform;
        //width = rt.rect.width;
        //height = rt.rect.height;

        /*Debug.Log(field.transform.position.x + (width / 2));
        Debug.Log(field.transform.position.x - (width / 2));
        Debug.Log(field.transform.position.y + (height / 2));
        Debug.Log(field.transform.position.y - (height / 2));*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (floatingJoystick.down )//&& paused == false)
        {*/
//            this.transform.position += direction * Time.deltaTime * moveSpeed;
            Spawn.gameStart = true;
            /*Vector3 direction = Vector2.up * floatingJoystick.Vertical + Vector2.right * floatingJoystick.Horizontal;

            this.transform.position = Vector2.MoveTowards(this.transform.position, direction, 0.05f);
            */
            /*mousePosi = Input.mousePosition;
            mousePosi = Camera.main.ScreenToWorldPoint(mousePosi);
            Debug.Log(mousePosi.x + "  " + mousePosi.y);
            if (mousePosi.x <= 2.64f && mousePosi.x >= -2.64f)
            {
                if (mousePosi.y <= 3.78f && mousePosi.y >= -4.78f)
                {
                    this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(mousePosi.x, mousePosi.y + 1), 0.05f);

                }
            }*/

            //this.transform.position = Vector2.Lerp(this.transform.position, mousePosi, 1);



        
        if (Spawn.spawned == true)
        {
            time += Time.deltaTime;
            if (time > speed)
            {
                FireBullet();
                time = 0;
            }

            if (roundTime > 0)
            {
                roundTime -= Time.deltaTime;
            }

            if (speedTime > 0)//speedUpgrade)
            {
                speed = 0.1f;
                speedTime -= Time.deltaTime;

                if (speedTime <= 0)
                {
                    speed = 1;
                }
            }
            if (bombTime > 0)
            {
                bTime += Time.deltaTime;
                bombTime -= Time.deltaTime;
            }
            if (bTime > 0.5)
            {
                bombTime -= Time.deltaTime;
                SpawnBomb();
                bTime = 0;
            }
            GetItem();
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
        if (roundTime <= 0)
        {
            GameObject myBullet;
            myBullet = Instantiate(bullet, GameObject.Find("GameManager").transform);
            myBullet.transform.position = GameObject.Find("right").transform.position;
        }
        else
        {
            ThreeSixtyBullet();
        }
    }

    void ThreeSixtyBullet()
    {
        closestEnemy = FindClosestEnemy();
        this.transform.right = closestEnemy.transform.position - this.transform.position;

        for (int i = 0; i < 16; i++)
        {
            float ang = i * Mathf.PI * 2f / 16;
            Vector3 pos;
            pos.x = GameObject.Find("player").transform.position.x + 1f * Mathf.Sin(ang);
            pos.y = GameObject.Find("player").transform.position.y + 1f * Mathf.Cos(ang);
            pos.z = GameObject.Find("player").transform.position.z;

            GameObject newBullet;
            newBullet = Instantiate(bullet, GameObject.Find("player").transform);
            newBullet.transform.position = pos;
            
        }
    }

    void SpawnBomb()
    {
        GameObject newBomb;
        newBomb = Instantiate(bomb, GameObject.Find("player").transform);
        newBomb.transform.SetParent(GameObject.Find("GameManager").transform);
        Debug.Log("bomb");

        Destroy(newBomb, 2.0f);
    }

    void GetItem()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        if (items != null)
        {
            for (int i = 0; i < items.Length; i++)
            {
                float dist = Vector3.Distance(items[i].transform.position, this.transform.position);

                if (dist <= 0.5)
                {
                    Destroy(items[i]);

                    //bombTime += 3;

                    float ran = Random.value;
                    if (ran <= 0.33) { roundTime += 4; }
                    if (ran > 0.33 && ran <= 0.66) { speedTime += 3; }
                    if (ran > 0.66) { bombTime += 3; }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
        }
    }
}
