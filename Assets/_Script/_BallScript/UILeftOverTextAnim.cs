using DG.Tweening;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UILeftOverTextAnim : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leftoverText;
    [SerializeField] CountdownTimer countdownTimer;
    public RectTransform movingObject; // Reference to the RectTransform of the star
    public RectTransform startPoint; // Starting position of the star (off-screen or center)
    public RectTransform endPoint; // Target position (e.g., a score or goal area)
    public float moveDuration = 1.5f; // Duration of the movement
    public float scaleDuration = 1f; // Duration for scaling effect
    public float rotationDuration = 1f; // Duration for rotation effect
    public CanvasGroup starCanvasGroup; // For fading effect (optional)

    void Start()
    {
        // Optionally, hide the star initially by setting its opacity to 0
        if (starCanvasGroup != null)
        {
            starCanvasGroup.alpha = 0;
        }

        // StartLeftoverUIAnimation();
    }

    public void StartLeftoverUIAnimation()
    {
        // Safely check if star and CanvasGroup are not null
        if (movingObject == null || startPoint == null || endPoint == null)
        {
            Debug.LogError("Star, StartPoint, or EndPoint is missing.");
            return;
        }

        // Move the text from start point to end point
        movingObject.position = startPoint.position;

        // Show the text by fading in
        if (starCanvasGroup != null)
        {
            starCanvasGroup.DOFade(1, 0.5f); // Fade-in effect over 0.5 seconds
        }

        // Start the movement animation
        movingObject.DOMove(endPoint.position, moveDuration).SetEase(Ease.InOutQuad);

        // Optionally, add scaling effect
        movingObject.DOScale(new Vector3(2f, 2f, 1), scaleDuration).SetLoops(2, LoopType.Yoyo);

        // Optionally, add rotation effect
        movingObject.DORotate(new Vector3(0, 0, 360), rotationDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);

        // Optionally, fade out the star at the end
        if (starCanvasGroup != null)
        {
            starCanvasGroup.DOFade(0, 0.5f).SetDelay(moveDuration); // Fade out after the move completes
        }
    }

    public void LeftOverUIAnimation()
    {
        StartLeftoverUIAnimation();
        leftoverText.text = "+" + countdownTimer.remainingTime;
    }

    public void ResetStarAnimation()
    {
        // Make sure to stop any active tweens before resetting
        movingObject.DOKill();

        // Reset the star's position, scale, and rotation
        movingObject.position = startPoint.position;
        movingObject.localScale = Vector3.one;
        movingObject.rotation = Quaternion.identity;

        // Reset CanvasGroup's alpha if it exists
        if (starCanvasGroup != null)
        {
            starCanvasGroup.alpha = 0;  // Ensure the star is hidden
        }
    }
}
