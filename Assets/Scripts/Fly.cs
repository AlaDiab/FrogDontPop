using UnityEngine;

public class Fly : MonoBehaviour
{
    // Movement settings
    public float moveSpeed = 1f;         // Base speed of the fly
    public float changeDirectionTime = 0.5f; // Time before changing direction
    public Vector2 movementArea = new Vector2(5f, 5f); // Bounds of the movement area

    private Vector2 movementDirection;  // Current movement direction
    private float timer;                // Timer to track direction changes
    private Vector2 startPosition;      // Initial position for area constraint
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();

        // Save the starting position
        startPosition = transform.position;

        // Choose an initial random direction
        movementDirection = GetRandomDirection();

        // Play sound
        AudioManager.Play(false, 3, 2);

        // Make movement speed faster as the playe eats more flies
        moveSpeed = 1 + (player.fliesEaten / 10);
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Change direction if the timer exceeds the set interval
        if (timer >= changeDirectionTime)
        {
            movementDirection = GetRandomDirection();
            timer = 0f;
        }

        // Move the fly in the chosen direction
        Vector2 newPosition = (Vector2)transform.position + movementDirection * moveSpeed * Time.deltaTime;

        // Clamp the position within the defined area
        newPosition.x = Mathf.Clamp(newPosition.x, startPosition.x - movementArea.x, startPosition.x + movementArea.x);
        newPosition.y = Mathf.Clamp(newPosition.y, startPosition.y - movementArea.y, startPosition.y + movementArea.y);

        // Apply the new position
        transform.position = newPosition;
    }

    // Generate a random direction for movement
    private Vector2 GetRandomDirection()
    {
        float angle = Random.Range(0f, 360f); // Random angle in degrees
        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized; // Convert to a unit vector

        sr.flipX = direction.x > 0; // Flip sprite if direction is to the right
        return direction;
    }

    // Optional: Visualize the movement area in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(startPosition, new Vector3(movementArea.x * 2, movementArea.y * 2, 0));
    }
}
