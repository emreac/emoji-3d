using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    

    [SerializeField] private GameObject[] animalPrefabs;
    [SerializeField] private float minX = -4f;
    [SerializeField] private float maxX = 4f;
    [SerializeField] private float birthRate;
    private Coroutine spawnCoroutine;
    [SerializeField] private float startDelay = 2f;
    //[SerializeField] private float gravityModifier;
    // Start is called before the first frame update
    void Start()
    {
        //Physics.gravity *= gravityModifier;
        //spawnCoroutine = StartCoroutine(SpawnObjects());
        InvokeRepeating("SpawnRandomAnimalInvoke",startDelay, birthRate);
    }

    void SpawnRandomAnimalInvoke() 
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), 0, 20);
        Instantiate(animalPrefabs[animalIndex], spawnPosition, animalPrefabs[animalIndex].transform.rotation);
    }

    //Coroutine way 
    IEnumerator SpawnObjects() 
    {
        while (true) {

            
            int animalIndex = Random.Range(0, animalPrefabs.Length);
            Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX),0,20);
            Instantiate(animalPrefabs[animalIndex], spawnPosition, animalPrefabs[animalIndex].transform.rotation);

            yield return new WaitForSeconds(birthRate);
        
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            int animalIndex = Random.Range(0, animalPrefabs.Length);
            Instantiate(animalPrefabs[animalIndex],new Vector3(Random.Range(minX,maxX),0,20),animalPrefabs[animalIndex].transform.rotation);
        }
        */
    }
}
