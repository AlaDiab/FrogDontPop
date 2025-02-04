using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Wall : MonoBehaviour
{
    private Player player;
    public Bubble bubblePrefab;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        // Start creating bubbles
        StartCoroutine(CreateBubble());
    }

    void Update()
    {
        player = (player == null) ? FindObjectOfType<Player>() : player;

        // Follow the player
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }

    IEnumerator CreateBubble()
    {
        // Create the bubble and add it to the list
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 1.5f));
        
        if (gameManager.gameStarted)
        {
            Bubble bubble = Instantiate(bubblePrefab);
            gameManager.bubbles.Add(bubble);

            // Find the height of the bubble
            int heightSelection = 1;
            while (gameManager.bubbles.FirstOrDefault(b => b.transform.position.y == (int)player.transform.position.y + 1 + heightSelection) != null)
            {
                heightSelection += gameManager.bubbleSpacing;
            }
            bubble.transform.position = new Vector3(transform.position.x, (int)player.transform.position.y + 1 + heightSelection, 0);//new Vector3(transform.position.x, (int)player.transform.position.y + gameManager.heights[heightSelection], 0);

            bubble.direction = (transform.position.x < 0) ? 1 : -1;
        }
        
        StartCoroutine(CreateBubble());
    }
}
