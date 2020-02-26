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

        this.transform.position +=towards* Time.deltaTime * speed; 
            //Vector2.MoveTowards(this.transform.position, FindClosestEnemy().transform.position, step);
        
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
}
