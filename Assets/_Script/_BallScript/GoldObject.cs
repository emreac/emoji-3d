using System.Collections;
using UnityEngine;
using DG.Tweening; // Import DoTween

public class GoldObject : MonoBehaviour
{
    [SerializeField] private PopSoundManager popSoundManager;
    [SerializeField] private UILeftOverTextAnim uiLeftTextAnim;  
    [SerializeField] private CountdownTimer countdownTimer;
    public Vector3 targetPosition;  // The position to move the gold object to
    public float moveDuration = 1f; // Duration of the move animation
    public Vector3 targetScale = new Vector3(2f, 2f, 2f);  // Target scale for the gold object
    public float scaleDuration = 1f; // Duration of the scale animation
    [SerializeField] private GameObject winUI;
    [SerializeField] private Rigidbody rb;
    
    [SerializeField] private GameObject shine;
    public bool isClickable = true;


    private void Start()
    {
        isClickable = true;
    }
    // Level completion logic
    public void CompleteLevel()
    {

        uiLeftTextAnim.LeftOverUIAnimation();
        // Replace this with your actual level completion logic
        Debug.Log("Level Completed!");
        //Physics.gravity = new Vector3(0, 0, 0);
        StartCoroutine(WinUILoader());
        countdownTimer.CompleteGame();
        shine.SetActive(true);
        DisableRigidbody();
        popSoundManager.PlayWin();

        // Animate the movement and scale of the gold object using DoTween
        Sequence sequence = DOTween.Sequence(); // Create a DoTween sequence

        // Add movement to the sequence
        sequence.Append(transform.DOMove(targetPosition, moveDuration)
                       .SetEase(Ease.InOutQuad)); // Movement easing

        // Add scaling to the sequence, it will run after movement
        sequence.Join(transform.DOScale(targetScale, scaleDuration)
                       .SetEase(Ease.InOutQuad)); // Scaling easing

        // Add a callback when both movement and scaling are done
        sequence.OnComplete(() => {
            //Debug.Log("Gold object moved and scaled to target.");
            // Any additional logic after both animations complete
        });
    }
    private void DisableRigidbody()
    {
        if (rb != null)
        {
            rb.isKinematic = true;  // Disable physics interaction
            rb.useGravity = false;  // Disable gravity
        }
    }
    IEnumerator WinUILoader()
    {
        yield return new WaitForSeconds(0.5f);
        winUI.SetActive(true);
        
    }

    // Detect mouse click on the gold object
    private void OnMouseDown()
    {
        // Only allow clicking if the object is clickable
        if (isClickable)
        {
            CompleteLevel(); // Call the method to complete the level
            isClickable = false; // Set the flag to false so it can't be clicked again
        }
    }

    // Optionally, you can use this method to enable the object to be clickable again later
    public void SetClickable(bool clickable)
    {
        isClickable = clickable;
    }
}
