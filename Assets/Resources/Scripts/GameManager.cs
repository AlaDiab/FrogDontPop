using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

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

    // User interface
    public GameObject startButton;
    public GameObject title;
    public TextMeshProUGUI heightDisplay;
    public TextMeshProUGUI fliesEatenDisplay;
    public GameObject gameOverScreen;

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

        // Update scores
        heightDisplay.text = $"Height: {Mathf.Round(player.heighestHeight * 10f) / 10f} meters";
        fliesEatenDisplay.text = $"Flies Eaten: {player.fliesEaten}";
    }

    public void StartGame()
    {
        title.SetActive(false);
        startButton.SetActive(false);
        gameStarted = true;
    }

    public void RestartGame()
    {
        // Reload the current active scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
