using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MenuScript : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(2);
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

    }
}