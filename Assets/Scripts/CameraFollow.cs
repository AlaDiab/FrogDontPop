using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Player player;
    [HideInInspector]

    void Update()
    {
        player = (player == null) ? FindObjectOfType<Player>() : player;

        // Move camera with player, but lock it to the floor
        transform.position = new Vector3(0, Mathf.Clamp(player.transform.position.y, transform.position.y, 10000), -10);
    }
}
