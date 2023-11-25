using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject firstMenu;
    public GameObject secondMenu;
    public GameObject pos1;
    public GameObject pos2;
    float speed;

    public void StartGame()
    {
        firstMenu.SetActive(false);
        secondMenu.SetActive(true);
        speed = 30.0f;
    }
    public void CreditsLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Return()
    {
        SceneManager.LoadScene(0);
    }

    public void LetsRoll()
    {
        SceneManager.LoadScene(2);
    }
    public void Backout()
    {
        SceneManager.LoadScene(0);
    }

    public void FixedUpdate()
    {
        pos1.transform.position = Vector3.MoveTowards(pos1.transform.position, pos2.transform.position, speed * Time.deltaTime);
    }

}
