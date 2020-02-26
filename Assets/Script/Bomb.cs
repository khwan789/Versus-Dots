using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    SphereCollider sCollider;
    // Start is called before the first frame update
    void Start()
    {
        sCollider = this.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale += new Vector3(1f,1f,0) * Time.deltaTime;
        sCollider.transform.localScale += new Vector3(1f, 1f, 0) * Time.deltaTime;

    }
}


        
