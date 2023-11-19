using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Full : MonoBehaviour
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

}
