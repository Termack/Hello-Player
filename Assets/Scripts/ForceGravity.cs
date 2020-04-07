using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGravity : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GravityManager.angle = -45;
    }
}
