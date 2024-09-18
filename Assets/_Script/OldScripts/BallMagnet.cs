using UnityEngine;

public class BallMagnet : MonoBehaviour
{
    public Transform mainBall; // The main ball (attractor)
    public float attractionForce = 5f; // The strength of the attraction
    public float orbitDistance = 2f; // Distance at which balls will orbit around the main ball
    public float orbitSpeed = 50f; // Speed of orbiting

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 directionToMainBall = mainBall.position - transform.position;
        float distance = directionToMainBall.magnitude;

        if (distance > orbitDistance)
        {
            // Pull towards the main ball until within the orbit distance
            rb.AddForce(directionToMainBall.normalized * attractionForce);
        }
        else
        {
            // When within orbit distance, orbit around the main ball
            OrbitAroundMainBall();
        }
    }

    void OrbitAroundMainBall()
    {
        // Rotate around the main ball while maintaining a fixed distance
        Vector3 orbitDirection = Vector3.Cross(Vector3.up, mainBall.position - transform.position).normalized;
        rb.MovePosition(transform.position + orbitDirection * orbitSpeed * Time.fixedDeltaTime);
    }
}
