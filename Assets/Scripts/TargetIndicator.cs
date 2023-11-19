using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public Transform[] target;
    public float rotationSpeed;
    void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TargetA"))
        {
            gameObject.SetActive(true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target[0].position - transform.position),
            rotationSpeed * Time.deltaTime);
        }
        else if (collision.gameObject.CompareTag("TargetB"))
        {
            gameObject.SetActive(true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target[1].position - transform.position),
            rotationSpeed * Time.deltaTime);
        }
        else if (collision.gameObject.CompareTag("TargetC"))
        {
            gameObject.SetActive(true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target[2].position - transform.position),
            rotationSpeed * Time.deltaTime);
        }
        else if (collision.gameObject.CompareTag("TargetD"))
        {
            gameObject.SetActive(true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target[3].position - transform.position),
            rotationSpeed * Time.deltaTime);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
