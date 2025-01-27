using UnityEngine;

public class Hit : MonoBehaviour
{
    private GameManager gameManager;
    private Player player;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Eat the fly
        if (other.CompareTag("Fly"))
        {
            player.fliesEaten++;
            Destroy(other.gameObject);
        }
    }
}
