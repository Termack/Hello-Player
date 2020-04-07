using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenKey : MonoBehaviour
{
    public Transform door;
    public Vector3 destination,velocity = Vector3.zero;
    public bool open = false;

    private void Awake()
    {
        Vector3 bounds = door.GetComponent<Collider2D>().bounds.size;
        float direction = (bounds.y > bounds.x ? bounds.y : bounds.x);
        destination = door.position + (door.up * direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider == door.GetComponent<Collider2D>())
        {
            open = true;
        }
    }

    private void Update()
    {
        if (open && door.position != destination)
        {
            door.position = Vector3.SmoothDamp(door.position, destination, ref velocity, 0.1f);
        }
    }
}
