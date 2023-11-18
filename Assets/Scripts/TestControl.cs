using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControl : MonoBehaviour
{
    float hiz = 20f;
    

    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        { transform.Translate(0f, 0f, hiz * Time.deltaTime); }
        if (Input.GetKey(KeyCode.S))
        { transform.Translate(0f, 0f, -hiz * Time.deltaTime); }
        if (Input.GetKey(KeyCode.A))
        { transform.Translate(-hiz * Time.deltaTime, 0f, 0f); }
        if (Input.GetKey(KeyCode.D))
        { transform.Translate(hiz * Time.deltaTime, 0f, 0f); }
    }

}

