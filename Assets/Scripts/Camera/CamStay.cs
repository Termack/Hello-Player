using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamStay : MonoBehaviour
{
    public Collider2D player;
    public CamAction mainCamera;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Collider2D>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<CamAction>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == player)
        {
            mainCamera.target = transform.parent;
            Camera.main.orthographicSize = transform.lossyScale.y + 3;
        }
    }
}
