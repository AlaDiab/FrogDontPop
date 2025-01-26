using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    // References
    [HideInInspector]
    public Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private GameManager gameManager;
    public Camera camera;
    public GameObject tongue;
    private SpriteRenderer sr;

    // Player variables
    public float movementSpeed;
    public float jumpForce;
    [HideInInspector]
    public bool onGround;
    public bool canMoveInAir;
    public float jumpOffset;
    public Sprite[] sprites;
    public int fliesEaten;
    public float heighestHeight;

    // Ground check variables
    public LayerMask groundLayer; // Layer to identify the ground
    public float groundCheckDistance = 0.1f; // Distance for raycast to detect ground

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!gameManager.gameStarted)
        {
            return;
        }

        // Check if player is on the ground
        onGround = IsGrounded();
    
        // Jump
        if (onGround && ControlsManager.Stick(0).y > 0 && rb.velocity.y == 0)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            
            AudioManager.Play(false, 4, 2);
    
            // Movement in midair
            if (!canMoveInAir)
            {
                float horizontalInput = ControlsManager.Stick(0).x;
                if (!IsCollidingHorizontally(horizontalInput))
                {
                    rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
                }
            }
        }

        // Record height
        heighestHeight = (heighestHeight < transform.position.y) ? transform.position.y : heighestHeight;
    
        // Change sprites
        sr.sprite = (onGround) ? sprites[0] : sprites[1];
        if (!onGround)
        {
            sr.flipX = (ControlsManager.Stick(0).x == 0 && !onGround) ? sr.flipX : (ControlsManager.Stick(0).x > 0);
        }
    
        if (canMoveInAir && !onGround)
        {
            float horizontalInput = ControlsManager.Stick(0).x;
            if (!IsCollidingHorizontally(horizontalInput))
            {
                rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
            }
        }
    
        // Handle mouse click raycast
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            HandleMouseClickRaycast();
            StartCoroutine(ShootTongue());
        }
    
        // Edit size of frog based on the flies eaten
        float newSize = 0.5f + (fliesEaten * 0.005f);
        transform.localScale = new Vector3(newSize, newSize, newSize);

        // End the game if you fall too far down
        if (transform.position.y <= camera.transform.position.y - 7)
        {
            gameManager.gameStarted = false;
            gameManager.gameOverScreen.SetActive(true);
        }
    }
    
    bool IsCollidingHorizontally(float horizontalInput)
    {
        if (horizontalInput == 0) return false; // No input, no collision
    
        // Determine the direction of movement
        Vector2 direction = horizontalInput > 0 ? Vector2.right : Vector2.left;
    
        // Perform a raycast to detect collisions
        float rayDistance = capsuleCollider.bounds.extents.x + 0.1f; // A small buffer to check slightly ahead
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDistance, groundLayer);
    
        // Debug line for visualization
        Debug.DrawRay(transform.position, direction * rayDistance, Color.blue);
    
        // Return true if there's an obstacle in the direction of movement
        return hit.collider != null;
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
        float maxDistance = 2.5f; // Example max distance
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, maxDistance);

        // Draw a debug line from the player to the mouse position
        Debug.DrawLine(transform.position, mousePosition, Color.green, 1f); // Line lasts for 2 seconds

        // Log what the ray hit (if anything)
        if (hit.collider != null)// && hit.collider.tag != "Player")
        {
            Debug.Log($"Ray hit: {hit.collider.tag}");
            //fliesEaten++;
            //Destroy(hit.collider.gameObject);
        }
    }

    IEnumerator ShootTongue()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set Z to 0 for 2D space

        // Calculate the direction from the player to the mouse position
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Perform a raycast to detect hits within the maximum distance
        float maxDistance = 5f; // Maximum tongue reach
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance);

        // Determine the actual distance to stretch the tongue
        float distance = hit.collider != null ? hit.distance : maxDistance;

        // Point the tongue toward the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        tongue.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Stretch the tongue
        tongue.transform.localScale = new Vector3(distance, tongue.transform.localScale.y, tongue.transform.localScale.z);

        // Make the tongue visible
        tongue.SetActive(true);

        // Wait for a short duration to simulate the tongue reaching out
        yield return new WaitForSeconds(0.2f);

        // Reset the tongue
        tongue.SetActive(false);
        tongue.transform.localScale = new Vector3(1, tongue.transform.localScale.y, tongue.transform.localScale.z);
    }
}
