using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public Handle[] levers;
    public bool goUp,invert;
    public static float angle = -45;
    private float gravity;
    private Rigidbody2D player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        gravity = Physics2D.gravity.y / 45;
    }

    public void ChangeGravity()
    {
        Debug.Log(goUp);
        goUp = angle<0;
        invert = true;
        Debug.Log(goUp);

    }

    private void Update()
    {
        Physics2D.gravity = new Vector2(0, gravity * -angle);
        if (!goUp && angle != -45 && invert)
        {
            float a = 0;
            angle = Mathf.SmoothDamp(angle, -45, ref a, 0.05f);
            if (angle <= -44.5)
            {
                angle = -45;
            }
        }else if (goUp && angle != 45 && invert)
        {
            float a = 0;
            angle = Mathf.SmoothDamp(angle,45,ref a,0.05f);
            if(angle >= 44.5)
            {
                angle = 45;
            }
        }
        else if(angle == 45 || angle == -45)
        {
            invert = false;
        }
        changeAngle();
    }

    private void changeAngle()
    {
        foreach (Handle h in levers)
        {
            h.angle = angle;
        }
    }
}
