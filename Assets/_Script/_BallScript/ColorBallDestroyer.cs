using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ColorBallDestroyer : MonoBehaviour
{
    public GameObject splashEffectPrefab;
    private HashSet<GameObject> touchingMovedBalls = new HashSet<GameObject>();  // Track only moved balls
    private const float holdTime = 0f;  // Time to hold for destruction check
    private bool isDestructionScheduled = false;
    public bool hasBeenMoved = false;  // Flag to check if the ball has been moved
  
    
    // Detect collision with other balls
    void OnCollisionEnter(Collision collision)
    {
        ColorBallDestroyer otherBallDestroyer = collision.gameObject.GetComponent<ColorBallDestroyer>();

        // Check if the other ball is valid and has the same tag
        if (otherBallDestroyer != null && collision.gameObject.tag == gameObject.tag)
        {
            // Add both moved and touched balls to the set if the current ball has been moved or the other one has been moved
            if (hasBeenMoved || otherBallDestroyer.hasBeenMoved)
            {
                touchingMovedBalls.Add(collision.gameObject);  // Add touched ball
                touchingMovedBalls.Add(gameObject);  // Add the current ball itself
                StartDestructionCheck();  // Initiate destruction check immediately
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Remove the ball from the set when they stop touching
        touchingMovedBalls.Remove(collision.gameObject);
    }

    private void StartDestructionCheck()
    {
        // Only destroy if there are 2 or more balls in the set (moved and touched) and it's not already scheduled
        if (touchingMovedBalls.Count >= 2 && !isDestructionScheduled)
        {
            isDestructionScheduled = true;
            StartCoroutine(CheckAndDestroyBalls());
        }
    }

    // Coroutine to wait for the specified hold time and then destroy the balls
    private IEnumerator CheckAndDestroyBalls()
    {
        yield return new WaitForSeconds(holdTime);  // Wait for hold time (set to 0 for instant destruction)

        if (touchingMovedBalls.Count >= 2 && AreMatchingMovedBalls())
        {
            DestroyMovedBalls();
        }

        isDestructionScheduled = false;
    }

    // Check if the balls in the set have the same tag
    private bool AreMatchingMovedBalls()
    {
        foreach (GameObject ball in touchingMovedBalls)
        {
            if (ball == null || ball.tag != gameObject.tag)
            {
                return false;
            }
        }

        return true;  // All balls match the tag
    }

    // Destroy the balls with matching tags
    private void DestroyMovedBalls()
    {
        // Destroy all touching balls in the set
        foreach (GameObject ball in touchingMovedBalls)
        {
            if (ball != null)
            {
                InstantiateSplashEffect(ball.transform.position);
                Destroy(ball);
                BallCounterManager.Instance.AddDestroyedBall();
               
            }
        }

        touchingMovedBalls.Clear();  // Clear the set after destruction
    }

 
    // Create the splash effect at the ball's position
    private void InstantiateSplashEffect(Vector3 position)
    {
        if (splashEffectPrefab != null)
        {
            GameObject splash = Instantiate(splashEffectPrefab, position, Quaternion.identity);
            Destroy(splash, 2f);  // Automatically destroy splash effect after 2 seconds
        }
    }
}
