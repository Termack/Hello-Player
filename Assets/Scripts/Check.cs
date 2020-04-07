using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public Controller controller;
    public int count;

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Non-Standable" && !collision.isTrigger)
        {
            controller.TriggerCheck(transform, false);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Non-Standable" && !collision.isTrigger)
        {
            controller.TriggerCheck(transform, true);
        }
    }


}
