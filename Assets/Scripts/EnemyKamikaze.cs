using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikaze : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float step;
    [SerializeField] private float endOfGame = -20f;
    [SerializeField] private float playerOffsetY = 4f;
    [SerializeField] private float lastPlayerPosition;

    Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        step = moveSpeed * Time.deltaTime; // calculate distance to move

        if (transform.position.y > player.transform.position.y)
        {
            MoveTowardsPlayer();
            lastPlayerPosition = player.transform.position.x;
        }
        else
            MoveDown();
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(player.position.x, player.position.y - playerOffsetY),
            step);
    }

    private void MoveDown()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(lastPlayerPosition, endOfGame),
            step);
    }
}
