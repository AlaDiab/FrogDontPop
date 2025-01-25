using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Player player;

    void Update()
    {
        // Follow the player up
        player = (player == null) ? FindObjectOfType<Player>() : player;
        transform.position = new Vector3(transform.position.x, player.transform.position.y, -10);
    }
}
