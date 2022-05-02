using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPull : MonoBehaviour
{
    public bool isPullActive = false;
    public bool isAttackActive = true;
    PlayerController player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    IEnumerator Start()
    {
        bool gravitySwitch = false;

        while (isAttackActive)
        {
            if (gravitySwitch)
            {
                StartCoroutine(ActivatePull());
                gravitySwitch = !gravitySwitch;
                yield return new WaitForSeconds(3f);
            }

            else
            {
                StartCoroutine(DeactivatePull());
                gravitySwitch = !gravitySwitch;
                yield return new WaitForSeconds(6f);
            }
        }
    }

    void Update()
    {
        if (isPullActive && Vector3.Distance(transform.position, player.transform.position) <= 4)
            player.moveSpeed = 4f;
        else
            player.moveSpeed = 7.5f;
    }

    IEnumerator ActivatePull()
    {
        Vector3 scale = new Vector3(2, 2, 2);

        while(Vector3.Distance(transform.localScale, scale) >= 1)
        {
            yield return new WaitForSeconds(0.05f);
            transform.localScale *= 1.05f;
        }

        isPullActive = true;
    }

    IEnumerator DeactivatePull()
    {
        Vector3 scale = new Vector3(1.5f, 1.5f, 1.5f);

        while (Vector3.Distance(transform.localScale, scale) <= 1)
        {
            yield return new WaitForSeconds(0.05f);
            transform.localScale *= 0.95f;
        }

        isPullActive = false;
    }
}
