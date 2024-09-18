using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerController : MonoBehaviour
{
    public float horizontalInput;
    [SerializeField] private GameObject projectilePrefab;

    public float speed;
    [SerializeField] private float lBound = -4f;
    [SerializeField] private float rBound = 4f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < lBound)
        {
            transform.position = new Vector3(lBound, transform.position.y, transform.position.z);
        }else if (transform.position.x > rBound)
        {
            transform.position = new Vector3(rBound,transform.position.y, transform.position.z);
        }
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        //Input
        if (Input.GetKeyDown(KeyCode.Space)) { 

            //activate food
            //Use instantiate for spawning objects
            Instantiate(projectilePrefab,transform.position,projectilePrefab.transform.rotation);

            
        
        }
    }
}
