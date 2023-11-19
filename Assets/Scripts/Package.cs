using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using UI;
using UnityEngine;
using UnityEngine.UI;


public class Package : MonoBehaviour
{
    [SerializeField] private GameObject[] packageList;
    public string Addres;
    private string[] addresList = { "A", "B", "C", "D" };

    [Header("Misc")]
    [SerializeField] private List<Sprite> objectiveImages;

    private int packageStateIndex;
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
        switch (Addres)
        {
            case "A":
                GetComponentInChildren<Image>().sprite = objectiveImages[0];
                break;
            case "B":
                GetComponentInChildren<Image>().sprite = objectiveImages[1];

                break;
            case "C":
                GetComponentInChildren<Image>().sprite = objectiveImages[2];

                break;
            case "D":
                GetComponentInChildren<Image>().sprite = objectiveImages[3];
                break;

        }
    }
    public void SetPackageState(int packageStateIndex)
    {
        this.packageStateIndex = packageStateIndex;
    }
    public int GetPackageState()
    {
        return packageStateIndex; 
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
            if (packageStateIndex > 0)
            {
                //GameObject go = GameObject.Find("Currency");
                Currnecy.Instance.EarnMoney(true, other.gameObject.transform.position + new Vector3(0, 0, 5));

            }
            else
            {
                //GameObject go = GameObject.Find("Currency");
                Currnecy.Instance.EarnMoney(false, other.gameObject.transform.position + new Vector3(0, 0, -5));
            }

            Object.Destroy(this.gameObject);
        }
        else if (other.tag == "Address")
        {
            //GameObject go = GameObject.Find("Currency");
            Currnecy.Instance.LoseMoney("wrongaddress", other.gameObject.transform.position);
            Object.Destroy(this.gameObject);
        }

        

    }

    public void ReuseAfterFall()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 2f);
        }
        
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
            FloatingTextManager.Instance.ShowText("CAREFUL!", transform.position, UnityEngine.Color.red);

            
            
            if (packageStateIndex + 1 >= 3)
            {
                if (gameObject.activeInHierarchy)
                {
                    gameObject.SetActive(false);
                    Destroy(this.gameObject, 2f);
                }
                
            }
            else
            {
                packageList[packageStateIndex].gameObject.SetActive(false);
                packageStateIndex++;
                packageList[packageStateIndex].gameObject.SetActive(true);
            }
        }
    }
}
