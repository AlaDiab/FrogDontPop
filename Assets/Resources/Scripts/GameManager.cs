using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Variables
    [HideInInspector]
    public List<Bubble> bubbles = new List<Bubble>();
    public GameObject skyPrefab; // Assign the prefab in the inspector
    private Player player;

    private Transform lastSkyPiece = null; // Track the y position of the last sky tile
    private float tileHeight = 10.8f;   // Height of each tile
    public Transform backgroundParent;
    public int bubbleSpacing;
    public bool gameStarted;

    void Start()
    {
        AudioManager.Play(true, "background");
        AudioManager.Play(true, "FrogDontPop");
    }

    void Update()
    {
        // Find the player if not already assigned
        player = player == null ? FindObjectOfType<Player>() : player;

        // Create first sky piece
        if (lastSkyPiece == null)
        {
            GameObject newSkyPiece = Instantiate(skyPrefab, new Vector3(0, 9.44f, 0), Quaternion.identity);
            newSkyPiece.transform.parent = backgroundParent;
            lastSkyPiece = newSkyPiece.transform;
        }

        // Check if we need to spawn a new sky prefab
        if (player.transform.position.y >= lastSkyPiece.position.y - 1)
        {
            // Instantiate a new sky prefab
            GameObject newSkyPiece = Instantiate(skyPrefab, new Vector3(0, lastSkyPiece.position.y + 10.8f, 0), Quaternion.identity);
            newSkyPiece.transform.parent = backgroundParent;
            lastSkyPiece = newSkyPiece.transform;
        }
    }
}
