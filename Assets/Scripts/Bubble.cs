using UnityEngine;

public class Bubble : MonoBehaviour
{
    private float originalHeight;
    private Player player;
    private BoxCollider2D cc;
    private bool sinking;
    public int direction = 0;
    private float movementSpeed = 0;
    
    void Start()
    {
        // Get references
        player = FindObjectOfType<Player>();
        cc = GetComponent<BoxCollider2D>();
        originalHeight = transform.position.y;

        // Generate speed
        movementSpeed = UnityEngine.Random.Range(0.2f, 1.0f);
    }

    void Update()
    {
        // Allow the player to jump through the bubble to come up on top
        cc.isTrigger = player.transform.position.y + player.jumpOffset < transform.position.y;

        // Bubbles sink when the player is on them
        transform.position += (sinking) ? new Vector3(0, -0.01f, 0) : Vector3.zero;

        // Move in the direction
        transform.position += new Vector3(direction * movementSpeed * Time.deltaTime, 0.0f, 0.0f);

        // Delete when outside of the map
        if (transform.position.x > 10.5f || transform.position.x < -10.5f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("collide");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("collide player");
            sinking = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            sinking = false;
        }
    }
}
