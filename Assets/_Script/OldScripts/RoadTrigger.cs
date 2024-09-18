using UnityEngine;

public class RoadTrigger : MonoBehaviour
{
    [SerializeField] private GameObject road;
    private Vector3 nextPosition;
    private Quaternion rotation = Quaternion.Euler(0, 90, 0);
    private float roadLength = 290f; // Length of each road segment

    private void Start()
    {
        // Initialize the next position where the first road will be generated
        nextPosition = new Vector3(0, 0, roadLength);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RoadTrigger"))
        {
            // Instantiate the road at the next position
            Instantiate(road, nextPosition, rotation);

            // Update the next position for the following road segment
            nextPosition += new Vector3(0, 0, roadLength);
        }
    }
}
