using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePackage : MonoBehaviour
{
    public void ReuseAfterFall()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 2f);
        }

    }
}
