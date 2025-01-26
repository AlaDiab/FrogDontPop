using UnityEngine;

public class Wall : MonoBehaviour
{
    private Player player;

    void Update()
    {
        player = (player == null) ? FindObjectOfType<Player>() : player;

        // Follow the player
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }
}
