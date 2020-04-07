using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlate : MonoBehaviour
{
    public bool isOn;
    public Transform door;
    public Vector3 on, off;
    public Vector3 destination,velocity = Vector3.zero;
    public Animator textMesh;
    public int aud;
    private Narrator narrator;


    private void Awake()
    {
        narrator = GameObject.Find("Narrator").GetComponent<Narrator>();
        textMesh = GameObject.Find("Opened").GetComponent<Animator>();
        float y = gameObject.GetComponent<Collider2D>().bounds.size.y;
        isOn = true;
        off = transform.position - (transform.up * y);
        on = transform.position;
        Vector3 bounds = door.GetComponent<Collider2D>().bounds.size;
        float direction = (bounds.y > bounds.x ? bounds.y : bounds.x);
        destination = door.position + (door.up * direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOn)
        {
            Action();
        }
    }

    public void Action()
    {
        if (narrator.played[aud] == 0)
        {
            narrator.PlayClip(aud);
        }
        textMesh.SetTrigger("Opened");
        isOn = false;
        transform.position = off;
        door.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void Update()
    {
        if (!isOn && door.position != destination)
        {
            door.position = Vector3.SmoothDamp(door.position, destination, ref velocity, 0.1f);
        }
    }
}
