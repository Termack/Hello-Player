using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAction : MonoBehaviour
{
    public Transform target;
    Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        Vector3 point = Camera.main.WorldToViewportPoint(target.position);
        Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, 0.1f);
    }
}
