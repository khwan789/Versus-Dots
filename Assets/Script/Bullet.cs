using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Player myPlayer;

    float speed = 3;
    Vector3 towards;

	// Use this for initialization
	void Start () {
        myPlayer = GameObject.Find("player").GetComponent<Player>();
        towards = myPlayer.transform.right;
    }
	
	// Update is called once per frame
	void Update () {

        this.transform.position += towards* Time.deltaTime * speed;         
    }
}
