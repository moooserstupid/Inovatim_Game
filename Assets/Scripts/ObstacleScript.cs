using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public Transform[] stops;
    public int Movespeed = 10;
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
}
