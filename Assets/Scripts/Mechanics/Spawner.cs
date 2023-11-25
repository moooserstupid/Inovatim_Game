using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Mechanics
{
    public class Spawner : MonoBehaviour
    {

        [SerializeField] private int[] boxQuantityList; 
        public GameObject[] Boxes;
        private GameObject randomSpawnSpot;
        public GameObject[] boxPositonList;
        public int gerisayimCount = 0;
        private bool go = true;
        private Vector3 V;
        private Quaternion Q = Quaternion.identity;

        void Start()
        {
            //StartCoroutine(BoxGen());

            StartCoroutine(GenerateBoxes());
        }

        IEnumerator GenerateBoxes()
        {
            for (int i = 0; i < boxQuantityList.Length; ++i)
            {
                for (int j = 0; j < boxQuantityList[i]; ++j)
                {
                    int randomSpawnSpotIndex = Random.Range(0, boxPositonList.Length);
                    randomSpawnSpot = boxPositonList[randomSpawnSpotIndex];
                    V = new Vector3(randomSpawnSpot.transform.position.x, randomSpawnSpot.transform.position.y, randomSpawnSpot.transform.position.z);
                    int boxIndex = Random.Range(0, Boxes.Length);
                    GameObject spawned = Instantiate(Boxes[boxIndex], V, Q);
                    switch (randomSpawnSpotIndex)
                    {
                        case 0:
                            spawned.GetComponent<Package>().SetAddress("A");
                            break;
                        case 1:
                            spawned.GetComponent<Package>().SetAddress("B");
                            break;
                        case 2:
                            spawned.GetComponent<Package>().SetAddress("C");
                            break;
                    }
                    spawned.transform.SetParent(randomSpawnSpot.transform);

                    yield return new WaitForSeconds(3f);
                }
            }
        }

        IEnumerator BoxGen()
        {
            while (go)
            {
                int randomSpawnSpotIndex = Random.Range(0, boxPositonList.Length);
                randomSpawnSpot = boxPositonList[randomSpawnSpotIndex];
                if (randomSpawnSpot.GetComponent<SpawnLocation>().fullOrNot == false)
                {
                    V = new Vector3(randomSpawnSpot.transform.position.x, randomSpawnSpot.transform.position.y, randomSpawnSpot.transform.position.z);
                    int boxIndex = Random.Range(0, Boxes.Length);
                    GameObject spawned = Instantiate(Boxes[boxIndex], V, Q);

                    switch (randomSpawnSpotIndex)
                    {
                        case 0:
                            spawned.GetComponent<Package>().SetAddress("A");
                            break;
                        case 1:
                            spawned.GetComponent<Package>().SetAddress("B");
                            break;
                        case 2:
                            spawned.GetComponent<Package>().SetAddress("C");
                            break;
                    }
                    spawned.transform.SetParent(randomSpawnSpot.transform);
                    randomSpawnSpot.GetComponent<SpawnLocation>().fullOrNot = true;
                    yield return new WaitForSeconds(5 - gerisayimCount);
                }
                go = false;

                for (int i = 0; i < boxPositonList.Length; i++)
                {

                    if (boxPositonList[i].GetComponent<SpawnLocation>().fullOrNot == false)
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
}