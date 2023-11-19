using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShow : MonoBehaviour
{
    public Transform[] target;
    public float rotationSpeed;
    int targetNo;
    void Start()
    {
        targetNo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target[targetNo].position - transform.position),
            rotationSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetNo++;
            
            if (targetNo == target.Length) 
            {
                targetNo = 0;
            }
        }
            //transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(target.position-transform.position),
            //rotationSpeed*Time.deltaTime);
    }
}
