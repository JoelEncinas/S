using UnityEngine;

public class Projectile : MonoBehaviour
{
    // variables
    [SerializeField] private float velocity = 600f;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("enemy1"))
        {
            collision.gameObject.SetActive(false);
            // add points
        }
    }
}
