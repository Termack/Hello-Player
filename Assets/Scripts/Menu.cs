using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public bool hasClicked = false;
    public TextMeshProUGUI title,subtitle;
    public Controller controller;
    public Narrator narrator;
    private TextMeshProUGUI txt;

    IEnumerator StartGame()
    {
        while (subtitle.text.Length > 0)
        {
            subtitle.text = subtitle.text.Remove(subtitle.text.Length-1);
            yield return new WaitForSeconds(0.05f);
        }
        while (title.text.Length > 0)
        {
            title.text = title.text.Remove(title.text.Length - 1);
            yield return new WaitForSeconds(0.15f);
        }
        Camera.main.GetComponent<CamAction>().enabled = true;
        Camera.main.orthographicSize = 16;
        controller.enabled = true;
    }

    private void Update()
    {
        if (Input.anyKeyDown && !hasClicked)
        {
            hasClicked = true;
            StartCoroutine("StartGame");
            narrator.PlayClip(0);
        }
    }
}
