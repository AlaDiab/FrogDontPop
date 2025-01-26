using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall : MonoBehaviour
{
    private Player player;
    public Bubble bubblePrefab;
    private List<int> heights = new List<int>();

    void Start()
    {
        // Start creating bubbles
        StartCoroutine(CreateBubble());
    }

    void Update()
    {
        player = (player == null) ? FindObjectOfType<Player>() : player;

        // Follow the player
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);

        // Repopulate heights
        if (heights.Count == 0)
        {
            for (int i = -1; i < 12; i++)
            {
                heights.Add(i);
            }
        }
    }

    IEnumerator CreateBubble()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(4, 10));
        Bubble bubble = Instantiate(bubblePrefab);
        int heightSelection = Random.Range(0, heights.Count);
        bubble.transform.position = new Vector3(transform.position.x, player.transform.position.y + heights[heightSelection], 0);
        heights.Remove(heightSelection);
        bubble.direction = (transform.position.x < 0) ? 1 : -1;
        StartCoroutine(CreateBubble());
    }
}
