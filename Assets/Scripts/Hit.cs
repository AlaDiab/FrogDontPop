using UnityEngine;

public class Hit : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);

        // Example: Check for a specific tag
        if (other.CompareTag("Fly"))
        {
            Debug.Log("We got you, bitch");
        }
    }
}
