using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObj : MonoBehaviour
{
    public Object obj;
    public Collider2D player;
    public bool killAll;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(obj);
            enabled = false;
        }else if (killAll)
        {
            Destroy(collision.gameObject);
        }
    }
}
