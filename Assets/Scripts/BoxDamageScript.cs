using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDamageScript : MonoBehaviour
{
    public GameObject[] Boxes;
    int boxNo;
    void Start()
    {
        boxNo = 0;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Boxes[boxNo].gameObject.SetActive(false);
            boxNo++;
            Boxes[boxNo].gameObject.SetActive(true);
            if (boxNo == 2)
            {
                gameObject.SetActive(false);
                Destroy(this.gameObject, 2f);
            }
        }
    }    
}
