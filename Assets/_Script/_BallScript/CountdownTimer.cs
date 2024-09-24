using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private GoldObject goldObject;
    [SerializeField] private LevelManager levelManager;
    public float countdownTime = 30f;
    public TextMeshProUGUI countdownText;
    public bool isCountdownRunning = true;
    [SerializeField] private GameObject loseUI;
    private bool isUserCompleteLevel;

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

            if (isGameComplete) break; // Stop if the game is complete
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
        float score = BallCounterManager.Instance.destroyedBallCount;

        TinySauce.OnGameFinished(isUserCompleteLevel,score,levelManager.levelName);
        DraggableBall.isGameCompleted = true;
        isGameComplete = true; // Mark the game as complete
        StopCountdown();
        StartCoroutine(AddBallsForLeftoverTime()); // Add balls one by one
    }

    public void StopCountdown()
    {
        isCountdownRunning = false;
    }

    public void GameOver()
    {
        // Disable dragging and clickable
        goldObject.isClickable = false;
        DraggableBall.isGameCompleted = true;
        loseUI.SetActive(true);
        Debug.Log("Game Over! Time's up!");
    }

    private IEnumerator AddBallsForLeftoverTime()
    {
        // Store the initial remaining time so that the correct amount of balls are added
        int initialRemainingTime = Mathf.FloorToInt(remainingTime);

        yield return new WaitForSeconds(1);

        // For each leftover second, add one ball
        for (int i = 0; i < initialRemainingTime; i++)
        {
            // Add the ball to the destroyed ball count
            BallCounterManager.Instance.AddDestroyedBall();

            // Update the countdown text to reflect the remaining leftover time
            remainingTime--;

            // Ensure remainingTime doesn't go below 0
            if (remainingTime < 0)
            {
                remainingTime = 0;
            }

            if (countdownText != null)
            {
                countdownText.text = remainingTime.ToString("F0");
            }

            yield return new WaitForSeconds(0.05f); // Delay for adding each ball
        }

        // Ensure countdown shows 0 once the leftover time is consumed
        if (countdownText != null)
        {
            countdownText.text = "0";
        }
    }
}
