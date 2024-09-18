using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLoop : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private float moveSpeed;
    //[SerializeField] private float offsetZ = 15f;
    private float repeatWith;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWith = GetComponent<BoxCollider>().size.x/2;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left* moveSpeed*Time.deltaTime);
        if(transform.position.z < startPos.z - repeatWith)
        {
            transform.position = startPos;
        }
    }
}
