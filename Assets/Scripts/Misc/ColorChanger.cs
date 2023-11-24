using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public int life = 30;
    public bool alive = true;
    public Material highLife;
    public Material midLife;
    public Material lowLife;


    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Renderer>().material = highLife;
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
                this.GetComponent<Renderer>().material = midLife;
            }
            else if(life <= life * (2 / 3))
            {
                this.GetComponent<Renderer>().material = lowLife;
            }

            if(life == 0)
            {
                alive = false;

                //GameObject go = GameObject.Find("Currency");
                GameStateManager.Instance.LoseMoney("not_alive", transform.position);

                Object.Destroy(this.gameObject);
            }
            yield return new WaitForSeconds(1);
            life -= 1;
        }
    }
}
