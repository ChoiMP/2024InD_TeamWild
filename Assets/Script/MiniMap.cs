using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    private GameObject mapStartPoint;
    [SerializeField]
    private GameObject mapEndPoint;
    [SerializeField]
    private GameObject mapPlayer;

    private float mapLength;
    private float timer;
    private const float totalTime = 60f; // Total time in seconds

    private void Awake()
    {
        if (mapStartPoint == null || mapEndPoint == null || mapPlayer == null)
        {
            Debug.LogError("Map objects are not assigned in the inspector.");
            return;
        }

        // Calculate the length of the map in world units
        mapLength = Mathf.Abs(mapEndPoint.transform.position.x - mapStartPoint.transform.position.x);
    }

    private void Start()
    {
        timer = 0f; // Initialize timer
    }

    private void Update()
    {
        if (mapLength == 0)
        {
            Debug.LogError("Map length is zero, check the positions of mapStartPoint and mapEndPoint.");
            return;
        }

        // Update the timer
        timer += Time.deltaTime;
        timer = Mathf.Clamp(timer, 0f, totalTime); // Clamp timer between 0 and totalTime

        // Calculate the player's ratio position based on the timer
        float ratio = timer / totalTime;

        // Calculate the corresponding position on the mini-map
        float mapX = mapStartPoint.transform.position.x + mapLength * ratio;

        // Update the mini-map player position
        mapPlayer.transform.position = new Vector2(mapX, mapPlayer.transform.position.y);
    }
}
