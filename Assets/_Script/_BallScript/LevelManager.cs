using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
  
    public int currentLevel;
    public string levelName;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene;
        currentScene = SceneManager.GetActiveScene();
        currentLevel = currentScene.buildIndex;
        levelName = currentScene.name;
        TinySauce.OnGameStarted(levelName);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartLevel()
    {
        // Reset ball count
        BallCounterManager.Instance.ResetBallCount();

        // Reinitialize or reset UI
        BallCounterManager.Instance.UpdateCounterText();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        // Reset ball count
        BallCounterManager.Instance.ResetBallCount();

        // Reinitialize or reset UI
        BallCounterManager.Instance.UpdateCounterText();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void GoLevel1()
    {
        SceneManager.LoadScene("Level1");
    } 
}
