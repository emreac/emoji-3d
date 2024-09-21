using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialUI;    // Reference to the tutorial UI GameObject
    public static bool isTutorialActive = true;        // Flag to check if the tutorial is still active

    private string tutorialKey = "HasSeenTutorial";    // Unique key for PlayerPrefs

    void Start()
    {
        // Check if the player has already seen the tutorial
        if (PlayerPrefs.GetInt(tutorialKey, 0) == 0)
        {
            // If not, show the tutorial
            ShowTutorial();
        }
        else
        {
            // If the player has already seen the tutorial, allow the game to proceed
            isTutorialActive = false;
            tutorialUI.SetActive(false);
        }
    }

    private void ShowTutorial()
    {
        tutorialUI.SetActive(true);   // Show the tutorial UI
        isTutorialActive = true;      // Block the game logic
        Time.timeScale = 0;           // Freeze the game
    }

    void Update()
    {
        // Detect mouse click or touch to close the tutorial
        if (tutorialUI.activeSelf && Input.GetMouseButtonDown(0))
        {
            CloseTutorial();
        }
    }

    private void CloseTutorial()
    {
        tutorialUI.SetActive(false);  // Hide the tutorial UI
        isTutorialActive = false;     // Allow the game to start

        // Unfreeze the game
        Time.timeScale = 1;

        // Save the tutorial "seen" state in PlayerPrefs
        PlayerPrefs.SetInt(tutorialKey, 1);
        PlayerPrefs.Save();
    }
}
