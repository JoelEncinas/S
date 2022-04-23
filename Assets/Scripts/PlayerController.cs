using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private bool isTurning = false;
    [SerializeField] private Vector2 movement;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private Animator animator;


    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        // movement
        rigidbody2d.MovePosition(rigidbody2d.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
