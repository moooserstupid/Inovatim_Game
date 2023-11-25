using Managers;
using System.Collections.Generic;
using UI.FloatingText;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics
{
    public class Package : MonoBehaviour
    {
        [SerializeField] private GameObject[] packageList;
        public string Addres;
        private string[] addresList = { "A", "B", "C", "D" };

        [Header("Misc")]
        [SerializeField] private List<Sprite> objectiveImages;

        private int packageStateIndex;
        public bool damaged = false;
        public bool collected;
        private void Awake()
        {
            collected = false;
        }
        // Start is called before the first frame update
        void Start()
        {
            //Addres = addresList[Random.Range(0, addresList.Length)];
            
            //GetComponent<Rigidbody>().freezeRotation = true;
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
        public void PackageCollected()
        {
            Debug.Log("Collected");
            collected = true;
        }
        public bool HasPackageBeenCollected()
        {
            return collected;
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
            if (collected)
            {
                if (other.name == Addres)
                {
                    if (packageStateIndex > 0)
                    {
                        //GameObject go = GameObject.Find("Currency");
                        GameStateManager.Instance.EarnMoney(true, other.gameObject.transform.position + new Vector3(0, 0, 5), Addres);

                    }
                    else
                    {
                        //GameObject go = GameObject.Find("Currency");
                        GameStateManager.Instance.EarnMoney(false, other.gameObject.transform.position + new Vector3(0, 0, -5), Addres);
                    }

                    Destroy(gameObject);
                }
                else if (other.tag == "Address")
                {
                    //GameObject go = GameObject.Find("Currency");
                    //GameStateManager.Instance.LoseMoney("wrongaddress", other.gameObject.transform.position);
                    //Destroy(gameObject);
                }
            } else if (other.CompareTag("Package"))
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                BoxCollider collider = gameObject.GetComponent<BoxCollider>();
                rb.velocity = Vector3.zero;
                //rb.angularVelocity = Vector3.zero;
                rb.freezeRotation = true;
                rb.useGravity = false;
                rb.Sleep();
                collider.isTrigger = true;
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
            if (collected)
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
                    FloatingTextManager.Instance.ShowText("CAREFUL!", transform.position, Color.red);



                    if (packageStateIndex + 1 >= 3)
                    {
                        if (gameObject.activeInHierarchy)
                        {
                            gameObject.SetActive(false);
                            Destroy(gameObject, 2f);
                        }

                    }
                    else
                    {
                        packageList[packageStateIndex].gameObject.SetActive(false);
                        packageStateIndex++;
                        packageList[packageStateIndex].gameObject.SetActive(true);
                    }
                }
            } else
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                BoxCollider collider = gameObject.GetComponent<BoxCollider>();
                rb.velocity = Vector3.zero;
                //rb.angularVelocity = Vector3.zero;
                rb.freezeRotation = true;
                rb.useGravity = false;
                rb.Sleep();
                collider.isTrigger = true;
            }


        }
    }
}