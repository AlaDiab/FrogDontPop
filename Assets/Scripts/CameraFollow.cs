using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Player player;
    private float highestHeight = 0.82f;

    void Update()
    {
        player = (player == null) ? FindObjectOfType<Player>() : player;

        // Move camera with player, but lock it to the floor
        if (transform.position.y > highestHeight)
        {
            highestHeight = transform.position.y;
        }
        transform.position = new Vector3(0, Mathf.Clamp(player.transform.position.y, highestHeight - 2, 10000), -10);
    }
}
