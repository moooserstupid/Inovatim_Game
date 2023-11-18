using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{


    public GameObject Box;
    private GameObject randomSpawnSpot;
    private int timer = 0;
    //public GameObject boxpositions;
    public GameObject[] boxPositonList;
    public int gerisayimCount = 0;
    private bool go = true;
    private Vector3 V;
    private Quaternion Q = Quaternion.identity;
    
        void Start()
    {
        //boxPositonList = boxpositions.GetComponents(typeof(GameObject));
        StartCoroutine(BoxGen());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator BoxGen()
    {
        while (go)
        {
            randomSpawnSpot = boxPositonList[Random.Range(0, boxPositonList.Length)];
            if (randomSpawnSpot.GetComponent<Full>().fullOrNot == false)
            {
                V = new Vector3(randomSpawnSpot.transform.position.x, randomSpawnSpot.transform.position.y, randomSpawnSpot.transform.position.z);
                Instantiate(Box, V,Q) ;
                new WaitForSeconds(30 - gerisayimCount);
                Debug.Log(2);
                go = false;
                randomSpawnSpot.GetComponent<Full>().fullOrNot = true;
            }

            for (int i = 0; i < boxPositonList.Length; i++)
            {

                if (boxPositonList[i].GetComponent<Full>().fullOrNot == false)
                {
                    go = true;
                    Debug.Log(123);
                    break;
                }    
            }
            yield return new WaitForSeconds(1);

        }
        Debug.Log(go);
        
    }
}

