using UnityEngine;

public class RandomXZPositionChanger : MonoBehaviour
{
    public GameObject[] objectsToMove; // Array of existing objects to move
    public Vector2 xRange = new Vector2(-10f, 10f); // Range for X-axis positions
    public Vector2 zRange = new Vector2(-10f, 10f); // Range for Z-axis positions
    public float fixedYPosition = 0f; // Fixed Y position for the objects

    void Start()
    {
        ChangePositions();
    }

    void ChangePositions()
    {
        foreach (GameObject obj in objectsToMove)
        {
            if (obj != null)
            {
                // Random X and Z positions within the defined range
                float randomX = Random.Range(xRange.x, xRange.y);
                float randomZ = Random.Range(zRange.x, zRange.y);

                // Create a new Vector3 with the random X, fixed Y, and random Z positions
                Vector3 newPosition = new Vector3(randomX, fixedYPosition, randomZ);

                // Set the object's position to the new random position
                obj.transform.position = newPosition;
            }
        }
    }
}
