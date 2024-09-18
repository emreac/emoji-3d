using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TagBallDestroyer : MonoBehaviour
{
    public GameObject splashEffectPrefab;
    private HashSet<GameObject> touchingBalls = new HashSet<GameObject>();  // Track touching balls
    private const float holdTime = 0.0f;  // Time to hold for destruction check
    private bool isDestructionScheduled = false;
    public bool hasBeenMoved = false;  // Flag to check if ball was moved

    void OnCollisionEnter(Collision collision)
    {
        // Check if the tags match
        if (collision.gameObject.CompareTag(gameObject.tag))
        {
            Debug.Log(gameObject.name + " collided with " + collision.gameObject.name + ". Tags match!");

            // Add the ball if the tags match
            touchingBalls.Add(collision.gameObject);

            // Check for destruction only if the ball has been moved
            if (hasBeenMoved)
            {
                StartDestructionCheck();
            }
        }
        else
        {
            Debug.Log(gameObject.name + " collided with " + collision.gameObject.name + ". Tags do not match.");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Remove ball when they stop touching
        touchingBalls.Remove(collision.gameObject);
    }

    private void StartDestructionCheck()
    {
        // Start destruction logic if 2 or more balls are touching and ball has been moved
        if (touchingBalls.Count >= 1 && !isDestructionScheduled)
        {
            Debug.Log("Starting destruction check for " + gameObject.name);
            isDestructionScheduled = true;
            StartCoroutine(CheckAndDestroyBalls());
        }
    }

    private IEnumerator CheckAndDestroyBalls()
    {
        yield return new WaitForSeconds(holdTime);  // Wait for the specified hold time

        if (touchingBalls.Count >= 1 && AreMatchingBalls())
        {
            Debug.Log("Balls are matching, destroying...");
            DestroyBalls();
        }

        isDestructionScheduled = false;
    }

    private bool AreMatchingBalls()
    {
        foreach (GameObject ball in touchingBalls)
        {
            if (ball == null || ball.tag != gameObject.tag)
            {
                Debug.Log("Matching failed. Ball tag: " + ball?.tag + " does not match " + gameObject.tag);
                return false;
            }
        }

        return true;  // All balls match in tag
    }

    private void DestroyBalls()
    {
        foreach (GameObject ball in touchingBalls)
        {
            if (ball != null)
            {
                InstantiateSplashEffect(ball.transform.position);
                Debug.Log("Destroying ball: " + ball.name);
                Destroy(ball);
            }
        }

        InstantiateSplashEffect(transform.position);
        Destroy(gameObject);

        touchingBalls.Clear();
    }

    private void InstantiateSplashEffect(Vector3 position)
    {
        if (splashEffectPrefab != null)
        {
            GameObject effect = Instantiate(splashEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 2f);  // Destroy the splash effect after 2 seconds
        }
    }
}
