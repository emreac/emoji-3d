using DG.Tweening;
using UnityEngine;

public class UIStarAnimation : MonoBehaviour
{
    public RectTransform star; // Reference to the RectTransform of the star
    public RectTransform startPoint; // Starting position of the star (off-screen or center)
    public RectTransform endPoint; // Target position (e.g., a score or goal area)
    public float moveDuration = 1.5f; // Duration of the movement
    public float scaleDuration = 1f; // Duration for scaling effect
    public float rotationDuration = 1f; // Duration for rotation effect
    public CanvasGroup starCanvasGroup; // For fading effect (optional)

    void Start()
    {
        // Check if the CanvasGroup is assigned before using it
        if (starCanvasGroup != null)
        {
            starCanvasGroup.alpha = 0;  // Ensure the star is invisible initially
        }
    }

    public void StartStarAnimation()
    {
        // Safely check if star and CanvasGroup are not null
        if (star == null || startPoint == null || endPoint == null)
        {
            Debug.LogError("Star, StartPoint, or EndPoint is missing.");
            return;
        }

        // Reset star's position to the start point
        star.position = startPoint.position;

        // Ensure CanvasGroup is not null and fade in the star
        if (starCanvasGroup != null)
        {
            starCanvasGroup.alpha = 1;  // Make the star visible
            starCanvasGroup.DOFade(1, 0.5f); // Fade-in effect over 0.5 seconds
        }

        // Start the movement animation
        star.DOMove(endPoint.position, moveDuration).SetEase(Ease.InOutQuad);

        // Add scaling effect
        star.DOScale(new Vector3(1.2f, 1.2f, 1), scaleDuration).SetLoops(2, LoopType.Yoyo);

        // Add rotation effect
        star.DORotate(new Vector3(0, 0, 360), rotationDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);

        // Optionally, fade out the star at the end
        if (starCanvasGroup != null)
        {
            starCanvasGroup.DOFade(0, 0.5f).SetDelay(moveDuration); // Fade out after the move completes
        }
    }

    // Reset the animation when restarting the game
    public void ResetStarAnimation()
    {
        // Make sure to stop any active tweens before resetting
        star.DOKill();

        // Reset the star's position, scale, and rotation
        star.position = startPoint.position;
        star.localScale = Vector3.one;
        star.rotation = Quaternion.identity;

        // Reset CanvasGroup's alpha if it exists
        if (starCanvasGroup != null)
        {
            starCanvasGroup.alpha = 0;  // Ensure the star is hidden
        }
    }
}
