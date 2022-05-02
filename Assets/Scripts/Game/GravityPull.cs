using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPull : MonoBehaviour
{
    public bool isPullActive = false;
    public bool isAttackActive = true;
    PlayerController player;
    BossController bossController;
    List<Transform> waypoints;
    int waypointIndex = 0;
    [SerializeField] private float addMoveSpeed = 1f;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        bossController = GameObject.Find("Boss1(Clone)").GetComponent<BossController>();
    }

    IEnumerator Start()
    {
        waypoints = bossController.GetCurrentPath();
        transform.position = waypoints[waypointIndex].position;
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
            player.moveSpeed = 3f;
        else
            player.moveSpeed = 7.5f;

        FollowPath();
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


    private void FollowPath()
    {
        waypoints = bossController.GetCurrentPath();

        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = addMoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
                waypointIndex++;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
