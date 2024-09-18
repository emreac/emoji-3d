using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnManager : MonoBehaviour
{
    /// <summary>
    /// used random range get random interval
    /// Instantiate the obstacles using coroutine, could be invaokeRepeating
    /// declare a spawn pos > Vector3 spanwpos = new Vector3(0,0,30);
    /// Instentiate(object, spawnpos, quaternion.identity);
    /// </summary>
    /// 

    private SideRunnerPlayerController controller;
    [SerializeField] private GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(0, 0, 30);
    //[SerializeField] private float spawnRate = 1f;
    private float minX = 1;
    private float maxX = 3;
    // Start is called before the first frame update
    void Start()
    {
    
        controller = GameObject.Find("Player").GetComponent<SideRunnerPlayerController>();
        StartCoroutine(ObstacleSpawnTimer());
        //Instantiate(obstaclePrefab,spawnPos,Quaternion.identity);

    }

    IEnumerator ObstacleSpawnTimer()
    {

        while (!controller.gameOver)
        {
            float randomInterval = Random.Range(minX, maxX);
            //StartDelay
            //yield return new WaitForSeconds(1);

            // Instantiate(object, transform, rotaion);
            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(randomInterval);
   
         
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Explosive Creation(every frame generates)
        //StartCoroutine(ObstacleSpawnTimer());

    }
}
