using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool isTurning = false;
    [SerializeField] private Vector2 movement;

    // components
    private Rigidbody2D rigidbody2d;
    private Animator animator;

    // scripts
    private ProjectileManager projectileManager;

    // Screen bounds
    Vector2 minBounds;
    Vector2 maxBounds;


    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        projectileManager = GetComponent<ProjectileManager>();
        InitBounds();
    }


    void Update()
    {
        // input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);

        if (movement.x == 0)
            isTurning = false;
        else
            isTurning = true;

        animator.SetBool("IsTurning", isTurning);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void Move()     
    {
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + movement.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(transform.position.y + movement.y, minBounds.y, maxBounds.y);

        rigidbody2d.MovePosition(newPos + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
