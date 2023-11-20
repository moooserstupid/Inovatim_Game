using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class Currnecy : MonoBehaviour
{
    public static Currnecy Instance { get; private set; } 
    [SerializeField] private AudioClip moneyEarn;
    [SerializeField] private AudioClip fail;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TextMeshProUGUI textMesh;

    public int money = 0;
    
    public int succes = 10;
    public int damagedSucces = 5;

    public int not_alive = 5;
    public int speedUp = 50;

    public int goal = 50;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void EarnMoney(bool damaged, Vector3 worldPosition)

    {
        if (damaged == true)
        {
            money += damagedSucces;
            FloatingTextManager.Instance.ShowText("+$" + damagedSucces.ToString(), worldPosition, Color.green);
        }
        else
        {
            money += succes;
            FloatingTextManager.Instance.ShowText("+$" + succes.ToString(), worldPosition, Color.green);
        }

        if (money >= goal)
        {
            GameWon();
        }
        textMesh.text = money.ToString();
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

    public void LoseMoney(string durum, Vector3 worldPosition)
    {
        //if (durum == "not_alive")
        //{
        //    money -= not_alive;
        //}
        //else if (durum == "speed")
        //{
        //    money -= speedUp;
        //}
        if (durum == "wrongaddress")
        {
            money -= not_alive;
            FloatingTextManager.Instance.ShowText("-$" + not_alive.ToString(), worldPosition, Color.red);
        }
        if (money <= 0)
        {
            GameLost();
        }
        textMesh.text = money.ToString();
        Debug.Log(money);
        _audioSource.PlayOneShot(fail, 0.5F) ;
    }
}


//private bool damaged = false;