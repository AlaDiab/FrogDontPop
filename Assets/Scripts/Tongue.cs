using UnityEngine;

public class Tongue : MonoBehaviour
{
    private Player player;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("something eaten");
        if (collision.gameObject.tag == "Fly")
        {
            player.fliesEaten++;
            Destroy(collision.gameObject);
        }
    }
}
