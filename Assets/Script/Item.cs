using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Gradient myGradient;
    float strobeDuration;
    // Start is called before the first frame update
    void Start()
    {
        strobeDuration = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        ColorChange();
    }

    void ColorChange()
    {
        float t = Mathf.PingPong(Time.time / strobeDuration, 1f);
        this.GetComponent<SpriteRenderer>().color = myGradient.Evaluate(t);
    }
}
