using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables
    public float moveSpeed = 7.5f;
    [SerializeField] private bool isTurning = false;
    [SerializeField] private Vector2 movement;
    public bool isImmune;
    float immuneTime;

    // components
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;

    // scripts
    private ProjectileManager projectileManager;

    // Screen bounds
    Vector2 minBounds;
    Vector2 maxBounds;
    [SerializeField] float paddingX = 0.5f;
    [SerializeField] float paddingY = 0.5f;

    // sprites
    [SerializeField] private bool ship2;
    Sprite[] sprites = null;
    Sprite ship2front;

    void Awake()
    {
        animator = GetComponent<Animator>();
        projectileManager = GetComponent<ProjectileManager>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        isImmune = false;
        immuneTime = 5f;

        ship2front = Resources.Load<Sprite>("Ship2_front");
    }

    private void Start()
    {
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
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingY * 6, maxBounds.y - paddingY);


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

    IEnumerator MakePlayerImmune()
    {
        IImmuneAnimation();
        boxCollider2D.enabled = false;
        isImmune = true;

        yield return new WaitForSeconds(immuneTime);

        isImmune = false;
        boxCollider2D.enabled = true;
    }

    IEnumerator ImmuneAnimation()
    {
        float timer = 0;

        do
        {
            spriteRenderer.color = new Color32(255, 255, 255, 100);
            yield return new WaitForSeconds(0.25f);
            spriteRenderer.color = new Color32(255, 255, 255, 255);
            yield return new WaitForSeconds(0.25f);
            timer += 0.5f;
        } while (timer <= immuneTime);
    }

    public void IMakePlayerImmune()
    {
        StartCoroutine(MakePlayerImmune());
    }

    public void IImmuneAnimation()
    {
        StartCoroutine(ImmuneAnimation());
    }

    public bool isPlayerImmune()
    {
        return isImmune;
    }
}
