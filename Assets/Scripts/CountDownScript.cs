using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountDownScript : MonoBehaviour
{
    public bool playcountdown;
    public int seconds;
    public int minutes;
    public TextMeshProUGUI timer;
    void Start()
    {
        StartCoroutine(countdown() );
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
