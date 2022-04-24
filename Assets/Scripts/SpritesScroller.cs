using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;

    Vector2 offset;
    Material material;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        moveSpeed = new Vector2(0, 0.001f);
    }

    private void Update()
    {
        material.mainTextureOffset += moveSpeed;
    }
}
