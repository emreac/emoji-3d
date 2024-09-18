using UnityEngine;

public class Attractor : MonoBehaviour
{
    public float attractionForce = 10f; // Force of attraction
    public float range = 10f; // Range within which objects are attracted

    void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Calculate direction towards the attractor
            Vector3 direction = transform.position - other.transform.position;

            // Apply a force towards the attractor
            float distance = direction.magnitude;
            if (distance <= range)
            {
                rb.AddForce(direction.normalized * attractionForce / distance); // Inverse square law-like effect
            }
        }
    }
}
