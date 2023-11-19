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
        //gameObject.SetActive(false);
    }
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
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("TargetA"))
    //    {
    //        gameObject.SetActive(true);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target[0].position - transform.position),
    //        rotationSpeed*Time.deltaTime);
    //    }
    //    else if (collision.gameObject.CompareTag("TargetB"))
    //    {
    //        gameObject.SetActive(true);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target[1].position - transform.position),
    //        rotationSpeed * Time.deltaTime);
    //    }
    //    else if (collision.gameObject.CompareTag("TargetC"))
    //    {
    //        gameObject.SetActive(true);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target[2].position - transform.position),
    //        rotationSpeed * Time.deltaTime);
    //    }
    //    else if (collision.gameObject.CompareTag("TargetD"))
    //    {
    //        gameObject.SetActive(true);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target[3].position - transform.position),
    //        rotationSpeed * Time.deltaTime);
    //    }
    //    else
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}
}
