using UnityEngine;

public class Player : MonoBehaviour
{
    // References
    [HideInInspector]
    public Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;

    // Player variables
    public float movementSpeed;
    public float jumpForce;
    private bool onGround;
    public bool canMoveInAir;
    public float jumpOffset;
    private SpriteRenderer sr;
    public Sprite[] sprites;
    public int fliesEaten;

    // Ground check variables
    public LayerMask groundLayer; // Layer to identify the ground
    public float groundCheckDistance = 0.1f; // Distance for raycast to detect ground

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
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
            if (!canMoveInAir)
            {
                float horizontalInput = ControlsManager.Stick(0).x;
                rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
            }
        }

        // Change sprites
        sr.sprite = (onGround) ? sprites[0] : sprites[1];
        if (!onGround)
        {
            sr.flipX = (ControlsManager.Stick(0).x == 0 && !onGround) ? sr.flipX : (ControlsManager.Stick(0).x > 0);
        }

        if (canMoveInAir && !onGround)
        {
            float horizontalInput = ControlsManager.Stick(0).x;
            rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
        }

        // Handle mouse click raycast
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            HandleMouseClickRaycast();
        }

        // Edit size of frog based on the flies eaten
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

    void HandleMouseClickRaycast()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set Z to 0 for 2D space

        // Calculate the direction from the player to the mouse position
        Vector2 rayDirection = (mousePosition - transform.position).normalized;

        // Cast a ray in the direction of the mouse position
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection);

        // Draw a debug line from the player to the mouse position
        Debug.DrawLine(transform.position, mousePosition, Color.green, 2f); // Line lasts for 2 seconds

        // Log what the ray hit (if anything)
        if (hit.collider != null)
        {
            Debug.Log($"Ray hit: {hit.collider.name}");
        }
        else
        {
            Debug.Log("Ray hit nothing.");
        }
    }
}
