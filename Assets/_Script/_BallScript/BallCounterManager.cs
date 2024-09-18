using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;


public class BallCounterManager : MonoBehaviour
{
    public static BallCounterManager Instance;
    private int destroyedBallCount = 0;
    public TextMeshProUGUI ballCounterText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Subscribe to the sceneLoaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Callback when a scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reassign the UI element after the new scene is loaded
        ballCounterText = GameObject.Find("BallCounterText")?.GetComponent<TextMeshProUGUI>();

        // Optionally update the UI after reassignment
        UpdateCounterText();
    }
    // Ensure to unsubscribe to avoid memory leaks
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void AddDestroyedBall()
    {
        destroyedBallCount++;
        UpdateCounterText();
       
      
    }

    public void ResetBallCount()
    {
        destroyedBallCount = 0; // Reset the count to 0
        UpdateCounterText();     // Update the UI text to reflect the reset
    }


    public void UpdateCounterText()
    {
        if (ballCounterText != null)
        {
            //Debug.Log("Updating ball count: " + destroyedBallCount);
            ballCounterText.text = (destroyedBallCount / 2).ToString();
        }
    }


}
