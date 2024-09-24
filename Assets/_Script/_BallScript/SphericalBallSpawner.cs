using UnityEngine;

public class SphericalBallSpawner : MonoBehaviour
{

    //[SerializeField] private Animator fadeInanimator;
    public GameObject[] ballPrefabs; // The ball prefab to instantiate
    public int numberOfBalls = 20; // Number of balls to instantiate
    public float sphereRadius = 5f; // Radius of the sphere
    public float minScale = 0.5f; // Minimum scale for the ball
    public float maxScale = 2f; // Maximum scale for the ball

    //Setting Gravity
    [SerializeField] private Vector3 defaultGravity = new Vector3(0, -9.81f, 0); // Default gravity
    
    [SerializeField] private float gravityModifier;

    void Start()
    {

        //fadeInanimator.SetBool("fadeInStart", true);
        Physics.gravity *= gravityModifier;
        SpawnBallsInSphere();
        ResetGravity();
    }
    void ResetGravity()
    {
        // Set gravity to the default value multiplied by the modifier
        Physics.gravity = defaultGravity * gravityModifier;
    }
    void SpawnBallsInSphere()
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            int randomBall = Random.Range(0, ballPrefabs.Length);
            // Generate a random point on a sphere using spherical coordinates
            Vector3 randomDirection = Random.onUnitSphere;

            // Scale the direction by the desired sphere radius
            Vector3 spawnPosition = randomDirection * sphereRadius;

            // Instantiate the ball at the calculated position with no rotation
            GameObject newBall = Instantiate(ballPrefabs[randomBall], spawnPosition, Quaternion.identity);

            // Randomize the scale of the ball
            float randomScale = Random.Range(minScale, maxScale);
            newBall.transform.localScale = Vector3.one * randomScale; // Apply uniform scaling
        }
    }
}
