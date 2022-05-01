using UnityEngine;

public class Projectile : MonoBehaviour
{
    // variables
    [SerializeField] private float velocity = 600f;

    // components
    private Rigidbody2D rigidbody2d;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidbody2d.velocity = new Vector2(0, velocity * Time.fixedDeltaTime);
    }
}
