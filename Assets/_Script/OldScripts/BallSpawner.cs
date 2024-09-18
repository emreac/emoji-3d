using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Animator fadeInanimator;

    [SerializeField] private GameObject[] balls;
    private Vector3 spawnPoint;

    private float minScale = 0.5f;
    private float maxScale = 2.0f;
    [SerializeField] private float repeatInterval;
    [SerializeField] private float stopAfterTime;
    
    // Start is called before the first frame update
    void Start()
    {
        fadeInanimator.SetBool("fadeInStart", true);
        InvokeRepeating("SpawnBallsInvoke", 1f, repeatInterval);
        Invoke("StopInvoke",stopAfterTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBallsInvoke()
    {
        float randomScale = Random.Range(minScale, maxScale);   
        int ballIndex = Random.Range(0, balls.Length);
        Vector3 spawnPoint = new Vector3(0,0,0);
        Instantiate(balls[ballIndex], spawnPoint, Quaternion.identity);
        GameObject newBall = Instantiate(balls[ballIndex], spawnPoint, Quaternion.identity);
        newBall.transform.localScale = Vector3.one * randomScale;
    }

    void StopInvoke()
    {
        CancelInvoke("SpawnBallsInvoke");
        Debug.Log("Invoking Stoped!");
    }
}
