using UnityEngine;

public class Bubble : MonoBehaviour
{
    private float originalHeight;
    private Player player;
    private BoxCollider2D cc;
    private bool sinking;
    public int direction = 0;
    private float movementSpeed = 0;
    private GameManager gameManager;
    private SpriteRenderer sr;
    
    void Start()
    {
        // Get references
        player = FindObjectOfType<Player>();
        cc = GetComponent<BoxCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
        sr = GetComponent<SpriteRenderer>();
        originalHeight = transform.position.y;

        // Generate speed
        movementSpeed = UnityEngine.Random.Range(0.5f, 1.2f);
    }

    void Update()
    {
        // Allow the player to jump through the bubble to come up on top
        cc.isTrigger = player.transform.position.y + player.jumpOffset < transform.position.y;

        // Bubbles sink when the player is on them
        if (sinking)
        {
            transform.position += (sinking) ? new Vector3(0, player.fliesEaten * -0.0001f, 0) : Vector3.zero;
            Color newColor = sr.color + new Color(0, 0, 0, player.fliesEaten * -0.0001f);
            newColor.a = Mathf.Clamp01(newColor.a); // Ensures alpha stays between 0 and 1
            sr.color = newColor;
        }

        // Move in the direction
        transform.position += new Vector3(direction * movementSpeed * Time.deltaTime, 0.0f, 0.0f);

        // Delete when outside of the map
        if (transform.position.x > 10.5f || transform.position.x < -10.5f || sr.color.a <= 0)
        {
            // Unparent the player
            if (transform.childCount == 1)
            {
                transform.GetChild(0).parent = null;
            }
            
            gameManager.bubbles.Remove(this);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sinking = true;
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sinking = false;
            collision.transform.SetParent(null);
        }
    }
}
