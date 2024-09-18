using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CountdownTimer : MonoBehaviour
{

    public float countdownTime = 30f;
    public TextMeshProUGUI countdownText;
    public bool isCountdownRunning = true;
    [SerializeField] private GameObject loseUI;

    public float remainingTime; // Store remaining time here
    private bool isGameComplete = false; // Track if the game is completed

    void Start()
    {
        
        DraggableBall.isGameCompleted = false;
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        remainingTime = countdownTime;

        while (remainingTime > 0 && isCountdownRunning)
        {
            if (countdownText != null)
            {
                countdownText.text = remainingTime.ToString("F0");
            }

            if (remainingTime <= 10 && countdownText.color != Color.red)
            {
                countdownText.DOColor(Color.red, 1f);
            }

            yield return new WaitForSeconds(1f);
            remainingTime--;

            if (isGameComplete) break; // Stop if game is complete
        }

        if (countdownText != null)
        {
            countdownText.text = "0";
        }

        if (remainingTime <= 0)
        {
            GameOver();
        }
    }

    public void CompleteGame()
    {

        DraggableBall.isGameCompleted = true;
        isGameComplete = true; // Mark the game as complete
        StopCountdown();
        StartCoroutine(AddBallsForLeftoverTime()); // Add balls one by one
    }


    public void StopCountdown()
    {
        isCountdownRunning = false;
    }

    private void GameOver()
    {
        //Disable dragging
        DraggableBall.isGameCompleted = true;
        loseUI.SetActive(true);
        Debug.Log("Game Over! Time's up!");
    }

    private IEnumerator AddBallsForLeftoverTime()
    {
        yield return new WaitForSeconds(1);

        // For each leftover second, add one ball
        while (remainingTime > 0)
        {
            // Add the ball to the destroyed ball count
            BallCounterManager.Instance.AddDestroyedBall();
            
            // Update the countdown text to reflect the remaining leftover time
            if (countdownText != null)
            {
                countdownText.text = remainingTime.ToString("F0");
            }

            // Decrease the remaining time and update the UI
            remainingTime--;

            yield return new WaitForSeconds(0.05f); // Delay for adding each ball
        }

        // Ensure countdown shows 0 once the leftover time is consumed
        if (countdownText != null)
        {
            countdownText.text = "0";
        }
    }
}
