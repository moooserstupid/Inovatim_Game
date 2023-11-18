using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool Gamestopped = false;
    public GameObject Pausemenu;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (Gamestopped)
            { Resume(); }
            else
            { Stop(); }
        }
    }
    public void Resume()
    {
        Pausemenu.SetActive(false);
        Time.timeScale = 1f;
        Gamestopped = false;
    }
    public void Stop()
    {
        Pausemenu.SetActive(true);
        Time.timeScale = 0f;
        Gamestopped = true;
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
