using UnityEngine;

public class RoadGeneratorTrigger : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float roadLength = 100f; // Length of each road segment
    public Transform roadParent; // The parent transform to attach the generated roads

    private Vector3 nextRoadPosition;

    private void Start()
    {
        // Set the initial position for the first road segment
        nextRoadPosition = new Vector3(0, 0, roadLength);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Generate a new road segment when the player enters the trigger
            GenerateRoad();
        }
    }

    private void GenerateRoad()
    {
        // Get a road segment from the object pool
        GameObject newRoad = ObjectPooler.Instance.GetPooledObject();

        // Position the new road segment
        newRoad.transform.position = nextRoadPosition;
        newRoad.transform.SetParent(roadParent);

        // Update the position for the next road segment
        nextRoadPosition.z -= roadLength;
    }
}
