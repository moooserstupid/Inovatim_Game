using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class SpawnLocation : MonoBehaviour
    {

        public bool fullOrNot = false;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(NotFull());
        }

        IEnumerator NotFull()
        {
            while (true)
            {
                yield return new WaitForSeconds(30);
                fullOrNot = false;
            }

        }

        public void TakePackage()
        {
            if (fullOrNot == false) return;
            fullOrNot = false;
            //GetComponentInChildren<FakePackage>().ReuseAfterFall();

        }

    }
}