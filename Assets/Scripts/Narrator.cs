using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Narrator : MonoBehaviour
{
    public AudioClip[] lines;
    public int[] played;
    public Collider2D[] triggers;
    public AudioSource source;
    public Vector3 place, size;
    public Transform obj;
    public TextMeshProUGUI end;
    private bool cantOpen=false,afterMaze=false,waitCake=false;
    public Transform cantOpenDoor;
    private float timer,cakeTime;
    private int cantOpenIndex=12,afterMazeIndex = 21;

    Transform o;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        played = new int[lines.Length];
    }

    public void PlayClip(int i)
    {
        source.clip = lines[i];
        source.Play();
        played[i]++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == triggers[0] && played[1] == 0)
        {
            PlayClip(1);
        }else if (collision == triggers[1] && played[2] == 0)
        {
            PlayClip(2);
        }else if (collision == triggers[2] && played[3] == 0)
        {
            PlayClip(3);
        }else if (collision == triggers[3] && played[4] == 0)
        {
            PlayClip(4);
        }else if (collision == triggers[4] && played[5] == 0)
        {
            PlayClip(5);
        }else if (collision == triggers[6] && played[10] == 0)
        {
            PlayClip(10);
        }else if (collision == triggers[7] && played[17] == 0 && played[11]==0)
        {
            PlayClip(11);
            cantOpen = true;
        }else if (collision == triggers[8] && played[18] == 0)
        {
            PlayClip(18);
        }else if (collision == triggers[9] && played[19] == 0)
        {
            PlayClip(19);
        }else if (collision == triggers[10] && played[20] == 0)
        {
            PlayClip(20);
        }else if (collision == triggers[11])
        {
            afterMaze = true;
        }else if (collision == triggers[12] && played[24] == 0)
        {
            PlayClip(24);
        }else if (collision == triggers[13] && played[25] == 0)
        {
            PlayClip(25);
        }else if (collision == triggers[14] && played[26] == 0)
        {
            PlayClip(26);
        }else if (collision == triggers[15] && played[27] == 0)
        {
            PlayClip(27);
            waitCake = true;
            end.color = new Color(1,1,1,1);
        }
    }

    private void FixedUpdate()
    {
        if (waitCake)
        {
            Debug.Log(source.time);
            if(Mathf.Abs(source.time - 112f) < 0.5)
            {
                if (o == null)
                {
                    o = Instantiate(obj, place, Quaternion.identity);
                    o.localScale = size;
                }
            }
        }
        timer += Time.deltaTime;
        if(cantOpenDoor.position.y < 97 && cantOpen)
        {
            PlayClip(17);
            cantOpen = false;
        }
        if (source.isPlaying)
        {
            timer = 0;
        }
        if (cantOpen && timer > 4)
        {
            if(played[cantOpenIndex] == 0 && !source.isPlaying && cantOpenIndex < 17)
            {
                PlayClip(cantOpenIndex);
                cantOpenIndex++;
            }
        }
        if(afterMaze && timer > 4)
        {
            if (played[afterMazeIndex] == 0 && !source.isPlaying && afterMazeIndex < 24)
            {
                PlayClip(afterMazeIndex);
                afterMazeIndex++;
            }
        }
    }
}
