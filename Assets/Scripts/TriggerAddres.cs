using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

public class TriggerAddres : MonoBehaviour
{
    //[SerializeField] private GameObject floatingTextPrefab;
    public string Addres;
    private string[] addresList = { "A", "B", "C", "D" };

    
    private bool fallen = true;
    public bool damaged = false;
    // Start is called before the first frame update
    void Start()
    {
        //Addres = addresList[Random.Range(0, addresList.Length)];
    }

    public void SetAddress(string address)
    {
        Addres = address;
    }
    public string GetAddress()
    {
        return Addres;
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

    public void ReuseAfterFall()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            BoxCollider collider = gameObject.GetComponent<BoxCollider>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.Sleep();
            collider.isTrigger = true;
            fallen = true;
            FloatingTextManager.Instance.ShowText("-50", transform.position, UnityEngine.Color.red);
        }
    }
}
