using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    public float angle, relative;
    private GravityManager manager;
    private bool selected;
    private float gravity;
    private Rigidbody2D player;

    private void Awake()
    {
        relative = transform.parent.rotation.eulerAngles.y;
        angle = -45;
        manager = GameObject.Find("GravityManager").GetComponent<GravityManager>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected = true;
        }
    }

    void Update()
    {
        if (selected)
        {
            Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (angle >= 135 || angle <= -135)
            {
                float temp = (angle < 0 ? -1 : 1);
                angle = -temp * ((angle * temp) - 180);
            }
            else if (angle >= 45 && angle < 135)
            {
                angle = 45;
            }
            else if (angle <= -45 && angle > -135)
            {
                angle = -45;
            }
            if (angle > -1 && angle < 1)
            {
                angle = 0;
            }
            transform.rotation = Quaternion.Euler(0, relative, angle);
            GravityManager.angle = angle;
        }
        if (Input.GetMouseButtonUp(0))
        {
            selected = false;
        }
    }
}
