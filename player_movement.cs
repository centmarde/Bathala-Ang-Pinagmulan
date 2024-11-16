using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Public variables for speed and jump force
    public float speed = 5f;
    public float jumpForce = 5f;

    // Private variables
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Check if the Rigidbody2D component is attached
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing!");
        }

        // Freeze rotation to keep the player upright
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Handle horizontal movement
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(speed * move, rb.linearVelocity.y);

        // Handle jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // Check if the player is grounded using collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isGrounded = true;
            Debug.Log("Player is grounded.");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isGrounded = false;
            Debug.Log("Player left the ground.");
        }
    }
}
