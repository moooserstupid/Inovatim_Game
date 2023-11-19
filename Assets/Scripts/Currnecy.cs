using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currnecy : MonoBehaviour
{

    [SerializeField] private AudioClip moneyEarn;
    [SerializeField] private AudioClip fail;
    [SerializeField] private AudioSource _audioSource;

    public int money = 0;
    
    public int succes = 10;
    public int damagedSucces = 5;

    public int not_alive = 5;
    public int speedUp = 50;

    public int goal = 50;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void EarnMoney(bool damaged)

    {
        if (damaged == true)
        {
            money += damagedSucces;
        }
        else
        {
            money += succes;
        }

        if (money >= goal)
        {
            GameWon();
        }
        Debug.Log(money);
        _audioSource.PlayOneShot(moneyEarn, 0.7F);
    }

    public void GameWon()
    {
        Debug.Log("Won");
    }

    public void GameLost()
    {
        Debug.Log("Lost");
    }

    public void LoseMoney(string durum)
    {
        if (durum == "not_alive")
        {
            money -= not_alive;
        }
        else if (durum == "speed")
        {
            money -= speedUp;
        }
        else if (durum == "wrongaddress")
        {
            money -= not_alive;
        }
        if (money <= 0)
        {
            GameLost();
        }
        Debug.Log(money);
        _audioSource.PlayOneShot(fail, 0.5F) ;
    }
}


//private bool damaged = false;