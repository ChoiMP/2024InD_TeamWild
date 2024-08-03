using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    private GameObject startPoint;
    [SerializeField]
    private GameObject endPoint;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject mapStartPoint;
    [SerializeField]
    private GameObject mapEndPoint;
    [SerializeField]
    private GameObject mapPlayer;

    private float worldLength;
    private float mapLength;

    private void Awake()
    {
        // Automatically find and assign GameObjects if not already assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (startPoint == null)
        {
            startPoint = GameObject.FindGameObjectWithTag("StartRunning");
        }
        if (endPoint == null)
        {
            endPoint = GameObject.FindGameObjectWithTag("StopRunning");
        }
        if (mapStartPoint == null || mapEndPoint == null || mapPlayer == null)
        {
            Debug.LogError("Map objects are not assigned in the inspector.");
            return;
        }

        // Calculate lengths
        worldLength = Mathf.Abs(endPoint.transform.position.x - startPoint.transform.position.x);
        mapLength = Mathf.Abs(mapEndPoint.transform.position.x - mapStartPoint.transform.position.x);
    }

    private void FixedUpdate()
    {
        if (worldLength == 0 || mapLength == 0)
        {
            Debug.LogError("World or map length is zero, check the positions of start and end points.");
            return;
        }

        // Get the player's current x position
        float playerX = player.transform.position.x;

        // Clamp the player position within the start and end points
        playerX = Mathf.Clamp(playerX, startPoint.transform.position.x, endPoint.transform.position.x);

        // Calculate the player's ratio position between start and end points
        float ratio = (playerX - startPoint.transform.position.x) / worldLength;

        // Calculate the corresponding position on the mini-map
        float mapX = mapStartPoint.transform.position.x + mapLength * ratio;

        // Update the mini-map player position
        mapPlayer.transform.position = new Vector2(mapX, mapPlayer.transform.position.y);
    }
}
