using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public float runSpeed = 40f;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool attack = false;

    public Animator animator; // Reference to the Animator component
    public Rigidbody2D rb; // Reference to the Rigidbody2D for physics
    private SpriteRenderer spriteRenderer; // Reference to SpriteRenderer for flipping

    void Start()
    {
        // Automatically assign Animator and Rigidbody2D if not set in the Inspector
        if (animator == null) animator = GetComponent<Animator>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get horizontal input and update the "walk" parameter
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("walk", Mathf.Abs(horizontalMove));

        // Flip the character based on movement direction
        if (horizontalMove > 0)
        {
            spriteRenderer.flipX = false; // Face right
        }
        else if (horizontalMove < 0)
        {
            spriteRenderer.flipX = true; // Face left
        }

        // Check for jump input
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetTrigger("jump");
        }

        // Check for attack input
        if (Input.GetButtonDown("Fire1")) // Assuming "Fire1" is mapped to attack
        {
            attack = true;
            animator.SetTrigger("attack");
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement to the Rigidbody2D
        Vector2 targetVelocity = new Vector2(horizontalMove * Time.fixedDeltaTime, rb.linearVelocity.y);
        rb.linearVelocity = targetVelocity;

        // Reset attack state (if needed for one-shot attacks)
        if (attack)
        {
            attack = false;
        }
    }
}
