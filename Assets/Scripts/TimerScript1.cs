using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript1 : MonoBehaviour
{
    public bool playcountdown;
    public int seconds;
    public int minutes;
    public TextMeshProUGUI timer;
    void Start()
    {
        StartCoroutine(countdown() );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator countdown()
    {
        while (playcountdown == true)
        {
            if (seconds > 0)
            {
                seconds--;
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                seconds = 60;
                minutes--;
            }
            timer.text = string.Format("{00:00}:{01:00}", minutes, seconds);

            if (minutes == 0 & seconds == 0)
            {
                playcountdown = false;
                Destroy(gameObject); 
            }
        }
    }
}
