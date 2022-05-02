using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPull : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        StartCoroutine(ActivatePull());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ActivatePull()
    {
        Vector3 scale = new Vector3(2, 2, 2);

        Debug.Log(Vector3.Distance(transform.localScale, scale));
        Debug.Log(transform.localScale * 1.2f);
        Debug.Log(Vector3.Distance(transform.localScale * 1.2f, scale));

        while(Vector3.Distance(transform.localScale, scale) >= 1)
        {
            transform.localScale *= 1.05f;
        }
    }
}
