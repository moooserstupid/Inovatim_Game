using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public int life = 30;
    public bool alive = true;
    public Color highLife;
    public Color highMid;
    public Color highLow;


    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        StartCoroutine(Live());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Live()
    {
        while (alive)
        {
            if (life <= (life) * (1 / 3))
            {
                this.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
            }
            else if(life <= life * (2 / 3))
            {
                this.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            }

            if(life == 0)
            {
                alive = false;
                Object.Destroy(this.gameObject);
            }
            yield return new WaitForSeconds(1);
            life -= 1;
        }
    }
}
