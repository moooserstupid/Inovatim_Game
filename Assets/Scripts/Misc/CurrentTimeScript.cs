using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentTimeScript : MonoBehaviour
{
    public bool ShowCurrentTime;
    public TextMeshProUGUI timer;

    void Start()
    {
        StartCoroutine(CurrentTime());
    }

    IEnumerator CurrentTime()
    {
        while (ShowCurrentTime== true)
        {
            timer.text = System.DateTime.Now.ToShortTimeString();
            yield return null;
        }
    }
}
