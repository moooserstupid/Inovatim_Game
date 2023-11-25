using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class Spawner : MonoBehaviour
    {


        public GameObject[] Boxes;
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
                if (randomSpawnSpot.GetComponent<SpawnLocation>().fullOrNot == false)
                {
                    V = new Vector3(randomSpawnSpot.transform.position.x, randomSpawnSpot.transform.position.y, randomSpawnSpot.transform.position.z);
                    GameObject spawned = Instantiate(Boxes[Random.Range(0, Boxes.Length)], V, Q);
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