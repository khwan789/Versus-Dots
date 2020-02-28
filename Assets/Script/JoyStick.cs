using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    //Joystick
    Vector2 initialPos;
    bool mouseDown;
    Vector2 movingPos;
    Vector2 direction;

    //player
    float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Joystick
        mouseDown = false;
        //player
        moveSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Click();
        Vector3 newDirection = new Vector3(direction.x, direction.y, 0);
        this.transform.position += newDirection * Time.deltaTime * moveSpeed;
    }

    void Click()
    {
        if (Input.GetMouseButton(0)) //mouseDown
        {
            if (!mouseDown)
            {
                initialPos = Input.mousePosition;
                mouseDown = true;
            }
            movingPos = Input.mousePosition;
            direction = (movingPos - initialPos).normalized;
        }
        else //mouseUp
        {
            direction = new Vector2(0, 0);
            mouseDown = false;
        }
    }
}
