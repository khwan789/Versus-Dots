using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Player myPlayer;
    float speed;
    Vector3 towards;
    Vector2 screenBounds;
	// Use this for initialization
	void Start () {
        myPlayer = GameObject.Find("player").GetComponent<Player>();
        speed = 5;
        towards = myPlayer.transform.right;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
	
	// Update is called once per frame
	void Update () {

        this.transform.position += towards* Time.deltaTime * speed;
        OutOfBound();
    }

    void OutOfBound()
    {
        Vector2 myPos = this.gameObject.transform.position;
        if (myPos.x > screenBounds.x || myPos.x < -screenBounds.x || myPos.y > screenBounds.y || myPos.y < -screenBounds.y)
        {
            Destroy(this.gameObject);
        }
    }
}
