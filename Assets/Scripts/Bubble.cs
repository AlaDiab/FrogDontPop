using UnityEngine;

public class Bubble : MonoBehaviour
{
    private float originalHeight;
    private Player player;
    private CapsuleCollider2D cc;
    private bool sinking;
    
    void Start()
    {
        // Get references
        player = FindObjectOfType<Player>();
        cc = GetComponent<CapsuleCollider2D>();
        originalHeight = transform.position.y;
    }

    void Update()
    {
        // Allow the player to jump through the bubble to come up on top
        cc.isTrigger = player.transform.position.y + player.jumpOffset < transform.position.y;

        // Bubbles sink when the player is on them
        transform.position += (sinking) ? new Vector3(0, -0.01f, 0) : Vector3.zero;
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
