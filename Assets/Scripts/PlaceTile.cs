using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTile : MonoBehaviour
{
    public Vector3 place, size;
    public Transform obj;
    public Collider2D player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player)
        {
            Transform o = Instantiate(obj, place, Quaternion.identity);
            o.localScale = size;
            Destroy(this);
        }
    }
}
