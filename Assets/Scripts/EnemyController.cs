using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private void Update()
    {
        var step = moveSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, -12f), step);
    }
}
