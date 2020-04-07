using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropShadow : MonoBehaviour
{
    public Vector3 offset;
    GameObject shadow;

    // Start is called before the first frame update
    void Start()
    {
        shadow = new GameObject("Shadow");
        shadow.transform.SetParent(transform);
        SpriteRenderer shadowSprite = shadow.AddComponent<SpriteRenderer>();
        shadowSprite.sprite = transform.GetComponent<SpriteRenderer>().sprite;
        shadowSprite.color = new Color(0.7f,0.7f,0.7f);
        shadow.transform.position = transform.position + offset;
        shadow.transform.rotation = transform.rotation;
        shadow.transform.localScale = new Vector3(1,1,1);
    }

}
