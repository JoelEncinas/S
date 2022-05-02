using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;

    Material material;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        moveSpeed = new Vector2(0, 0.1f);
    }

    private void Update()
    {
        material.mainTextureOffset += moveSpeed * Time.deltaTime;
    }
}
