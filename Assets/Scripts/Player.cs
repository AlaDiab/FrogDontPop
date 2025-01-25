using UnityEngine;

public class Player : MonoBehaviour
{
    // References
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;

    // Player variables
    public float movementSpeed;
    public float jumpForce;
    private bool onGround;

    // Ground check variables
    public LayerMask groundLayer; // Layer to identify the ground
    public float groundCheckDistance = 0.1f; // Distance for raycast to detect ground

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        // Check if player is on the ground
        onGround = IsGrounded();

        // Jump
        if (onGround && ControlsManager.Stick(0).y > 0 && rb.velocity.y == 0)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            // Movement in midair
            float horizontalInput = ControlsManager.Stick(0).x;
            rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
        }
    }

    bool IsGrounded()
    {
        // Raycast from the bottom of the capsule collider
        Vector2 raycastOrigin = new Vector2(transform.position.x, capsuleCollider.bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, groundCheckDistance, groundLayer);

        // Debug line to visualize the raycast in the Scene view
        Debug.DrawRay(raycastOrigin, Vector2.down * groundCheckDistance, Color.red);

        return hit.collider != null; // Returns true if the raycast hits something
    }
}