using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject player;
    public bool spiral;
    float x = 0;
    float speed = 1f;

    SphereCollider sCollider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        sCollider = this.GetComponent<SphereCollider>();


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
        this.transform.position += transform.right * Time.deltaTime * 2;
        this.transform.Rotate(new Vector3(0, 0, 50) * Time.deltaTime * speed);
    }


}



