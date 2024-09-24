using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBombController : MonoBehaviour
{
    private CountdownTimer countdownTimer;
    // Start is called before the first frame update
    void Start()
    {
       // countdownTimer = new CountdownTimer();
       
        countdownTimer = GetComponent<CountdownTimer>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MineBomb"))
        {
            countdownTimer.GameOver();
            Debug.Log("BOOOM!");
        }
    }
}
