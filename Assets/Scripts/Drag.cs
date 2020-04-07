using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public float frequency, force, damp;
    private float gravity, mass;
    TargetJoint2D joint;

    private void Awake()
    {
        mass = transform.GetComponent<Rigidbody2D>().mass;
        gravity = transform.GetComponent<Rigidbody2D>().gravityScale;
    }

    private void OnMouseDown()
    {
        transform.GetComponent<Rigidbody2D>().mass = mass/10;
        transform.GetComponent<Rigidbody2D>().gravityScale = 0;
        joint = gameObject.AddComponent<TargetJoint2D>();
        joint.frequency = frequency;
        joint.maxForce = force;
        joint.dampingRatio = damp;
        joint.autoConfigureTarget = false;
        //Vector3 dist = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //joint.anchor = new Vector2(dist.x/transform.lossyScale.x,dist.y/transform.lossyScale.y);
    }

    private void OnMouseDrag()
    {
        transform.GetComponent<TargetJoint2D>().target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        transform.GetComponent<Rigidbody2D>().gravityScale = gravity;
        transform.GetComponent<Rigidbody2D>().mass = mass;
        Destroy(joint);
    }
}
