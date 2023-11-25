using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorScript : MonoBehaviour
{
    public Transform[] stops;
    public int Movespeed = 20;
    int Count;
    void Start()
    {
        Count = 0;
        transform.position = Vector3.MoveTowards(transform.position, stops[Count].position, Movespeed * Time.deltaTime);
    }
    void Update()
    {
        if (transform.position == stops[Count].position)
        {
            Count++;
        }
        if (Count >= stops.Length)
        {
            Count = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, stops[Count].position, Movespeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)//oyuncunun harektli zemin ile ayný anda hareket etmesi için 
    {
        if (collision.gameObject.tag == "TestTag")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit(Collision collision)//oyuncunun harektli zemin ile ayný anda hareket etmesi için 
    {
        if (collision.gameObject.tag == "TestTag")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
