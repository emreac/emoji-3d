using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextureBallDestroyer : MonoBehaviour
{
    public GameObject splashEffectPrefab;
    private HashSet<GameObject> touchingBalls = new HashSet<GameObject>();  // Track touching balls
    private const float holdTime = .0f;  // Time to hold for destruction check
    private bool isDestructionScheduled = false;
    public bool hasBeenMoved = false;  // Flag to check if ball was moved

    // Detect collision with other balls
    void OnCollisionEnter(Collision collision)
    {
        Renderer thisRenderer = GetComponent<Renderer>();
        Renderer otherRenderer = collision.gameObject.GetComponent<Renderer>();

        if (otherRenderer != null && AreTexturesMatching(thisRenderer, otherRenderer))
        {
            // Only add if the textures match
            touchingBalls.Add(collision.gameObject);

            // Check for destruction only if the ball has been moved
            if (hasBeenMoved)
            {
                StartDestructionCheck();
            }
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
            isDestructionScheduled = true;
            StartCoroutine(CheckAndDestroyBalls());
        }
    }

    // Coroutine to wait for hold time and destroy the balls if still valid
    private IEnumerator CheckAndDestroyBalls()
    {
        yield return new WaitForSeconds(holdTime);  // Wait for 1 second

        if (touchingBalls.Count >= 1 && AreMatchingBalls())
        {
            DestroyBalls();
        }

        isDestructionScheduled = false;
    }

    // Check if the balls have matching textures
    private bool AreMatchingBalls()
    {
        Renderer thisRenderer = GetComponent<Renderer>();
        foreach (GameObject ball in touchingBalls)
        {
            Renderer ballRenderer = ball.GetComponent<Renderer>();
            if (ball == null || !AreTexturesMatching(thisRenderer, ballRenderer))
            {
                return false;
            }
        }

        return true;  // All balls match in texture
    }

    // Helper function to compare textures
    private bool AreTexturesMatching(Renderer thisRenderer, Renderer otherRenderer)
    {
        return thisRenderer.material.mainTexture == otherRenderer.material.mainTexture;
    }

    // Destroy the matching balls
    private void DestroyBalls()
    {
        foreach (GameObject ball in touchingBalls)
        {
            if (ball != null)
            {
                InstantiateSplashEffect(ball.transform.position);
                Destroy(ball);
            }
        }

        // Destroy the current ball itself
        InstantiateSplashEffect(transform.position);
        Destroy(gameObject);

        touchingBalls.Clear();
    }

    // Create the splash effect at the ball's position
    private void InstantiateSplashEffect(Vector3 position)
    {
        if (splashEffectPrefab != null)
        {
            GameObject effect = Instantiate(splashEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 2f);  // Destroy the splash effect after 2 seconds
        }
    }
}
