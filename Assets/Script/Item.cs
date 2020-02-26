using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Gradient myGradient;
    float strobeDuration = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ColorChange()
    {
        float t = Mathf.PingPong(Time.time / strobeDuration, 1f);
        this.GetComponent<SpriteRenderer>().color = myGradient.Evaluate(t);
    }
}
