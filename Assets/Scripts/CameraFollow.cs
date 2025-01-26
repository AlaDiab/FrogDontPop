using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Player player;

    void Update()
    {
        player = (player == null) ? FindObjectOfType<Player>() : player;

        // Move camera with player, but lock it to the floor
        transform.position = new Vector3(0, Mathf.Clamp(player.transform.position.y, 0, 10000), -10);
    }

    /*public Transform target; // The player or object to follow
    public Rigidbody2D targetRigidbody; // Reference to the player's Rigidbody2D
    public float movementSpeed = 5f; // Speed for following the target
    public float verticalOffset = 2f; // Offset for vertical adjustment based on velocity
    public float smoothTime = 0.2f; // Smooth time for vertical offset adjustments

    private Vector3 velocity = Vector3.zero; // Used for smooth damp

    void Update()
    {
        // Base target position (follows the player but maintains a fixed Z-axis)
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10);

        // Adjust the vertical offset based on the player's velocity
        if (targetRigidbody != null)
        {
            float velocityY = targetRigidbody.velocity.y;
            targetPosition.y += Mathf.Sign(velocityY) * verticalOffset * Mathf.Abs(velocityY) / 10f; // Scale offset by velocity magnitude
        }

        // Smoothly move the camera towards the adjusted position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }*/
}
