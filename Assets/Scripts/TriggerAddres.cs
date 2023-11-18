using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAddres : MonoBehaviour
{
    public string Addres;
    private string[] addresList = { "A", "B", "C", "D" };
    public bool damaged = false;
    // Start is called before the first frame update
    void Start()
    {
        Addres = addresList[Random.Range(0, addresList.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.name == Addres)
        {
            if (damaged)
            {
                GameObject go = GameObject.Find("Currency");
                go.GetComponent<Currnecy>().EarnMoney(true);

            }
            else
            {
                GameObject go = GameObject.Find("Currency");
                go.GetComponent<Currnecy>().EarnMoney(false);
            }

            Object.Destroy(this.gameObject);
        }
        else if (other.tag == "Address")
        {
            GameObject go = GameObject.Find("Currency");
            go.GetComponent<Currnecy>().LoseMoney("wrongaddress");
            Object.Destroy(this.gameObject);
        }

    }
}
