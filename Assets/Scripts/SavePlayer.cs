using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayer : MonoBehaviour
{
    public Vector3 where,where2;
    public Narrator narrator;
    private Transform player;
    public bool maze,key;
    public int times,limit,aud;
    private float timer;
    private bool canPlay = true;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        narrator = GameObject.Find("Narrator").GetComponent<Narrator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision == player.GetComponent<Collider2D>())
        {
            if (key)
            {
                if (!narrator.source.isPlaying)
                {
                    player.position = where;
                }
            }
            else {
                if (maze)
                {
                    if (timer > 5)
                    {
                        player.position = where;
                        timer = 0;
                    }
                    if (player.GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0))
                    {
                        timer += Time.deltaTime;
                    }
                    else
                    {
                        timer = 0;
                    }
                }
                else
                {
                    timer += Time.deltaTime;
                    Debug.Log(timer);
                    if (timer > 3 && Physics2D.gravity.y < 0)
                    {
                        if (narrator.played[aud] == 0 && canPlay && aud != 9)
                        {
                            narrator.PlayClip(aud);
                            aud++;
                            canPlay = false;
                        }
                        if (!narrator.source.isPlaying)
                        {
                            player.position = where;
                            canPlay = true;
                            timer = 0;
                            if (times >= limit && limit != 0)
                            {
                                player.position = where2;
                                narrator.PlayClip(9);
                                Destroy(this);
                            }
                            times++;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timer = 0;
    }

}
