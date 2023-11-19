using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoxTime
{
    public int minute;
    public int second;
}
public class TimerScript : MonoBehaviour
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
    public void SetTime(int seconds, int minutes)
    {
        this.seconds = seconds;
        this.minutes = minutes;
    }
    public BoxTime GetTime()
    {
        BoxTime boxTime = new BoxTime();
        boxTime.minute = minutes;
        boxTime.second = seconds;
        return boxTime;
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
