using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikaze : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float step;
    [SerializeField] private float endOfGame = -12f;

    Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        step = moveSpeed * Time.deltaTime; // calculate distance to move

        if (transform.position.y > player.transform.position.y)
            MoveTowardsPlayer();
        else
            MoveDown();
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(player.position.x, player.position.y),
            step);
    }

    private void MoveDown()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(transform.position.x, endOfGame),
            step);
    }
}
