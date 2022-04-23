using UnityEngine;

public class Projectile : MonoBehaviour
{
    // variables
    private float velocity = 300f;

    // components
    private Rigidbody2D rigidbody2d;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidbody2d.velocity = new Vector2(0, velocity * Time.fixedDeltaTime);
    }
}
