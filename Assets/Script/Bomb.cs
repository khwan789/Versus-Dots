using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject player;
    public bool spiral;
    float startAngle;
    float speed;

    SphereCollider sCollider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        sCollider = this.GetComponent<SphereCollider>();
        startAngle = 200f;
        speed = 1f;

    }

    // Update is called once per frame
    void Update()
    {
        /*this.transform.localScale += new Vector3(1f,1f,0) * Time.deltaTime;
        sCollider.transform.localScale += new Vector3(1f, 1f, 0) * Time.deltaTime;*/
        if (spiral)
        {
            SpiralMove();
        }

    }

    void SpiralMove()
    {
        this.transform.position += transform.right * Time.deltaTime * 3;
        this.transform.Rotate(new Vector3(0, 0, startAngle) * Time.deltaTime * speed);
        startAngle -= 35 * Time.deltaTime;
        Debug.Log(startAngle);
        if(startAngle <= 0)
        {
            Destroy(this.gameObject);
        }
    }


}



