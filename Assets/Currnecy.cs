using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currnecy : MonoBehaviour
{
    public int money = 0;
    
    public int succes = 10;
    public int damagedSucces = 5;

    public int not_alive = 5;
    public int speedUp = 50;


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
        Debug.Log(money);
    }

    public void LoseMoney(string durum)
    {
        if (durum == "not_alive")
        {
            money -= not_alive;
        }
        else if (durum == "hýzlan")
        {
            money -= speedUp;
        }
        //else
        //{
        //    money -= shelf;
        //}
        Debug.Log(money);
    }

}


//private bool damaged = false;