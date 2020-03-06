using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    GameObject player;
    float speed = 0.3f;
    
	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");	
	}
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, step);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //gameend
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "Bomb")
        {
            Destroy(this.gameObject);
        }
    }
}
