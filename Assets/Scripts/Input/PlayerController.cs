using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables
    [SerializeField] private float moveSpeed = 7.5f;
    [SerializeField] private bool isTurning = false;
    [SerializeField] private Vector2 movement;

    // components
    private Animator animator;

    // scripts
    private ProjectileManager projectileManager;

    // Screen bounds
    Vector2 minBounds;
    Vector2 maxBounds;
    [SerializeField] float paddingX = 0.5f;
    [SerializeField] float paddingY = 0.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        projectileManager = GetComponent<ProjectileManager>();
        InitBounds();
    }


    void Update()
    {
        GetInput();
        AnimateShipTurns();
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
        Vector2 delta = movement * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingX, maxBounds.x - paddingX);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingY, maxBounds.y - paddingY);


        transform.position = newPos;
    }

    private void GetInput()
    {
        // input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void AnimateShipTurns()
    {
        animator.SetFloat("Horizontal", movement.x);

        if (movement.x == 0)
            isTurning = false;
        else
            isTurning = true;

        animator.SetBool("IsTurning", isTurning);
    }
}
