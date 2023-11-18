using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{


    public GameObject Box;
    private GameObject randomSpawnSpot;
    public GameObject[] boxPositonList;
    public int gerisayimCount = 0;
    private bool go = true;
    private Vector3 V;
    private Quaternion Q = Quaternion.identity;
    
        void Start()
    {
        StartCoroutine(BoxGen());
    }

    IEnumerator BoxGen()
    {
        while (go)
        {
            randomSpawnSpot = boxPositonList[Random.Range(0, boxPositonList.Length)];
            if (randomSpawnSpot.GetComponent<Full>().fullOrNot == false)
            {
                V = new Vector3(randomSpawnSpot.transform.position.x, randomSpawnSpot.transform.position.y, randomSpawnSpot.transform.position.z);
                Instantiate(Box,V,Q);
                randomSpawnSpot.GetComponent<Full>().fullOrNot = true;
                yield return new WaitForSeconds(5 - gerisayimCount);
            }
            go = false;

            for (int i = 0; i < boxPositonList.Length; i++)
            {

                if (boxPositonList[i].GetComponent<Full>().fullOrNot == false)
                {
                    go = true;
                    break;
                }    
            }
            yield return new WaitForSeconds(1);
            if (!go)
            {
                yield return new WaitForSeconds(20);
                go = true;
            }

        }
    }
}

