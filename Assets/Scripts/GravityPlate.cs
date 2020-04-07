using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPlate : MonoBehaviour
{

    public bool isOn;
    public Vector3 on, off;
    public GravityManager manager;

    private void Awake()
    {
        isOn = true;
        off = transform.GetChild(0).position - (transform.GetChild(0).up * 0.2f);
        on = transform.GetChild(0).position;
        manager = GameObject.Find("GravityManager").GetComponent<GravityManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOn && !collision.isTrigger)
        {
            Action();
        }
    }

    public void Action()
    {
        manager.ChangeGravity();
        isOn = false;
        transform.GetChild(0).position = off;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isOn && !collision.isTrigger)
        {
            isOn = true;
            transform.GetChild(0).position = on;
        }
    }
}
