using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        var step = moveSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector2.MoveTowards(
            transform.position
            , new Vector2(player.position.x, player.position.y - 4f)
            , step);

    }
}
