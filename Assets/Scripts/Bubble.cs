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
    public ParticleSystem popEffect;
    
    void Start()
    {
        // Get references
        player = FindObjectOfType<Player>();
        cc = GetComponent<BoxCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
        sr = GetComponent<SpriteRenderer>();
        originalHeight = transform.position.y;

        // Generate speed
        movementSpeed = UnityEngine.Random.Range(0.5f, 1.5f);
    }

    void Update()
    {
        // Disable bubbles if the game is over
        if (!gameManager.gameStarted)
        {
            cc.isTrigger = true;
        }
        else
        {
            // Allow the player to jump through the bubble to come up on top
            cc.isTrigger = player.transform.position.y + player.jumpOffset < transform.position.y;
        }

        // Bubbles sink when the player is on them
        if (sinking && player.onGround)
        {
            transform.position += (sinking) ? new Vector3(0, player.fliesEaten * -0.0005f, 0) : Vector3.zero;
            Color newColor = sr.color + new Color(0, 0, 0, player.fliesEaten * -0.0005f);
            newColor.a = Mathf.Clamp01(newColor.a); // Ensures alpha stays between 0 and 1
            sr.color = newColor;
        }

        // Move in the direction
        transform.position += new Vector3(direction * movementSpeed * Time.deltaTime, 0.0f, 0.0f);

        // Delete when outside of the map
        if (transform.position.x > 10.5f || transform.position.x < -10.5f || sr.color.a <= 0)
        {
            gameManager.bubbles.Remove(this);

            // Play sound if popped by the player
            if (sr.color.a <= 0)
            {
                AudioManager.Play(false, 2, 2);

                // Create pop effect
                ParticleSystem newEffect = Instantiate(popEffect);
                newEffect.transform.position = transform.position;
            }

            // Unparent the player
            player.transform.parent = null;

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
