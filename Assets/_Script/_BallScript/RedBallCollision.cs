using System.Collections.Generic;
using UnityEngine;

public class RedBallCollision : MonoBehaviour
{
    private HashSet<GameObject> touchingRedBalls = new HashSet<GameObject>(); // Track which red balls are touching

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object collided with is a red ball
        if (collision.gameObject.CompareTag("RedBall") && !touchingRedBalls.Contains(collision.gameObject))
        {
            touchingRedBalls.Add(collision.gameObject);

            // Check if there are 2 other red balls touching this one
            if (touchingRedBalls.Count == 2) // Including this ball makes 3 balls
            {
                Debug.Log("Three red balls are touching!");
                // Add your logic for what happens when 3 red balls touch each other
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Remove the ball from the list when it stops touching
        if (collision.gameObject.CompareTag("RedBall"))
        {
            touchingRedBalls.Remove(collision.gameObject);
        }
    }
}
